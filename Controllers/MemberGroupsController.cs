using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.MemberGroup
                .Include(m => m.MembershipFees)
                .Include("MembershipFees.Members")
                .Include(x => x.EmployeeMemberGroups)
                .Include("MembershipFees.Members.RehearsalMembers")
                .ToListAsync());
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
