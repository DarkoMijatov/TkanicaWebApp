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
    public class EmployeesController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public EmployeesController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<Employee> viewModel)
        {
            var tkanicaWebAppContext = _context.Employee
                  .Include(x => x.EmployeeMemberGroups)
                  .Include(x => x.EarningType)
                  .Include(x => x.PayPeriod)
                  .Include(x => x.RehearsalEmployees)
                  .Include("RehearsalEmployees.Rehearsal");
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = sort == viewModel.CurrentSort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "firstNameAsc" => await tkanicaWebAppContext.OrderBy(x => x.FirstName).ToListAsync(),
                    "firstNameDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.FirstName).ToListAsync(),
                    "lastNameAsc" => await tkanicaWebAppContext.OrderBy(x => x.LastName).ToListAsync(),
                    "lastNameDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.LastName).ToListAsync(),
                    "titleAsc" => await tkanicaWebAppContext.OrderBy(x => x.Title).ToListAsync(),
                    "titleDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Title).ToListAsync(),
                    "dateOfBirthAsc" => await tkanicaWebAppContext.OrderBy(x => x.DateOfBirth).ToListAsync(),
                    "dateOfBirthDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.DateOfBirth).ToListAsync(),
                    "startDateAsc" => await tkanicaWebAppContext.OrderBy(x => x.StartDate).ToListAsync(),
                    "startDateDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.StartDate).ToListAsync(),
                    "endDateAsc" => await tkanicaWebAppContext.OrderBy(x => x.EndDate).ToListAsync(),
                    "endDateDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.EndDate).ToListAsync(),
                    "activeAsc" => await tkanicaWebAppContext.OrderBy(x => x.Active).ToListAsync(),
                    "activeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Active).ToListAsync(),
                    "earningTypeAsc" => await tkanicaWebAppContext.OrderBy(x => x.EarningType.Name).ToListAsync(),
                    "earningTypeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.EarningType.Name).ToListAsync(),
                    "earningAmountAsc" => await tkanicaWebAppContext.OrderBy(x => x.EarningAmount).ToListAsync(),
                    "earningAmountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.EarningAmount).ToListAsync(),
                    "payPeriodAsc" => await tkanicaWebAppContext.OrderBy(x => x.PayPeriod).ToListAsync(),
                    "payPeriodDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.PayPeriod).ToListAsync(),
                    "otherExpensesAsc" => await tkanicaWebAppContext.OrderBy(x => x.OtherExpenses).ToListAsync(),
                    "otherExpensesDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.OtherExpenses).ToListAsync(),
                    "otherExpensesDescriptionAsc" => await tkanicaWebAppContext.OrderBy(x => x.OtherExpensesDescription).ToListAsync(),
                    "otherExpensesDescriptionDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.OtherExpensesDescription).ToListAsync(),
                    "employeeMemberGroupsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.EmployeeMemberGroups.Count).ToListAsync(),
                    "employeeMemberGroupsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.EmployeeMemberGroups.Count).ToListAsync(),
                    "rehearsalsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.RehearsalEmployees.Count(x => x.Rehearsal.Date.Year == DateTime.UtcNow.Year && x.Rehearsal.Date.Month == DateTime.UtcNow.Month)).ToListAsync(),
                    "rehearsalsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.RehearsalEmployees.Count(x => x.Rehearsal.Date.Year == DateTime.UtcNow.Year && x.Rehearsal.Date.Month == DateTime.UtcNow.Month)).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.FirstName.ToLower().Contains(viewModel.Search) ||
                        x.LastName.ToLower().Contains(viewModel.Search) ||
                        x.LastName.ToLower().Contains(viewModel.Search) ||
                        x.Title.ToLower().Contains(viewModel.Search) ||
                        x.EarningType.Name.ToLower().Contains(viewModel.Search) ||
                        x.PayPeriod.Name.ToLower().Contains(viewModel.Search) ||
                        (!string.IsNullOrEmpty(x.OtherExpensesDescription) && x.OtherExpensesDescription.ToLower().Contains(viewModel.Search)))
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

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(x => x.EarningType)
                .Include(x => x.PayPeriod)
                .Include(x => x.EmployeeMemberGroups)
                .Include("EmployeeMemberGroups.MemberGroup")
                .Include(x => x.RehearsalEmployees)
                .Include("RehearsalEmployees.Rehearsal")
                .Include("RehearsalEmployees.Rehearsal.RehearsalMembers")
                .Include("RehearsalEmployees.Rehearsal.RehearsalMembers.Member")
                .Include("RehearsalEmployees.Rehearsal.RehearsalMembers.Member.MembershipFee")
                .Include("RehearsalEmployees.Rehearsal.RehearsalMembers.Member.MembershipFee.MemberGroup")
                .Include(x => x.Transactions)
                .Include("Transactions.TransactionType")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["EarningTypeId"] = new SelectList(_context.EarningType, "Id", "Name");
            ViewData["PayPeriodId"] = new SelectList(_context.PayPeriod, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Title,DateOfBirth,StartDate,EndDate,Active,EarningTypeId,EarningAmount,PayPeriodId,OtherExpenses,OtherExpensesDescription,CreatedAt,UpdatedAt")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee
                .Include(x => x.EarningType)
                .Include(x => x.PayPeriod)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["EarningTypeId"] = new SelectList(_context.EarningType, "Id", "Name");
            ViewData["PayPeriodId"] = new SelectList(_context.PayPeriod, "Id", "Name");
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Title,DateOfBirth,StartDate,EndDate,Active,EarningTypeId,EarningAmount,PayPeriodId,OtherExpenses,OtherExpensesDescription,CreatedAt,UpdatedAt")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(x => x.EarningType)
                .Include(x => x.PayPeriod)
                .Include(x => x.EmployeeMemberGroups)
                .Include("EmployeeMemberGroups.MemberGroup")
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Employee'  is null.");
            }
            var employee = await _context.Employee
                .Include(x => x.EmployeeMemberGroups)
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return _context.Employee.Any(e => e.Id == id);
        }
    }
}
