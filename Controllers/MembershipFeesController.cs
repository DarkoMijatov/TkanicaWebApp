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
    public class MembershipFeesController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public MembershipFeesController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: MembershipFees
        public async Task<IActionResult> Index()
        {
            var tkanicaWebAppContext = _context.MembershipFee.Include(m => m.MemberGroup).Include(m => m.Members);
            return View(await tkanicaWebAppContext.ToListAsync());
        }

        // GET: MembershipFees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MembershipFee == null)
            {
                return NotFound();
            }

            var membershipFee = await _context.MembershipFee
                .Include(m => m.MemberGroup)
                .Include(m => m.Members)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipFee == null)
            {
                return NotFound();
            }

            return View(membershipFee);
        }

        // GET: MembershipFees/Create
        public IActionResult Create()
        {
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name");
            return View();
        }

        // POST: MembershipFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MemberGroupId,Amount,CreatedAt,UpdatedAt")] MembershipFee membershipFee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipFee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name", membershipFee.MemberGroupId);
            return View(membershipFee);
        }

        // GET: MembershipFees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MembershipFee == null)
            {
                return NotFound();
            }
            var membershipFee = await _context.MembershipFee.FindAsync(id);
            if (membershipFee == null)
            {
                return NotFound();
            }
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name", membershipFee.MemberGroupId);
            return View(membershipFee);
        }

        // POST: MembershipFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MemberGroupId,Amount,CreatedAt,UpdatedAt")] MembershipFee membershipFee)
        {
            if (id != membershipFee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipFee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipFeeExists(membershipFee.Id))
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
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name", membershipFee.MemberGroupId);
            return View(membershipFee);
        }

        // GET: MembershipFees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MembershipFee == null)
            {
                return NotFound();
            }

            var membershipFee = await _context.MembershipFee
                .Include(m => m.MemberGroup)
                .Include(m => m.Members)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipFee == null)
            {
                return NotFound();
            }

            return View(membershipFee);
        }

        // POST: MembershipFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MembershipFee == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.MembershipFee'  is null.");
            }
            var membershipFee = await _context.MembershipFee.FindAsync(id);
            if (membershipFee != null)
            {
                _context.MembershipFee.Remove(membershipFee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipFeeExists(int id)
        {
          return _context.MembershipFee.Any(e => e.Id == id);
        }
    }
}
