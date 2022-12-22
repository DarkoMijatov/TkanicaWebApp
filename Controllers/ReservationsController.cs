using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Extensions;
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
            if (!Classes.Constants._loggedIn)
                return RedirectToAction("Login", "Users");
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
                .Include(r => r.ClothingReservations)
                .Include("ClothingReservations.Clothing")
                .Include("ClothingReservations.Clothing.ClothingType")
                .Include("ClothingReservations.Clothing.ClothingRegion")
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
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");
            ViewData["ClothingId"] = new SelectList(_context.Clothing
                .Include(x => x.ClothingType)
                .Include(x => x.ClothingRegion)
                .Include(x => x.ClothingReservations)
                .Include("ClothingReservations.Reservation")
                .Where(x => !x.ClothingReservations.Any(c => c.Reservation.Active)), "Id", "ClothingText");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,MemberId,ClothingIds")] ReservationViewModel reservationViewModel)
        {
            var reservation = new Reservation();
            if (ModelState.IsValid)
            {
                reservation.Date = reservationViewModel.Date;
                reservation.MemberId = reservationViewModel.MemberId;
                reservation.Active = true;
                reservation.ClothingReservations = new List<ClothingReservation>();
                foreach (int clothingId in reservationViewModel.ClothingIds)
                {
                    reservation.ClothingReservations.Add(new ClothingReservation
                    {
                        ClothingId = clothingId,
                        Reservation = reservation
                    });
                }
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");
            ViewData["ClothingId"] = new SelectList(_context.Clothing
                .Include(x => x.ClothingType)
                .Include(x => x.ClothingRegion)
                .Include(x => x.ClothingReservations)
                .Include("ClothingReservations.Reservation")
                .Where(x => !x.ClothingReservations.Any(c => c.Reservation.Active)), "Id", "ClothingText");
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Member)
                .Include(r => r.ClothingReservations)
                .Include("ClothingReservations.Clothing")
                .Include("ClothingReservations.Clothing.ClothingType")
                .Include("ClothingReservations.Clothing.ClothingRegion")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            var reservationViewModel = new ReservationViewModel
            {
                Id = reservation.Id,
                Date = reservation.Date,
                Active = reservation.Active,
                MemberId = reservation.MemberId,
                ClothingIds = reservation.ClothingReservations.Select(x => x.ClothingId).ToList()
            };
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");
            ViewData["ClothingId"] = new SelectList(_context.Clothing
                .Include(x => x.ClothingType)
                .Include(x => x.ClothingRegion)
                .Include(x => x.ClothingReservations)
                .Include("ClothingReservations.Reservation")
                .Where(x => x.ClothingReservations.Select(x => x.ReservationId).Contains(reservation.Id)), "Id", "ClothingText", reservation.ClothingReservations.Select(x => x.ClothingId))
                .Concat(
                    new SelectList(_context.Clothing
                    .Include(x => x.ClothingType)
                    .Include(x => x.ClothingRegion)
                    .Include(x => x.ClothingReservations)
                    .Include("ClothingReservations.Reservation")
                    .Where(x => !x.ClothingReservations.Select(x => x.ReservationId).Contains(reservation.Id)), "Id", "ClothingText", reservation.ClothingReservations.Select(x => x.ClothingId))
                );
            return View(reservationViewModel);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,MemberId,ClothingIds")] ReservationViewModel reservationViewModel)
        {
            var reservation = await _context.Reservation
                .Include(r => r.Member)
                .Include(r => r.ClothingReservations)
                .Include("ClothingReservations.Clothing")
                .Include("ClothingReservations.Clothing.ClothingType")
                .Include("ClothingReservations.Clothing.ClothingRegion")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (id != reservationViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    reservation.Date = reservationViewModel.Date;
                    reservation.MemberId = reservationViewModel.MemberId;
                    reservation.Active = reservationViewModel.Active;
                    _context.ClothingReservation.RemoveRange(reservation.ClothingReservations);
                    foreach (int clothingId in reservationViewModel.ClothingIds)
                    {
                        reservation.ClothingReservations.Add(new ClothingReservation
                        {
                            ClothingId = clothingId,
                            Reservation = reservation
                        });
                    }
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
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");
            ViewData["ClothingId"] = new SelectList(_context.Clothing
                .Include(x => x.ClothingType)
                .Include(x => x.ClothingRegion)
                .Include(x => x.ClothingReservations)
                .Include("ClothingReservations.Reservation")
                .Where(x => x.ClothingReservations.Select(x => x.ReservationId).Contains(reservation.Id)), "Id", "ClothingText", reservation.ClothingReservations.Select(x => x.ClothingId))
                .Concat(
                    new SelectList(_context.Clothing
                    .Include(x => x.ClothingType)
                    .Include(x => x.ClothingRegion)
                    .Include(x => x.ClothingReservations)
                    .Include("ClothingReservations.Reservation")
                    .Where(x => !x.ClothingReservations.Select(x => x.ReservationId).Contains(reservation.Id)), "Id", "ClothingText", reservation.ClothingReservations.Select(x => x.ClothingId))
                );
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
                .Include(r => r.ClothingReservations)
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
            var reservation = await _context.Reservation
                .Include(r => r.ClothingReservations)
                .FirstOrDefaultAsync(m => m.Id == id);
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
