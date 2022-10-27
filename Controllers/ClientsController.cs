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
    public class ClientsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public ClientsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string sort, PageViewModel<Client> viewModel)
        {
            var tkanicaWebAppContext = _context.Client
              .Include(x => x.AccountNumbers)
              .Include(x => x.CreditorTransactions)
              .Include(x => x.DebtorTransactions);
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = viewModel.CurrentSort == sort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "nameAsc" => await tkanicaWebAppContext.OrderBy(x => x.Name).ToListAsync(),
                    "nameDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Name).ToListAsync(),
                    "isCompanyAsc" => await tkanicaWebAppContext.OrderBy(x => x.IsCompany).ToListAsync(),
                    "isCompanyDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.IsCompany).ToListAsync(),
                    "addressAsc" => await tkanicaWebAppContext.OrderBy(x => x.Address).ToListAsync(),
                    "addressDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Address).ToListAsync(),
                    "cityAsc" => await tkanicaWebAppContext.OrderBy(x => x.City).ToListAsync(),
                    "cityDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.City).ToListAsync(),
                    "phoneAsc" => await tkanicaWebAppContext.OrderBy(x => x.PhoneNumber).ToListAsync(),
                    "phoneDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.PhoneNumber).ToListAsync(),
                    "emailAsc" => await tkanicaWebAppContext.OrderBy(x => x.Email).ToListAsync(),
                    "emailDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Email).ToListAsync(),
                    "websiteAsc" => await tkanicaWebAppContext.OrderBy(x => x.Website).ToListAsync(),
                    "websiteDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Website).ToListAsync(),
                    "idNumberAsc" => await tkanicaWebAppContext.OrderBy(x => x.IdNumber).ToListAsync(),
                    "idNumberDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.IdNumber).ToListAsync(),
                    "taxNumberAsc" => await tkanicaWebAppContext.OrderBy(x => x.TaxNumber).ToListAsync(),
                    "taxNumberDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.TaxNumber).ToListAsync(),
                    "accountNumbersCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.AccountNumbers.Count).ToListAsync(),
                    "accountNumbersCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.AccountNumbers.Count).ToListAsync(),
                    "incomingTransactionsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.CreditorTransactions.Count).ToListAsync(),
                    "incomingTransactionsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.CreditorTransactions.Count).ToListAsync(),
                    "outgoingTransactionsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.DebtorTransactions.Count).ToListAsync(),
                    "outgoingTransactionsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.DebtorTransactions.Count).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync();
            return View(viewModel);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(x => x.AccountNumbers)
                .Include(x => x.CreditorTransactions)
                .Include(x => x.DebtorTransactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["AccountNumberIds"] = new SelectList(_context.AccountNumber
                .Where(x => x.BalanceId == null && x.ClientId == null), "Id", "BankAccountNumber");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsCompany,Address,City,PhoneNumber,Email,Website,IdNumber,TaxNumber,Logo,AccountNumberIds")] ClientViewModel clientViewModel)
        {
            var client = new Client();
            if (ModelState.IsValid)
            {
                client.Name = clientViewModel.Name;
                client.IsCompany = clientViewModel.IsCompany;
                client.Address = clientViewModel.Address;
                client.City = clientViewModel.City;
                client.PhoneNumber = clientViewModel.PhoneNumber;
                client.Email = clientViewModel.Email;
                client.Website = clientViewModel.Website;
                client.IdNumber = clientViewModel.IdNumber;
                client.TaxNumber = clientViewModel.TaxNumber;
                client.Logo = clientViewModel.Logo;
                client.AccountNumbers = new List<AccountNumber>();
                foreach(var accountNumberId in  clientViewModel.AccountNumberIds)
                {
                    var accountNumber = await _context.AccountNumber.FirstAsync(m => m.Id == accountNumberId);
                    accountNumber.Client = client;
                    client.AccountNumbers.Add(accountNumber);
                }
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(x => x.AccountNumbers)
                .Include(x => x.CreditorTransactions)
                .Include(x => x.DebtorTransactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            var clientViewModel = new ClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                IsCompany = client.IsCompany,
                Address = client.Address,
                City = client.City,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                Website = client.Website,
                IdNumber = client.IdNumber,
                TaxNumber = client.TaxNumber,
                Logo = client.Logo,
                AccountNumberIds = client.AccountNumbers.Select(x => x.Id).ToList()
            };
            ViewData["AccountNumberIds"] = new SelectList(_context.AccountNumber
                .Where(x => x.BalanceId == null || x.ClientId == client.Id), "Id", "BankAccountNumber", client.AccountNumbers.Select(x => x.Id));
            return View(clientViewModel);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsCompany,Address,City,PhoneNumber,Email,Website,IdNumber,TaxNumber,Logo,AccountNumberIds")] ClientViewModel clientViewModel)
        {
            var client = await _context.Client
                .Include(x => x.AccountNumbers)
                .Include(x => x.CreditorTransactions)
                .Include(x => x.DebtorTransactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.Name = clientViewModel.Name;
                    client.IsCompany = clientViewModel.IsCompany;
                    client.Address = clientViewModel.Address;
                    client.City = clientViewModel.City;
                    client.PhoneNumber = clientViewModel.PhoneNumber;
                    client.Email = clientViewModel.Email;
                    client.Website = clientViewModel.Website;
                    client.IdNumber = clientViewModel.IdNumber;
                    client.TaxNumber = clientViewModel.TaxNumber;
                    client.Logo = clientViewModel.Logo;
                    client.AccountNumbers = new List<AccountNumber>();
                    foreach(var accountNumberId in clientViewModel.AccountNumberIds)
                    {
                        var accountNumber = await _context.AccountNumber.FirstAsync(m => m.Id == accountNumberId);
                        accountNumber.Client = client;
                        client.AccountNumbers.Add(accountNumber);
                    }
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(x => x.AccountNumbers)
                .Include(x => x.CreditorTransactions)
                .Include(x => x.DebtorTransactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Client'  is null.");
            }
            var client = await _context.Client
                .Include(x => x.AccountNumbers)
                .Include(x => x.CreditorTransactions)
                .Include(x => x.DebtorTransactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return _context.Client.Any(e => e.Id == id);
        }
    }
}
