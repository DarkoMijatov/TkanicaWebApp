using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Controllers
{
    public class AccountNumbersController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public AccountNumbersController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: AccountNumbers
        public async Task<IActionResult> Index()
        {
            var tkanicaWebAppContext = _context.AccountNumber
                .Include(a => a.Creditor)
                .Include("Creditor.Transactions")
                .Include(a => a.Debtor)
                .Include("Debtor.Transactions")
                .Include(a => a.Balance)
                .Include("Balance.Transactions");
            return View(await tkanicaWebAppContext.ToListAsync());
        }

        // GET: AccountNumbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AccountNumber == null)
            {
                return NotFound();
            }

            var accountNumber = await _context.AccountNumber
                .Include(a => a.Creditor)
                .Include("Creditor.Transactions")
                .Include(a => a.Debtor)
                .Include("Debtor.Transactions")
                .Include(a => a.Balance)
                .Include("Balance.Transactions")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountNumber == null)
            {
                return NotFound();
            }

            return View(accountNumber);
        }

        // GET: AccountNumbers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountNumbers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BankAccountNumber,Bank")] AccountNumber accountNumber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountNumber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountNumber);
        }

        // GET: AccountNumbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AccountNumber == null)
            {
                return NotFound();
            }

            var accountNumber = await _context.AccountNumber
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountNumber == null)
            {
                return NotFound();
            }
            return View(accountNumber);
        }

        // POST: AccountNumbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BankAccountNumber,Bank")] AccountNumber accountNumber)
        {
            if (id != accountNumber.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountNumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountNumberExists(accountNumber.Id))
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
            return View(accountNumber);
        }

        // GET: AccountNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AccountNumber == null)
            {
                return NotFound();
            }

            var accountNumber = await _context.AccountNumber
                .Include(a => a.Creditor)
                .Include("Creditor.Transactions")
                .Include(a => a.Debtor)
                .Include("Debtor.Transactions")
                .Include(a => a.Balance)
                .Include("Balance.Transactions")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountNumber == null)
            {
                return NotFound();
            }

            return View(accountNumber);
        }

        // POST: AccountNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AccountNumber == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.AccountNumber'  is null.");
            }
            var accountNumber = await _context.AccountNumber
                .Include(a => a.Creditor)
                .Include("Creditor.Transactions")
                .Include(a => a.Debtor)
                .Include("Debtor.Transactions")
                .Include(a => a.Balance)
                .Include("Balance.Transactions")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountNumber != null)
            {
                _context.AccountNumber.Remove(accountNumber);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountNumberExists(int id)
        {
          return _context.AccountNumber.Any(e => e.Id == id);
        }
    }
}
