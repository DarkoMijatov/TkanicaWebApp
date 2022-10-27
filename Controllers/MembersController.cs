using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;
using TkanicaWebApp.ViewModels;

namespace TkanicaWebApp.Controllers
{
    public class MembersController : Controller
    {
        private readonly TkanicaWebAppContext _context;
        public MembersController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index(string sort, PageViewModel<Member> viewModel)
        {
            var tkanicaWebAppContext = _context.Member
                .Include(m => m.MembershipFee)
                .Include(x => x.MembershipFee.MemberGroup)
                .Include(x => x.RehearsalMembers)
                .Include(x => x.Transactions);
            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = sort == viewModel.CurrentSort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "idAsc" => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync(),
                    "idDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Id).ToListAsync(),
                    "firstNameAsc" => await tkanicaWebAppContext.OrderBy(x => x.FirstName).ToListAsync(),
                    "firstNameDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.FirstName).ToListAsync(),
                    "lastNameAsc" => await tkanicaWebAppContext.OrderBy(x => x.LastName).ToListAsync(),
                    "lastNameDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.LastName).ToListAsync(),
                    "dateOfBirthAsc" => await tkanicaWebAppContext.OrderBy(x => x.DateOfBirth).ToListAsync(),
                    "dateOfBirthDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.DateOfBirth).ToListAsync(),
                    "dateOfEntryAsc" => await tkanicaWebAppContext.OrderBy(x => x.DateOfEntry).ToListAsync(),
                    "dateOfEntryDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.DateOfEntry).ToListAsync(),
                    "activeAsc" => await tkanicaWebAppContext.OrderBy(x => x.Active).ToListAsync(),
                    "activeDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Active).ToListAsync(),
                    "phoneAsc" => await tkanicaWebAppContext.OrderBy(x => x.Phone).ToListAsync(),
                    "phoneDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Phone).ToListAsync(),
                    "emailAsc" => await tkanicaWebAppContext.OrderBy(x => x.Email).ToListAsync(),
                    "emailDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Email).ToListAsync(),
                    "memberGroupAsc" => await tkanicaWebAppContext.OrderBy(x => x.MembershipFee.MemberGroup.Name).ToListAsync(),
                    "memberGroupDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.MembershipFee.MemberGroup.Name).ToListAsync(),
                    "debtAmountAsc" => await tkanicaWebAppContext.OrderBy(x => x.Transactions.Where(x => x.TransactionTypeId == 1 && !x.Paid).Sum(x => x.Amount)).ToListAsync(),
                    "debtAmountDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.Transactions.Where(x => x.TransactionTypeId == 1 && !x.Paid).Sum(x => x.Amount)).ToListAsync(),
                    "rehearsalsAsc" => await tkanicaWebAppContext.OrderBy(x => x.RehearsalMembers.Count).ToListAsync(),
                    "rehearsalsDesc" => await tkanicaWebAppContext.OrderByDescending(x => x.RehearsalMembers.Count).ToListAsync(),
                    _ => await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync()
                };
            }
            else
                viewModel.List = await tkanicaWebAppContext.OrderBy(x => x.Id).ToListAsync();
            return View(viewModel);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .Include(m => m.MembershipFee)
                .Include(x => x.MembershipFee.MemberGroup)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalMembers.Rehearsal")
                .Include(x => x.Transactions)
                .Include("Transactions.TransactionType")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["MembershipFeeId"] = new SelectList(_context.MembershipFee, "Id", "Name");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth,DateOfEntry,Active,Phone,Email,Address,City,School,Class,YearsOfExperience,FacebookProfileUrl,InstagramProfileUrl,TikTokProfileUrl,ProfilePicture,MembershipFeeId,CreatedAt,UpdatedAt")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembershipFeeId"] = new SelectList(_context.MembershipFee, "Id", "Name", member.MembershipFeeId);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .Include(m => m.MembershipFee)
                .Include(x => x.MembershipFee.MemberGroup)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalMembers.Rehearsal")
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            ViewData["MembershipFeeId"] = new SelectList(_context.MembershipFee.Where(x => x.MemberGroup.Active), "Id", "Name", member.MembershipFeeId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,DateOfEntry,Active,Phone,Email,Address,City,School,Class,YearsOfExperience,FacebookProfileUrl,InstagramProfileUrl,TikTokProfileUrl,ProfilePicture,MembershipFeeId,CreatedAt,UpdatedAt")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            ViewData["MembershipFeeId"] = new SelectList(_context.MembershipFee.Where(x => x.MemberGroup.Active), "Id", "Name", member.MembershipFeeId);
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .Include(m => m.MembershipFee)
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalMembers.Rehearsal")
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Member == null)
            {
                return Problem("Entity set 'TkanicaWebAppContext.Member'  is null.");
            }
            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {
                _context.Member.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
          return _context.Member.Any(e => e.Id == id);
        }
    }
}
