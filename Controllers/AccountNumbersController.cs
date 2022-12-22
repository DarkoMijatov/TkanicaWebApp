using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;
using TkanicaWebApp.ViewModels;

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
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<AccountNumber> viewModel)
        {
            if (!Classes.Constants._loggedIn)
                return RedirectToAction("Login", "Users");
            var tkanicaWebAppContext = _context.AccountNumber
                .Include(a => a.Client)
                .Include("Client.CreditorTransactions")
                .Include("Client.DebtorTransactions")
                .Include(a => a.Balance)
                .Include("Balance.Transactions")
                .Include("Balance.Transactions.TransactionType");
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = sort == viewModel.CurrentSort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "accountNumberAsc" => await tkanicaWebAppContext.OrderBy(x => x.BankAccountNumber).ToListAsync(),
                    "accountNumberDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.BankAccountNumber).ToListAsync(),
                    "bankAsc" => await tkanicaWebAppContext.OrderBy(x => x.Bank).ToListAsync(),
                    "bankDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Bank).ToListAsync(),
                    "ownerAsc" => await tkanicaWebAppContext.OrderBy(x => x.ClientId != null ? x.Client.Name : x.BalanceId != null ? x.Balance.Name : "Nema vlasnika").ToListAsync(),
                    "ownerDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.ClientId != null ? x.Client.Name : x.BalanceId != null ? x.Balance.Name : "Nema vlasnika").ToListAsync(),
                    "incomingTransactionsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.ClientId != null ? x.Client.CreditorTransactions.Count : x.BalanceId != null ? x.Balance.Transactions.Count(x => x.TransactionType.Direction == 1) : 0).ToListAsync(),
                    "incomingTransactionsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.ClientId != null ? x.Client.CreditorTransactions.Count : x.BalanceId != null ? x.Balance.Transactions.Count(x => x.TransactionType.Direction == 1) : 0).ToListAsync(),
                    "outgoingTransactionsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.ClientId != null ? x.Client.DebtorTransactions.Count : x.BalanceId != null ? x.Balance.Transactions.Count(x => x.TransactionType.Direction == -1) : 0).ToListAsync(),
                    "outgoingTransactionsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.ClientId != null ? x.Client.DebtorTransactions.Count : x.BalanceId != null ? x.Balance.Transactions.Count(x => x.TransactionType.Direction == -1) : 0).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.BankAccountNumber.ToLower().Contains(viewModel.Search) ||
                        x.Bank.ToLower().Contains(viewModel.Search) ||
                        (x.Balance != null && x.Balance.Name.ToLower().Contains(viewModel.Search)) ||
                        (x.ClientId != null && x.Client.Name.ToLower().Contains(viewModel.Search)))
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

        // GET: AccountNumbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AccountNumber == null)
            {
                return NotFound();
            }

            var accountNumber = await _context.AccountNumber
                .Include(a => a.Client)
                .Include("Client.CreditorTransactions")
                .Include("Client.DebtorTransactions")
                .Include(a => a.Balance)
                .Include("Balance.Transactions")
                .Include("Balance.Transactions.TransactionType")
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
                .Include(a => a.Client)
                .Include("Client.CreditorTransactions")
                .Include("Client.DebtorTransactions")
                .Include(a => a.Balance)
                .Include("Balance.Transactions")
                .Include("Balance.Transactions.TransactionType")
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
                .Include(a => a.Client)
                .Include("Client.CreditorTransactions")
                .Include("Client.DebtorTransactions")
                .Include(a => a.Balance)
                .Include("Balance.Transactions")
                .Include("Balance.Transactions.TransactionType")
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
