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
    public class ReservationsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public ReservationsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<Reservation> viewModel)
        {
            var tkanicaWebAppContext = _context.Reservation
                .Include(r => r.Member)
                .Include(r => r.ClothingReservations);
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = viewModel.CurrentSort == sort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "idAsc" => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync(),
                    "idDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Id).ToListAsync(),
                    "dateAsc" => await tkanicaWebAppContext.OrderBy(x => x.Date).ToListAsync(),
                    "dateDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Date).ToListAsync(),
                    "memberAsc" => await tkanicaWebAppContext.OrderBy(x => x.Member.FirstName).OrderBy(x => x.Member.LastName).ToListAsync(),
                    "memberDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Member.FirstName).OrderByDescending(x => x.Member.LastName).ToListAsync(),
                    "activeAsc" => await tkanicaWebAppContext.OrderBy(x => x.Active).ToListAsync(),
                    "activeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Active).ToListAsync(),
                    "clothingsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.ClothingReservations.Count).ToListAsync(),
                    "clothingsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.ClothingReservations.Count).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderByDescending(x => x.Id).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderByDescending(x => x.Id).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.Member.FirstName.ToLower().Contains(viewModel.Search) ||
                        x.Member.LastName.ToLower().Contains(viewModel.Search))
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

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Active,MemberId,CreatedAt,UpdatedAt")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", reservation.MemberId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", reservation.MemberId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Active,MemberId,CreatedAt,UpdatedAt")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", reservation.MemberId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
          return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
