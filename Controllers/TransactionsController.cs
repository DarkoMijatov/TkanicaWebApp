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
    public class TransactionsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public TransactionsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var tkanicaWebAppContext = _context.Transaction.Include(t => t.Balance).Include(t => t.Creditor).Include(t => t.Debtor).Include(t => t.Employee).Include(t => t.Member).Include(t => t.TransactionType);
            return View(await tkanicaWebAppContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Balance)
                .Include(t => t.Creditor)
                .Include(t => t.Debtor)
                .Include(t => t.Employee)
                .Include(t => t.Member)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["BalanceId"] = new SelectList(_context.Balance, "Id", "Id");
            ViewData["CreditorId"] = new SelectList(_context.Creditor, "Id", "Id");
            ViewData["DebtorId"] = new SelectList(_context.Debtor, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id");
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id");
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Id");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransactionNumber,TransactionTypeId,DebtorId,CreditorId,ReferenceNumber,Amount,Description,Paid,TransactionDate,PaidDate,MemberId,EmployeeId,BalanceId,CreatedAt,UpdatedAt")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BalanceId"] = new SelectList(_context.Balance, "Id", "Id", transaction.BalanceId);
            ViewData["CreditorId"] = new SelectList(_context.Creditor, "Id", "Id", transaction.CreditorId);
            ViewData["DebtorId"] = new SelectList(_context.Debtor, "Id", "Id", transaction.DebtorId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", transaction.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", transaction.MemberId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Id", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["BalanceId"] = new SelectList(_context.Balance, "Id", "Id", transaction.BalanceId);
            ViewData["CreditorId"] = new SelectList(_context.Creditor, "Id", "Id", transaction.CreditorId);
            ViewData["DebtorId"] = new SelectList(_context.Debtor, "Id", "Id", transaction.DebtorId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", transaction.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", transaction.MemberId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Id", transaction.TransactionTypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransactionNumber,TransactionTypeId,DebtorId,CreditorId,ReferenceNumber,Amount,Description,Paid,TransactionDate,PaidDate,MemberId,EmployeeId,BalanceId,CreatedAt,UpdatedAt")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            ViewData["BalanceId"] = new SelectList(_context.Balance, "Id", "Id", transaction.BalanceId);
            ViewData["CreditorId"] = new SelectList(_context.Creditor, "Id", "Id", transaction.CreditorId);
            ViewData["DebtorId"] = new SelectList(_context.Debtor, "Id", "Id", transaction.DebtorId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", transaction.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", transaction.MemberId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Id", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Balance)
                .Include(t => t.Creditor)
                .Include(t => t.Debtor)
                .Include(t => t.Employee)
                .Include(t => t.Member)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transaction == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Transaction'  is null.");
            }
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return _context.Transaction.Any(e => e.Id == id);
        }
    }
}
