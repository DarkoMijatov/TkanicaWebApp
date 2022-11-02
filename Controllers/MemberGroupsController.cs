using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;
using TkanicaWebApp.ViewModels;

namespace TkanicaWebApp.Controllers
{
    public class MemberGroupsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public MemberGroupsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: MemberGroups
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<MemberGroup> viewModel)
        {
            var tkanicaWebAppContext = _context.MemberGroup
                .Include(m => m.MembershipFees)
                .Include("MembershipFees.Members")
                .Include(x => x.EmployeeMemberGroups)
                .Include("MembershipFees.Members.RehearsalMembers");

            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = viewModel.CurrentSort == sort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "nameAsc" => await tkanicaWebAppContext.OrderBy(x => x.Name).ToListAsync(),
                    "nameDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Name).ToListAsync(),
                    "activeAsc" => await tkanicaWebAppContext.OrderBy(x => x.Active).ToListAsync(),
                    "activeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Active).ToListAsync(),
                    "createdAtAsc" => await tkanicaWebAppContext.OrderBy(x => x.CreatedAt).ToListAsync(),
                    "createdAtDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.CreatedAt).ToListAsync(),
                    "activeMembersCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.MembershipFees.SelectMany(x => x.Members).Count(x => x.Active)).ToListAsync(),
                    "activeMembersCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.MembershipFees.SelectMany(x => x.Members).Count(x => x.Active)).ToListAsync(),
                    "membersCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.MembershipFees.SelectMany(x => x.Members).Count()).ToListAsync(),
                    "membersCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.MembershipFees.SelectMany(x => x.Members).Count()).ToListAsync(),
                    "employeesCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.EmployeeMemberGroups.Count).ToListAsync(),
                    "employeesCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.EmployeeMemberGroups.Count).ToListAsync(),
                    "rehearsalsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.MembershipFees.SelectMany(x => x.Members).SelectMany(x => x.RehearsalMembers).Select(x => x.RehearsalId).Distinct().Count()).ToListAsync(),
                    "rehearsalsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.MembershipFees.SelectMany(x => x.Members).SelectMany(x => x.RehearsalMembers).Select(x => x.RehearsalId).Distinct().Count()).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.Name.ToLower().Contains(viewModel.Search))
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

        // GET: MemberGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberGroup == null)
            {
                return NotFound();
            }
            var memberGroup = await _context.MemberGroup
                .Include(m => m.MembershipFees)
                .Include("MembershipFees.Members")
                .Include(x => x.EmployeeMemberGroups)
                .Include("EmployeeMemberGroups.Employee")
                .Include("MembershipFees.Members.RehearsalMembers")
                .Include("MembershipFees.Members.RehearsalMembers.Rehearsal")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberGroup == null)
            {
                return NotFound();
            }

            return View(memberGroup);
        }

        // GET: MemberGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Active,CreatedAt,UpdatedAt")] MemberGroup memberGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberGroup);
        }

        // GET: MemberGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberGroup == null)
            {
                return NotFound();
            }

            var memberGroup = await _context.MemberGroup.FindAsync(id);
            if (memberGroup == null)
            {
                return NotFound();
            }
            return View(memberGroup);
        }

        // POST: MemberGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Active,CreatedAt,UpdatedAt")] MemberGroup memberGroup)
        {
            if (id != memberGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberGroupExists(memberGroup.Id))
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
            return View(memberGroup);
        }

        // GET: MemberGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberGroup == null)
            {
                return NotFound();
            }
            var memberGroup = await _context.MemberGroup
                .Include(m => m.MembershipFees)
                .Include("MembershipFees.Members")
                .Include(x => x.EmployeeMemberGroups)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberGroup == null)
            {
                return NotFound();
            }

            return View(memberGroup);
        }

        // POST: MemberGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberGroup == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.MemberGroup'  is null.");
            }
            var memberGroup = await _context.MemberGroup
                .Include(m => m.MembershipFees)
                .Include("MembershipFees.Members")
                .Include(x => x.EmployeeMemberGroups)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberGroup != null)
            {
                _context.MemberGroup.Remove(memberGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberGroupExists(int id)
        {
          return _context.MemberGroup.Any(e => e.Id == id);
        }
    }
}
