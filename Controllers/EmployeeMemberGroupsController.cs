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
    public class EmployeeMemberGroupsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public EmployeeMemberGroupsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: EmployeeMemberGroups
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<EmployeeMemberGroup> viewModel)
        {
            if (!Classes.Constants._loggedIn)
                return RedirectToAction("Login", "Users");
            var tkanicaWebAppContext = _context.EmployeeMemberGroup
                .Include(e => e.Employee)
                .Include(e => e.MemberGroup)
                .OrderBy(e => e.MemberGroupId);
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = sort == viewModel.CurrentSort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "employeeAsc" => await tkanicaWebAppContext.OrderBy(x => x.Employee.FirstName + " " + x.Employee.LastName).ToListAsync(),
                    "employeeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Employee.FirstName + " " + x.Employee.LastName).ToListAsync(),
                    "memberGroupAsc" => await tkanicaWebAppContext.OrderBy(x => x.MemberGroup.Name).ToListAsync(),
                    "memberGroupDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.MemberGroup.Name).ToListAsync(),
                    "createdAtAsc" => await tkanicaWebAppContext.OrderBy(x => x.CreatedAt).ToListAsync(),
                    "createdAtDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.CreatedAt).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.Employee.FirstName.ToLower().Contains(viewModel.Search) ||
                        x.Employee.LastName.ToLower().Contains(viewModel.Search) ||
                        x.MemberGroup.Name.ToLower().Contains(viewModel.Search))
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

        // GET: EmployeeMemberGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeMemberGroup == null)
            {
                return NotFound();
            }

            var employeeMemberGroup = await _context.EmployeeMemberGroup
                .Include(e => e.Employee)
                .Include(e => e.MemberGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeMemberGroup == null)
            {
                return NotFound();
            }

            return View(employeeMemberGroup);
        }

        // GET: EmployeeMemberGroups/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name");
            return View();
        }

        // POST: EmployeeMemberGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,MemberGroupId,CreatedAt,UpdatedAt")] EmployeeMemberGroup employeeMemberGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeMemberGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", employeeMemberGroup.EmployeeId);
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name", employeeMemberGroup.MemberGroupId);
            return View(employeeMemberGroup);
        }

        // GET: EmployeeMemberGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeMemberGroup == null)
            {
                return NotFound();
            }

            var employeeMemberGroup = await _context.EmployeeMemberGroup.FindAsync(id);
            if (employeeMemberGroup == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", employeeMemberGroup.EmployeeId);
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name", employeeMemberGroup.MemberGroupId);
            return View(employeeMemberGroup);
        }

        // POST: EmployeeMemberGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,MemberGroupId,CreatedAt,UpdatedAt")] EmployeeMemberGroup employeeMemberGroup)
        {
            if (id != employeeMemberGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeMemberGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeMemberGroupExists(employeeMemberGroup.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", employeeMemberGroup.EmployeeId);
            ViewData["MemberGroupId"] = new SelectList(_context.MemberGroup, "Id", "Name", employeeMemberGroup.MemberGroupId);
            return View(employeeMemberGroup);
        }

        // GET: EmployeeMemberGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeMemberGroup == null)
            {
                return NotFound();
            }

            var employeeMemberGroup = await _context.EmployeeMemberGroup
                .Include(e => e.Employee)
                .Include(e => e.MemberGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeMemberGroup == null)
            {
                return NotFound();
            }

            return View(employeeMemberGroup);
        }

        // POST: EmployeeMemberGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeMemberGroup == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.EmployeeMemberGroup'  is null.");
            }
            var employeeMemberGroup = await _context.EmployeeMemberGroup.FindAsync(id);
            if (employeeMemberGroup != null)
            {
                _context.EmployeeMemberGroup.Remove(employeeMemberGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeMemberGroupExists(int id)
        {
          return _context.EmployeeMemberGroup.Any(e => e.Id == id);
        }
    }
}
