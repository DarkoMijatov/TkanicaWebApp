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
        public async Task<IActionResult> Index()
        {
              return View(await _context.Rehearsal
                  .Include(x => x.RehearsalEmployees)
                  .Include(x => x.RehearsalMembers)
                  .Include("RehearsalEmployees.Employee")
                  .Include("RehearsalMembers.Member")
                  .Include("RehearsalMembers.Member.MembershipFee")
                  .Include("RehearsalMembers.Member.MembershipFee.MemberGroup")
                  .ToListAsync());
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

            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
            ViewData["MemberId"] = new SelectList(_context.Member.Where(x => x.Active), "Id", "FullName");
            return View();
        }

        // POST: Rehearsals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,EmployeeIds,MemberIds")] RehearsalViewModel rehearsalViewModel)
        {
            var rehearsal = new Rehearsal();
            if (ModelState.IsValid)
            {
                rehearsal.Date = rehearsalViewModel.Date;
                rehearsal.RehearsalMembers = new List<RehearsalMember>();
                rehearsal.RehearsalEmployees = new List<RehearsalEmployee>();
                foreach(var memberId in rehearsalViewModel.MemberIds)
                {
                    rehearsal.RehearsalMembers.Add(new RehearsalMember { Rehearsal = rehearsal, MemberId = memberId });
                }
                foreach (var employeeId in rehearsalViewModel.EmployeeIds)
                {
                    rehearsal.RehearsalEmployees.Add(new RehearsalEmployee { Rehearsal = rehearsal, EmployeeId = employeeId });
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

            var rehearsal = await _context.Rehearsal.FindAsync(id);
            if (rehearsal == null)
            {
                return NotFound();
            }
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name");
            ViewData["Employees"] = new SelectList(_context.EmployeeMemberGroup, "EmployeeId", "FullName");
            ViewData["Members"] = new SelectList(_context.Member, "Id", "FullName");
            return View(rehearsal);
        }

        // POST: Rehearsals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,CreatedAt,UpdatedAt")] Rehearsal rehearsal, [Bind("EmployeeId")] List<RehearsalEmployee> rehearsalEmployees, [Bind("MemberId")] List<RehearsalMember> rehearsalMembers)
        {
            if (id != rehearsal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rehearsal.RehearsalMembers = rehearsalMembers;
                    rehearsal.RehearsalEmployees = rehearsalEmployees;
                    _context.Update(rehearsal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RehearsalExists(rehearsal.Id))
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
