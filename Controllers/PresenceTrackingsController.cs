using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TkanicaWebApp.Classes;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;
using TkanicaWebApp.ViewModels;

namespace TkanicaWebApp.Controllers
{
    public class PresenceTrackingsController : Controller
    {
        private readonly TkanicaWebAppContext _context;

        public PresenceTrackingsController(TkanicaWebAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sort, string search, int? pageIndex, PageViewModel<PresenceTracking> viewModel)
        {
            var tkanicaWebAppContext = _context.Member
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalMembers.Rehearsal")
                .Include(x => x.MembershipFee)
                .Include("MembershipFee.MemberGroup");
            var members = await tkanicaWebAppContext.AsNoTracking().Where(x => x.Active).ToListAsync();
            var rehearsals = await _context.Rehearsal
                .Include(x => x.RehearsalMembers)
                .Include("RehearsalMembers.Member")
                .Include("RehearsalMembers.Member.MembershipFee")
                .Include("RehearsalMembers.Member.MembershipFee.MemberGroup")
                .AsNoTracking()
                .Where(x => x.Date.Year == DateTime.UtcNow.Year)
                .ToListAsync();
            var presenceTrackings = new List<PresenceTracking>();
            foreach(var member in members)
            {
                presenceTrackings.Add(new PresenceTracking
                {
                    Member = member,
                    MemberId = member.Id
                });
            }
            foreach(var presenceTracking in presenceTrackings)
            {
                int rehearsalsSeptemberCount = rehearsals.Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, 9, 1) && x.Date <= new DateTime(DateTime.UtcNow.Year, 10, 1) &&
                    x.RehearsalMembers.Select(x => x.Member.MembershipFee.MemberGroupId).First() == presenceTracking.Member.MembershipFee.MemberGroupId);
                int memberRehearsalsSeptemberCount = presenceTracking.Member.RehearsalMembers.Select(x => x.Rehearsal).Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, 9, 1) && x.Date <= new DateTime(DateTime.UtcNow.Year, 10, 1));
                presenceTracking.SeptemberPercentage = rehearsalsSeptemberCount != 0 ? Math.Round(memberRehearsalsSeptemberCount * 100m / rehearsalsSeptemberCount, 2) : null;
                int rehearsalsOctoberCount = rehearsals.Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, 10, 1) && x.Date <= new DateTime(DateTime.UtcNow.Year, 11, 1) &&
                    x.RehearsalMembers.Select(x => x.Member.MembershipFee.MemberGroupId).First() == presenceTracking.Member.MembershipFee.MemberGroupId);
                int memberRehearsalsOctoberCount = presenceTracking.Member.RehearsalMembers.Select(x => x.Rehearsal).Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, 10, 1) && x.Date <= new DateTime(DateTime.UtcNow.Year, 11, 1));
                presenceTracking.OctoberPercentage = rehearsalsOctoberCount > 0 ? Math.Round(memberRehearsalsOctoberCount * 100m / rehearsalsOctoberCount, 2) : null;
                int rehearsalsNovemberCount = rehearsals.Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, 11, 1) && x.Date <= new DateTime(DateTime.UtcNow.Year, 12, 1) &&
                    x.RehearsalMembers.Select(x => x.Member.MembershipFee.MemberGroupId).First() == presenceTracking.Member.MembershipFee.MemberGroupId);
                int memberRehearsalsNovemberCount = presenceTracking.Member.RehearsalMembers.Select(x => x.Rehearsal).Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, 11, 1) && x.Date <= new DateTime(DateTime.UtcNow.Year, 12, 1));
                presenceTracking.NovemberPercentage = rehearsalsNovemberCount > 0 ? Math.Round(memberRehearsalsNovemberCount * 100m / rehearsalsNovemberCount, 2) : null;
                int rehearsalsDecemberCount = rehearsals.Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, 12, 1) && x.Date <= new DateTime(DateTime.UtcNow.Year + 1, 1, 1) &&
                    x.RehearsalMembers.Select(x => x.Member.MembershipFee.MemberGroupId).First() == presenceTracking.Member.MembershipFee.MemberGroupId);
                int memberRehearsalsDecemberCount = presenceTracking.Member.RehearsalMembers.Select(x => x.Rehearsal).Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, 12, 1) && x.Date <= new DateTime(DateTime.UtcNow.Year + 1, 1, 1));
                presenceTracking.DecemberPercentage = rehearsalsDecemberCount > 0 ? Math.Round(memberRehearsalsDecemberCount * 100m / rehearsalsDecemberCount, 2) : null;
                int totalRehearsalsCount = rehearsalsSeptemberCount + rehearsalsOctoberCount + rehearsalsNovemberCount + rehearsalsDecemberCount;
                presenceTracking.TotalPercentage = totalRehearsalsCount > 0 ? Math.Round((memberRehearsalsSeptemberCount + memberRehearsalsOctoberCount + memberRehearsalsNovemberCount + memberRehearsalsDecemberCount) * 100m / totalRehearsalsCount, 2) : null;
            }

            if (!string.IsNullOrEmpty(sort))
            {
                viewModel.CurrentSort = sort == viewModel.CurrentSort ? sort.Replace("Asc", "Desc") : sort;
                viewModel.List = viewModel.CurrentSort switch
                {
                    "nameAsc" => presenceTrackings.OrderBy(x => x.Member.FirstName + " " + x.Member.LastName).ToList(),
                    "nameDesc" => presenceTrackings.OrderByDescending(x => x.Member.FirstName + " " + x.Member.LastName).ToList(),
                    "septemberAsc" => presenceTrackings.OrderBy(x => x.SeptemberPercentage).ToList(),
                    "septemberDesc" => presenceTrackings.OrderByDescending(x => x.SeptemberPercentage).ToList(),
                    "octoberAsc" => presenceTrackings.OrderBy(x => x.OctoberPercentage).ToList(),
                    "octoberDesc" => presenceTrackings.OrderByDescending(x => x.OctoberPercentage).ToList(),
                    "novemberAsc" => presenceTrackings.OrderBy(x => x.NovemberPercentage).ToList(),
                    "novemberDesc" => presenceTrackings.OrderByDescending(x => x.NovemberPercentage).ToList(),
                    "decemberAsc" => presenceTrackings.OrderBy(x => x.DecemberPercentage).ToList(),
                    "decemberDesc" => presenceTrackings.OrderByDescending(x => x.DecemberPercentage).ToList(),
                    "totalAsc" => presenceTrackings.OrderBy(x => x.TotalPercentage).ToList(),
                    "totalDesc" => presenceTrackings.OrderByDescending(x => x.TotalPercentage).ToList(),
                    _ => presenceTrackings.OrderBy(x => x.MemberId).ToList()
                };
            }
            else
                viewModel.List = presenceTrackings.OrderBy(x => x.MemberId).ToList();

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
    }
}
