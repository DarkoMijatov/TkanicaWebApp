using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Quartz;
using TkanicaWebApp.Data;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.BackgroundJobs
{
    public class EarningBackgroundJob : IJob
    {
        private readonly TkanicaWebAppContext _context;

        public EarningBackgroundJob(TkanicaWebAppContext context)
        {
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var lastEarningUpdate = await _context.EarningUpdate.OrderByDescending(x => x.UpdatedAt).FirstOrDefaultAsync();

            if (DateTime.UtcNow.Day == 1 && (lastEarningUpdate is null) || (lastEarningUpdate.Month != DateTime.UtcNow.Month - 1))
            {
                var month = (DateTime.UtcNow.Month - 1) switch
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

                var employees = await _context.Employee
                    .Include(x => x.Transactions)
                    .Include(x => x.RehearsalEmployees)
                    .Include("RehearsalEmployees.Rehearsal")
                    .Include("RehearsalEmployees.Rehearsal.RehearsalMembers")
                    .Include("RehearsalEmployees.Rehearsal.RehearsalMembers.Member")
                    .Where(x => x.Active)
                    .ToListAsync();

                foreach (var employee in employees)
                {
                    if (!employee.Transactions.Any(x => x.TransactionTypeId == 2 && x.Description == $"{month} {DateTime.UtcNow.Year}"))
                    {
                        var amount = employee.EarningTypeId switch
                        {
                            1 => employee.EarningAmount,
                            2 => employee.RehearsalEmployees.Select(x => x.Rehearsal).Count() * employee.EarningAmount,
                            _ => employee.RehearsalEmployees.Select(x => x.Rehearsal)
                                .SelectMany(x => x.RehearsalMembers)
                                .DistinctBy(x => x.Member)
                                .Where(x => x.Member.Active)
                                .Count() * employee.EarningAmount
                        };

                        amount += employee.OtherExpenses ?? 0m;

                        _context.Transaction.Add(new Models.Transaction
                        {
                            TransactionTypeId = 2,
                            EmployeeId = employee.Id,
                            BalanceId = 1,
                            Description = $"{month} {DateTime.UtcNow.Year}.",
                            Amount = amount,
                            Paid = false,
                            TransactionDate = DateTime.UtcNow
                        });
                    }
                }

                _context.EarningUpdate.Add(new EarningUpdate
                {
                    Month = DateTime.UtcNow.Month - 1,
                    Year = DateTime.UtcNow.Year
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
