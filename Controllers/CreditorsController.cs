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
    public class CreditorsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public CreditorsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Creditors
        public async Task<IActionResult> Index()
        {
              return View(await _context.Creditor.ToListAsync());
        }

        // GET: Creditors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Creditor == null)
            {
                return NotFound();
            }

            var creditor = await _context.Creditor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditor == null)
            {
                return NotFound();
            }

            return View(creditor);
        }

        // GET: Creditors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Creditors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsCompany,Address,City,PhoneNumber,Email,IdNumber,TaxNumber,CreatedAt,UpdatedAt")] Creditor creditor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creditor);
        }

        // GET: Creditors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Creditor == null)
            {
                return NotFound();
            }

            var creditor = await _context.Creditor.FindAsync(id);
            if (creditor == null)
            {
                return NotFound();
            }
            return View(creditor);
        }

        // POST: Creditors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsCompany,Address,City,PhoneNumber,Email,IdNumber,TaxNumber,CreatedAt,UpdatedAt")] Creditor creditor)
        {
            if (id != creditor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditorExists(creditor.Id))
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
            return View(creditor);
        }

        // GET: Creditors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Creditor == null)
            {
                return NotFound();
            }

            var creditor = await _context.Creditor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditor == null)
            {
                return NotFound();
            }

            return View(creditor);
        }

        // POST: Creditors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Creditor == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Creditor'  is null.");
            }
            var creditor = await _context.Creditor.FindAsync(id);
            if (creditor != null)
            {
                _context.Creditor.Remove(creditor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditorExists(int id)
        {
          return _context.Creditor.Any(e => e.Id == id);
        }
    }
}
