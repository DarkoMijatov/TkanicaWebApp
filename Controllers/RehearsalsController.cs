using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;
using TkanicaWebApp.ViewModels;

namespace TkanicaWebApp.Controllers
{
    public class RehearsalsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public RehearsalsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Rehearsals
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<Rehearsal> viewModel)
        {
            var tkanicaWebAppContext = _context.Rehearsal
                .Include(x => x.RehearsalEmployees)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalEmployees.Employee")
                .Include("RehearsalMembers.Member")
                .Include("RehearsalMembers.Member.MembershipFee")
                .Include("RehearsalMembers.Member.MembershipFee.MemberGroup");
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = viewModel.CurrentSort == sort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "dateAsc" => await tkanicaWebAppContext.OrderBy(x => x.Date).ToListAsync(),
                    "dateDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Date).ToListAsync(),
                    "memberGroupAsc" => await tkanicaWebAppContext.OrderBy(x => x.RehearsalMembers.Select(x => x.Member.MembershipFee.MemberGroup).First().Name).ToListAsync(),
                    "memberGroupDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.RehearsalMembers.Select(x => x.Member.MembershipFee.MemberGroup).First().Name).ToListAsync(),
                    "employeesCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.RehearsalEmployees.Count).ToListAsync(),
                    "employeesCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.RehearsalEmployees.Count).ToListAsync(),
                    "membersCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.RehearsalMembers.Count).ToListAsync(),
                    "membersCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.RehearsalMembers.Count).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderByDescending(x => x.Date).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderByDescending(x => x.Date).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.Date.ToString().Contains(viewModel.Search) || 
                        x.RehearsalMembers.Select(x => x.Member.MembershipFee.MemberGroup).First().Name.ToLower().Contains(viewModel.Search))
                    .ToList();
            }

            int pageCount = viewModel.List.Count % 5 == 0 ? viewModel.List.Count / 5 : viewModel.List.Count / 5 + 1;
            if (pageIndex != null)
            {
                viewModel.PageIndex = pageIndex!.Value;
                viewModel.HasPreviousPage = pageIndex > 1;
                viewModel.HasNextPage = pageIndex < pageCount;
            }
            else
            {
                viewModel.PageIndex = 1;
                viewModel.HasPreviousPage = false;
                viewModel.HasNextPage = pageCount > 1;
            }
            viewModel.List = viewModel.List.Skip((viewModel.PageIndex - 1) * 5).Take(5).ToList();

            return View(viewModel);
        }

        // GET: Rehearsals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rehearsal == null)
            {
                return NotFound();
            }

            var rehearsal = await _context.Rehearsal
                .Include(x => x.RehearsalEmployees)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalEmployees.Employee")
                .Include("RehearsalMembers.Member")
                .Include("RehearsalMembers.Member.MembershipFee")
                .Include("RehearsalMembers.Member.MembershipFee.MemberGroup")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rehearsal == null)
            {
                return NotFound();
            }

            return View(rehearsal);
        }

        // GET: Rehearsals/Create
        public IActionResult Create()
        {

            ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(x => x.Active), "Id", "FullName");
            ViewData["MemberId"] = new SelectList(_context.Member.Where(x => x.Active), "Id", "FullName");
            return View();
        }

        // POST: Rehearsals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,EmployeeIds,MemberIds")] RehearsalViewModel rehearsalViewModel)
        {
            var rehearsal = new Rehearsal();
            if (ModelState.IsValid)
            {
                rehearsal.Date = rehearsalViewModel.Date;
                rehearsal.RehearsalMembers = new List<RehearsalMember>();
                rehearsal.RehearsalEmployees = new List<RehearsalEmployee>();
                if (rehearsalViewModel.MemberIds != null && rehearsalViewModel.MemberIds.Any())
                {
                    foreach (var memberId in rehearsalViewModel.MemberIds)
                    {
                        rehearsal.RehearsalMembers.Add(new RehearsalMember { Rehearsal = rehearsal, MemberId = memberId });
                    }
                }
                if (rehearsalViewModel.EmployeeIds != null && rehearsalViewModel.EmployeeIds.Any())
                {
                    foreach (var employeeId in rehearsalViewModel.EmployeeIds)
                    {
                        rehearsal.RehearsalEmployees.Add(new RehearsalEmployee { Rehearsal = rehearsal, EmployeeId = employeeId });
                    }
                }
                _context.Add(rehearsal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rehearsal);
        }

        // GET: Rehearsals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rehearsal == null)
            {
                return NotFound();
            }

            var rehearsal = await _context.Rehearsal
                .Include(x => x.RehearsalEmployees)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalEmployees.Employee")
                .Include("RehearsalMembers.Member")
                .Include("RehearsalMembers.Member.MembershipFee")
                .Include("RehearsalMembers.Member.MembershipFee.MemberGroup")
                .FirstOrDefaultAsync(m => m.Id == id);
            var rehearsalViewModel = new RehearsalViewModel
            {
                Id = rehearsal.Id,
                Date = rehearsal.Date,
                EmployeeIds = rehearsal.RehearsalEmployees.Select(x => x.EmployeeId).ToList(),
                MemberIds = rehearsal.RehearsalMembers.Select(x => x.MemberId).ToList()
            };
            if (rehearsal == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(x => x.Active), "Id", "FullName", rehearsal.RehearsalEmployees.Select(x => x.EmployeeId));
            ViewData["MemberId"] = new SelectList(_context.Member.Where(x => x.Active), "Id", "FullName", rehearsal.RehearsalMembers.Select(x => x.MemberId));
            return View(rehearsalViewModel);
        }

        // POST: Rehearsals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,EmployeeIds,MemberIds")] RehearsalViewModel rehearsalViewModel)
        {
            var rehearsal = await _context.Rehearsal
                .Include(x => x.RehearsalEmployees)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalEmployees.Employee")
                .Include("RehearsalMembers.Member")
                .Include("RehearsalMembers.Member.MembershipFee")
                .Include("RehearsalMembers.Member.MembershipFee.MemberGroup")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (id != rehearsalViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    rehearsal.Date = rehearsalViewModel.Date;
                    _context.RehearsalEmployee.RemoveRange(rehearsal.RehearsalEmployees);
                    _context.RehearsalMember.RemoveRange(rehearsal.RehearsalMembers);
                    if (rehearsalViewModel.MemberIds != null && rehearsalViewModel.MemberIds.Any())
                    {
                        foreach (var memberId in rehearsalViewModel.MemberIds)
                        {
                            rehearsal.RehearsalMembers.Add(new RehearsalMember { Rehearsal = rehearsal, MemberId = memberId });
                        }
                    }
                    if (rehearsalViewModel.EmployeeIds != null && rehearsalViewModel.EmployeeIds.Any())
                    {
                        foreach (var employeeId in rehearsalViewModel.EmployeeIds)
                        {
                            rehearsal.RehearsalEmployees.Add(new RehearsalEmployee { Rehearsal = rehearsal, EmployeeId = employeeId });
                        }
                    }
                    _context.Update(rehearsal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RehearsalExists(rehearsalViewModel.Id.Value))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rehearsal);
        }

        // GET: Rehearsals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rehearsal == null)
            {
                return NotFound();
            }

            var rehearsal = await _context.Rehearsal
                .Include(x => x.RehearsalEmployees)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalEmployees.Employee")
                .Include("RehearsalMembers.Member")
                .Include("RehearsalMembers.Member.MembershipFee")
                .Include("RehearsalMembers.Member.MembershipFee.MemberGroup")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rehearsal == null)
            {
                return NotFound();
            }

            return View(rehearsal);
        }

        // POST: Rehearsals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rehearsal == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Rehearsal'  is null.");
            }
            var rehearsal = await _context.Rehearsal
                .Include(x => x.RehearsalEmployees)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalEmployees.Employee")
                .Include("RehearsalMembers.Member")
                .Include("RehearsalMembers.Member.MembershipFee")
                .Include("RehearsalMembers.Member.MembershipFee.MemberGroup")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rehearsal != null)
            {
                _context.Rehearsal.Remove(rehearsal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RehearsalExists(int id)
        {
          return _context.Rehearsal.Any(e => e.Id == id);
        }
    }
}
