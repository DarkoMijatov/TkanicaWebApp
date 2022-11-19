using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Classes;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;
using TkanicaWebApp.ViewModels;

namespace TkanicaWebApp.Controllers
{
    public class ClothingRegionsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public ClothingRegionsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: ClothingRegions
        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<ClothingRegion> viewModel)
        {
            var tkanicaWebAppContext = _context.ClothingRegion
                .Include(x => x.Clothings)
                .Include("Clothings.ClothingType");
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = sort == viewModel.CurrentSort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "nameAsc" => await tkanicaWebAppContext.OrderBy(x => x.Name).ToListAsync(),
                    "nameDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Name).ToListAsync(),
                    "areaAsc" => await tkanicaWebAppContext.OrderBy(x => x.AreaId).ToListAsync(),
                    "areaDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.AreaId).ToListAsync(),
                    "clothingsCountAsc" => await tkanicaWebAppContext.OrderBy(x => x.Clothings.Count).ToListAsync(),
                    "clothingsCountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Clothings.Count).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync(),
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                viewModel.Search = search.ToLower();
                viewModel.List = viewModel.List.
                    Where(x => x.Name.ToLower().Contains(viewModel.Search) ||
                        Constants.Areas.GetValueOrDefault(x.AreaId).ToLower().Contains(viewModel.Search))
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

        // GET: ClothingRegions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClothingRegion == null)
            {
                return NotFound();
            }

            var clothingRegion = await _context.ClothingRegion
                .Include(x => x.Clothings)
                .Include("Clothings.ClothingType")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingRegion == null)
            {
                return NotFound();
            }

            return View(clothingRegion);
        }

        // GET: ClothingRegions/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Areas), "Id", "Name");
            return View();
        }

        // POST: ClothingRegions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AreaId")] ClothingRegion clothingRegion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothingRegion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothingRegion);
        }

        // GET: ClothingRegions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClothingRegion == null)
            {
                return NotFound();
            }

            var clothingRegion = await _context.ClothingRegion
               .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingRegion == null)
            {
                return NotFound();
            }

            ViewData["AreaId"] = new SelectList(DictionaryValues.GetDictionaryValues(Constants.Areas), "Id", "Name");

            return View(clothingRegion);
        }

        // POST: ClothingRegions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name", "AreaId")] ClothingRegion clothingRegion)
        {
            if (id != clothingRegion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingRegion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingRegionExists(clothingRegion.Id))
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
            return View(clothingRegion);
        }

        // GET: ClothingRegions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClothingRegion == null)
            {
                return NotFound();
            }

            var clothingRegion = await _context.ClothingRegion
               .Include(x => x.Clothings)
               .Include("Clothings.ClothingType")
               .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingRegion == null)
            {
                return NotFound();
            }

            return View(clothingRegion);
        }

        // POST: ClothingRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClothingRegion == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.ClothingRegion'  is null.");
            }
            var clothingRegion = await _context.ClothingRegion
               .Include(b => b.Clothings)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingRegion != null)
            {
                _context.ClothingRegion.Remove(clothingRegion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingRegionExists(int id)
        {
            return _context.ClothingRegion.Any(e => e.Id == id);
        }
    }
}
