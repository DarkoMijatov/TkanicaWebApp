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
    public class TransactionsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public TransactionsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<Transaction> viewModel)
        {
            var tkanicaWebAppContext = _context.Transaction
                .Include(t => t.Balance)
                .Include(t => t.Creditor)
                .Include(t => t.Debtor)
                .Include(t => t.Employee)
                .Include(t => t.Member)
                .Include(t => t.TransactionType);
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = viewModel.CurrentSort == sort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "transactionNumberAsc" => await tkanicaWebAppContext.OrderBy(x => x.TransactionNumber).ToListAsync(),
                    "transactionNumberDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.TransactionNumber).ToListAsync(),
                    "transactionTypeAsc" => await tkanicaWebAppContext.OrderBy(x => x.TransactionType.Name).ToListAsync(),
                    "transactionTypeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.TransactionType.Name).ToListAsync(),
                    "balanceAsc" => await tkanicaWebAppContext.OrderBy(x => x.Balance.Name).ToListAsync(),
                    "balanceDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Balance.Name).ToListAsync(),
                    "debtorAsc" => await tkanicaWebAppContext.OrderBy(x => x.DebtorId != null ? x.Debtor.Name : x.MemberId != null ? x.Member.FirstName + " " + x.Member.LastName : @"KUD ""Pargar""").ToListAsync(),
                    "debtorDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.DebtorId != null ? x.Debtor.Name : x.MemberId != null ? x.Member.FirstName + " " + x.Member.LastName : @"KUD ""Pargar""").ToListAsync(),
                    "creditorAsc" => await tkanicaWebAppContext.OrderBy(x => x.CreditorId != null ? x.Creditor.Name : x.EmployeeId != null ? x.Employee.FirstName + " " + x.Employee.LastName : @"KUD ""Pargar""").ToListAsync(),
                    "creditorDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.CreditorId != null ? x.Creditor.Name : x.EmployeeId != null ? x.Employee.FirstName + " " + x.Employee.LastName : @"KUD ""Pargar""").ToListAsync(),
                    "referenceNumberAsc" => await tkanicaWebAppContext.OrderBy(x => x.ReferenceNumber).ToListAsync(),
                    "referenceNumberDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.ReferenceNumber).ToListAsync(),
                    "amountAsc" => await tkanicaWebAppContext.OrderBy(x => x.Amount).ToListAsync(),
                    "amountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Amount).ToListAsync(),
                    "descriptionAsc" => await tkanicaWebAppContext.OrderBy(x => x.Description).ToListAsync(),
                    "descriptionDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Description).ToListAsync(),
                    "isPaidAsc" => await tkanicaWebAppContext.OrderBy(x => x.Paid).ToListAsync(),
                    "isPaidDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Paid).ToListAsync(),
                    "transactionDateAsc" => await tkanicaWebAppContext.OrderBy(x => x.TransactionDate).ToListAsync(),
                    "transactionDateDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.TransactionDate).ToListAsync(),
                    "paidDateAsc" => await tkanicaWebAppContext.OrderBy(x => x.PaidDate).ToListAsync(),
                    "paidDateDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.PaidDate).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderByDescending(x => x.TransactionDate).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderByDescending(x => x.TransactionDate).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.TransactionDate.ToString().Contains(viewModel.Search) ||
                        (x.Paid && x.PaidDate.ToString().Contains(viewModel.Search)) ||
                        (x.CreditorId != null && x.Creditor.Name.ToLower().Contains(viewModel.Search)) ||
                        (x.DebtorId != null && x.Debtor.Name.ToLower().Contains(viewModel.Search)) ||
                        x.Balance.Name.ToLower().Contains(viewModel.Search) ||
                        (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(viewModel.Search)) ||
                        (!string.IsNullOrEmpty(x.TransactionNumber) && x.TransactionNumber.ToLower().Contains(viewModel.Search)) ||
                        x.TransactionType.Name.ToLower().Contains(viewModel.Search) ||
                        x.Amount.ToString().Contains(viewModel.Search) ||
                        (x.MemberId != null && (x.Member.FirstName.ToLower().Contains(viewModel.Search) || x.Member.LastName.ToLower().Contains(viewModel.Search))) ||
                        (x.EmployeeId != null && (x.Employee.FirstName.ToLower().Contains(viewModel.Search) || x.Employee.LastName.ToLower().Contains(viewModel.Search))))
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

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Balance)
                .Include(t => t.Balance.AccountNumber)
                .Include(t => t.Creditor)
                .Include(t => t.Creditor.AccountNumbers)
                .Include(t => t.Debtor)
                .Include(t => t.Debtor.AccountNumbers)
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
        public async Task<IActionResult> Create()
        {
            ViewData["BalanceId"] = new SelectList(_context.Balance, "Id", "Name", null);
            ViewData["CreditorId"] = new SelectList(_context.Client, "Id", "Name", null);
            ViewData["DebtorId"] = new SelectList(_context.Client, "Id", "Name", null);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", null);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", null);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Name", null);
            ViewData["TransactionNumber"] = $"{await _context.Transaction.CountAsync(x => x.Paid && x.TransactionDate.Year == DateTime.UtcNow.Year) + 1}/{DateTime.UtcNow.Year}";
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransactionNumber,TransactionTypeId,DebtorId,CreditorId,ReferenceNumber,Amount,Description,Paid,TransactionDate,PaidDate,MemberId,EmployeeId,BalanceId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.TransactionTypeId == 1)
                {
                    transaction.DebtorId = null;
                    transaction.CreditorId = null;
                    transaction.EmployeeId = null;
                    var month = transaction.TransactionDate.Month switch
                    {
                        1 => "januar",
                        2 => "februar",
                        3 => "mart",
                        4 => "april",
                        5 => "maj",
                        6 => "jun",
                        7 => "jul",
                        8 => "avgust",
                        9 => "septembar",
                        10 => "oktobar",
                        11 => "novembar",
                        _ => "decembar"
                    };
                    transaction.Description = $"{month} {transaction.TransactionDate.Year}.";
                }
                else if (transaction.TransactionTypeId == 2)
                {
                    transaction.DebtorId = null;
                    transaction.CreditorId = null;
                    transaction.MemberId = null;
                    transaction.Description = $"{transaction.TransactionDate.Month}.{transaction.TransactionDate.Year}.";
                }
                else if (transaction.TransactionTypeId == 3 && transaction.BalanceId == 1)
                {
                    transaction.DebtorId = null;
                    transaction.CreditorId = null;
                    transaction.EmployeeId = null;
                }
                else if (transaction.TransactionTypeId == 3 && transaction.BalanceId != 1)
                {
                    transaction.EmployeeId = null;
                    transaction.CreditorId = null;
                    transaction.MemberId = null;
                }
                else if (transaction.TransactionTypeId == 4)
                {
                    transaction.DebtorId = null;
                    transaction.CreditorId = null;
                    transaction.EmployeeId = null;
                }
                else if (transaction.TransactionTypeId == 5)
                {
                    transaction.DebtorId = null;
                    transaction.CreditorId = null;
                    transaction.MemberId = null;
                }
                else if (transaction.TransactionTypeId == 6)
                {
                    transaction.EmployeeId = null;
                    transaction.CreditorId = null;
                    transaction.MemberId = null;
                }
                else
                {
                    transaction.EmployeeId = null;
                    transaction.DebtorId = null;
                    transaction.MemberId = null;
                }
                if (!transaction.Paid)
                {
                    transaction.TransactionNumber = "";
                }
                else
                {
                    if (transaction.PaidDate == null)
                        transaction.PaidDate = DateTime.UtcNow;
                }
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BalanceId"] = new SelectList(_context.Balance, "Id", "Name", null);
            ViewData["CreditorId"] = new SelectList(_context.Client, "Id", "Name", null);
            ViewData["DebtorId"] = new SelectList(_context.Client, "Id", "Name", null);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", null);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", null);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Name", null);
            ViewData["TransactionNumber"] = $"{await _context.Transaction.CountAsync(x => x.Paid && x.TransactionDate.Year == DateTime.UtcNow.Year) + 1}/{DateTime.UtcNow.Year}";
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Balance)
                .Include(t => t.Balance.AccountNumber)
                .Include(t => t.Creditor)
                .Include(t => t.Creditor.AccountNumbers)
                .Include(t => t.Debtor)
                .Include(t => t.Debtor.AccountNumbers)
                .Include(t => t.Employee)
                .Include(t => t.Member)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["BalanceId"] = new SelectList(_context.Balance, "Id", "Name", transaction.BalanceId);
            ViewData["CreditorId"] = new SelectList(_context.Client, "Id", "Name", transaction.CreditorId);
            ViewData["DebtorId"] = new SelectList(_context.Client, "Id", "Name", transaction.DebtorId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", transaction.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", transaction.MemberId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Name", transaction.TransactionTypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransactionNumber,TransactionTypeId,DebtorId,CreditorId,ReferenceNumber,Amount,Description,Paid,TransactionDate,PaidDate,MemberId,EmployeeId,BalanceId")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (transaction.TransactionTypeId == 1)
                    {
                        transaction.DebtorId = null;
                        transaction.CreditorId = null;
                        transaction.EmployeeId = null;
                    }
                    else if (transaction.TransactionTypeId == 2)
                    {
                        transaction.DebtorId = null;
                        transaction.CreditorId = null;
                        transaction.MemberId = null;
                    }
                    else if (transaction.TransactionTypeId == 3 && transaction.BalanceId == 1)
                    {
                        transaction.DebtorId = null;
                        transaction.CreditorId = null;
                        transaction.EmployeeId = null;
                    }
                    else if (transaction.TransactionTypeId == 3 && transaction.BalanceId != 1)
                    {
                        transaction.EmployeeId = null;
                        transaction.CreditorId = null;
                        transaction.MemberId = null;
                    }
                    else if (transaction.TransactionTypeId == 4)
                    {
                        transaction.DebtorId = null;
                        transaction.CreditorId = null;
                        transaction.EmployeeId = null;
                    }
                    else if (transaction.TransactionTypeId == 5)
                    {
                        transaction.DebtorId = null;
                        transaction.CreditorId = null;
                        transaction.MemberId = null;
                    }
                    else if (transaction.TransactionTypeId == 6)
                    {
                        transaction.EmployeeId = null;
                        transaction.CreditorId = null;
                        transaction.MemberId = null;
                    }
                    else
                    {
                        transaction.EmployeeId = null;
                        transaction.DebtorId = null;
                        transaction.MemberId = null;
                    }
                    if (transaction.Paid && string.IsNullOrEmpty(transaction.TransactionNumber))
                    {
                        if (transaction.PaidDate == null)
                            transaction.PaidDate = DateTime.UtcNow;
                        transaction.TransactionNumber = $"{await _context.Transaction.CountAsync(x => x.Paid && x.TransactionDate.Year == DateTime.UtcNow.Year) + 1}/{DateTime.UtcNow.Year}";
                    }
                    if (!transaction.Paid)
                    {
                        transaction.TransactionNumber = "";
                        transaction.PaidDate = null;
                    }
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
            ViewData["BalanceId"] = new SelectList(_context.Balance, "Id", "Name", transaction.BalanceId);
            ViewData["CreditorId"] = new SelectList(_context.Client, "Id", "Name", transaction.CreditorId);
            ViewData["DebtorId"] = new SelectList(_context.Client, "Id", "Name", transaction.DebtorId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", transaction.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", transaction.MemberId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Name", transaction.TransactionTypeId);
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
                .Include(t => t.Balance.AccountNumber)
                .Include(t => t.Creditor)
                .Include(t => t.Creditor.AccountNumbers)
                .Include(t => t.Debtor)
                .Include(t => t.Debtor.AccountNumbers)
                .Include(t => t.Employee)
                .Include(t => t.Member)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(x => x.Id == id);
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
