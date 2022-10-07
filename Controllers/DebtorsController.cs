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
    public class DebtorsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public DebtorsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Debtors
        public async Task<IActionResult> Index()
        {
              return View(await _context.Debtor.ToListAsync());
        }

        // GET: Debtors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Debtor == null)
            {
                return NotFound();
            }

            var debtor = await _context.Debtor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (debtor == null)
            {
                return NotFound();
            }

            return View(debtor);
        }

        // GET: Debtors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Debtors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsCompany,Address,City,PhoneNumber,Email,IdNumber,TaxNumber,CreatedAt,UpdatedAt")] Debtor debtor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(debtor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(debtor);
        }

        // GET: Debtors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Debtor == null)
            {
                return NotFound();
            }

            var debtor = await _context.Debtor.FindAsync(id);
            if (debtor == null)
            {
                return NotFound();
            }
            return View(debtor);
        }

        // POST: Debtors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsCompany,Address,City,PhoneNumber,Email,IdNumber,TaxNumber,CreatedAt,UpdatedAt")] Debtor debtor)
        {
            if (id != debtor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(debtor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DebtorExists(debtor.Id))
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
            return View(debtor);
        }

        // GET: Debtors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Debtor == null)
            {
                return NotFound();
            }

            var debtor = await _context.Debtor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (debtor == null)
            {
                return NotFound();
            }

            return View(debtor);
        }

        // POST: Debtors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Debtor == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Debtor'  is null.");
            }
            var debtor = await _context.Debtor.FindAsync(id);
            if (debtor != null)
            {
                _context.Debtor.Remove(debtor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DebtorExists(int id)
        {
          return _context.Debtor.Any(e => e.Id == id);
        }
    }
}
