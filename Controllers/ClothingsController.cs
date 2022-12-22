using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Classes;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;
using TkanicaWebApp.ViewModels;

namespace TkanicaWebApp.Controllers
{
    public class ClothingsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public ClothingsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Clothings
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<Clothing> viewModel)
        {
            if (!Classes.Constants._loggedIn)
                return RedirectToAction("Login", "Users");
            var tkanicaWebAppContext = _context.Clothing
                .Include(c => c.ClothingRegion)
                .Include(c => c.ClothingType)
                .Include(c => c.ClothingReservations)
                .Include("ClothingReservations.Reservation");
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = sort == viewModel.CurrentSort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "idAsc" => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync(),
                    "idDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Id).ToListAsync(),
                    "typeAsc" => await tkanicaWebAppContext.OrderBy(x => x.ClothingType.Name).ToListAsync(),
                    "typeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.ClothingType.Name).ToListAsync(),
                    "regionAsc" => await tkanicaWebAppContext.OrderBy(x => x.ClothingRegion.Name).ToListAsync(),
                    "regionDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.ClothingRegion.Name).ToListAsync(),
                    "genderAsc" => await tkanicaWebAppContext.OrderBy(x => x.Gender).ToListAsync(),
                    "genderDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Gender).ToListAsync(),
                    "ageAsc" => await tkanicaWebAppContext.OrderBy(x => x.Age).ToListAsync(),
                    "ageDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Age).ToListAsync(),
                    "sizeAsc" => await tkanicaWebAppContext.OrderBy(x => x.Size).ToListAsync(),
                    "sizeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Size).ToListAsync(),
                    "reservedAsc" => await tkanicaWebAppContext.OrderBy(x => x.ClothingReservations.Count(x => x.Reservation.Active)).ToListAsync(),
                    "reservedDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.ClothingReservations.Count(x => x.Reservation.Active)).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync(),
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.ClothingRegion.Name.ToLower().Contains(viewModel.Search) ||
                        x.ClothingType.Name.ToLower().Contains(viewModel.Search) ||
                        (!string.IsNullOrEmpty(x.Size) && x.Size.ToLower().Contains(viewModel.Search)) ||
                        Constants.Gender.GetValueOrDefault(x.Gender).ToLower().Contains(viewModel.Search) ||
                        Constants.Age.GetValueOrDefault(x.Age).ToLower().Contains(viewModel.Search))
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

        // GET: Clothings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clothing == null)
            {
                return NotFound();
            }

            var clothing = await _context.Clothing
                .Include(c => c.ClothingRegion)
                .Include(c => c.ClothingType)
                .Include(c => c.ClothingReservations)
                .Include("ClothingReservations.Reservation")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothing == null)
            {
                return NotFound();
            }

            return View(clothing);
        }

        // GET: Clothings/Create
        public IActionResult Create()
        {
            ViewData["ClothingRegionId"] = new SelectList(_context.ClothingRegion, "Id", "Name");
            ViewData["ClothingTypeId"] = new SelectList(_context.ClothingType, "Id", "Name");
            ViewData["Gender"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Gender), "Id", "Name");
            ViewData["Age"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Age), "Id", "Name");
            return View();
        }

        // POST: Clothings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClothingTypeId,ClothingRegionId,Gender,Age,Size,Image")] Clothing clothing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClothingRegionId"] = new SelectList(_context.ClothingRegion, "Id", "Name", clothing.ClothingRegionId);
            ViewData["ClothingTypeId"] = new SelectList(_context.ClothingType, "Id", "Name", clothing.ClothingTypeId);
            ViewData["Gender"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Gender), "Id", "Name");
            ViewData["Age"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Age), "Id", "Name");
            return View(clothing);
        }

        // GET: Clothings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clothing == null)
            {
                return NotFound();
            }

            var clothing = await _context.Clothing.FindAsync(id);
            if (clothing == null)
            {
                return NotFound();
            }
            ViewData["ClothingRegionId"] = new SelectList(_context.ClothingRegion, "Id", "Name", clothing.ClothingRegionId);
            ViewData["ClothingTypeId"] = new SelectList(_context.ClothingType, "Id", "Name", clothing.ClothingTypeId);
            ViewData["Gender"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Gender), "Id", "Name", clothing.Gender);
            ViewData["Age"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Age), "Id", "Name", clothing.Age);
            return View(clothing);
        }

        // POST: Clothings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClothingTypeId,ClothingRegionId,Gender,Age,Size,Image")] Clothing clothing)
        {
            if (id != clothing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingExists(clothing.Id))
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
            ViewData["ClothingRegionId"] = new SelectList(_context.ClothingRegion, "Id", "Name", clothing.ClothingRegionId);
            ViewData["ClothingTypeId"] = new SelectList(_context.ClothingType, "Id", "Name", clothing.ClothingTypeId);
            ViewData["Gender"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Gender), "Id", "Name", clothing.Gender);
            ViewData["Age"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Age), "Id", "Name", clothing.Age);
            return View(clothing);
        }

        // GET: Clothings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clothing == null)
            {
                return NotFound();
            }

            var clothing = await _context.Clothing
                .Include(c => c.ClothingRegion)
                .Include(c => c.ClothingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothing == null)
            {
                return NotFound();
            }

            return View(clothing);
        }

        // POST: Clothings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clothing == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Clothing'  is null.");
            }
            var clothing = await _context.Clothing.FindAsync(id);
            if (clothing != null)
            {
                _context.Clothing.Remove(clothing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingExists(int id)
        {
          return _context.Clothing.Any(e => e.Id == id);
        }
    }
}
