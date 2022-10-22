﻿using System;
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
    public class BalancesController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public BalancesController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Balances
        public async Task<IActionResult> Index()
        {
            var tkanicaWebAppContext = _context.Balance
                .Include(b => b.AccountNumber)
                .Include(b => b.Transactions)
                .Include("Transactions.TransactionType");
            return View(await tkanicaWebAppContext.ToListAsync());
        }

        // GET: Balances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Balance == null)
            {
                return NotFound();
            }

            var balance = await _context.Balance
                .Include(b => b.AccountNumber)
                .Include(b => b.Transactions)
                .Include("Transactions.TransactionType")
                .Include("Transactions.Member")
                .Include("Transactions.Employee")
                .Include("Transactions.Debtor")
                .Include("Transactions.Creditor")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // GET: Balances/Create
        public IActionResult Create()
        {
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumber
                .Where(x => x.BalanceId == null && x.ClientId == null), "Id", "BankAccountNumber");
            return View();
        }

        // POST: Balances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AccountNumberId,IsCash")] Balance balance)
        {
            if (ModelState.IsValid)
            {
                if (balance.IsCash)
                    balance.AccountNumberId = null;
                _context.Add(balance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountNumberId"] = new SelectList(_context.AccountNumber
                .Where(x => x.BalanceId == null && x.ClientId == null), "Id", "BankAccountNumber");
            return View(balance);
        }

        // GET: Balances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Balance == null)
            {
                return NotFound();
            }

            var balance = await _context.Balance
               .Include(b => b.AccountNumber)
               .Include(b => b.Transactions)
               .Include("Transactions.TransactionType")
               .FirstOrDefaultAsync(m => m.Id == id);
            if (balance == null)
            {
                return NotFound();
            }
            return View(balance);
        }

        // POST: Balances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name","IsCash","AccountNumberId")] Balance balance)
        {
            if (id != balance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(balance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BalanceExists(balance.Id))
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
            return View(balance);
        }

        // GET: Balances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Balance == null)
            {
                return NotFound();
            }

            var balance = await _context.Balance
               .Include(b => b.AccountNumber)
               .Include(b => b.Transactions)
               .Include("Transactions.TransactionType")
               .FirstOrDefaultAsync(m => m.Id == id);
            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // POST: Balances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Balance == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Balance'  is null.");
            }
            var balance = await _context.Balance
               .Include(b => b.AccountNumber)
               .Include(b => b.Transactions)
               .Include("Transactions.TransactionType")
               .FirstOrDefaultAsync(m => m.Id == id);
            if (balance != null)
            {
                _context.Balance.Remove(balance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BalanceExists(int id)
        {
          return _context.Balance.Any(e => e.Id == id);
        }
    }
}
