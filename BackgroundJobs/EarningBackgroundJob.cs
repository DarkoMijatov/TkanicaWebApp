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
            bool jobAlreadyDone = await _context.EarningUpdate
                .AnyAsync(x => x.Day == DateTime.UtcNow.Day && x.Month == DateTime.UtcNow.Month && x.Year == DateTime.UtcNow.Year);

            if (!jobAlreadyDone)
            {

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
                    if(employee.PayPeriodId == 1 && !employee.Transactions.Any(x => x.TransactionTypeId == 2 && x.TransactionDate == DateTime.UtcNow))
                    {
                        var transactionDate = DateTime.UtcNow.AddDays(-1);

                        var amount = employee.EarningTypeId switch
                        {
                            1 => employee.EarningAmount,
                            2 => employee.RehearsalEmployees.Select(x => x.Rehearsal).Count(x => x.Date == transactionDate) * employee.EarningAmount,
                            _ => employee.RehearsalEmployees.Select(x => x.Rehearsal).Where(x => x.Date == transactionDate)
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
                            Description = $"{transactionDate.Day}.{transactionDate.Month}.{transactionDate.Year}.",
                            Amount = amount,
                            Paid = false,
                            TransactionDate = DateTime.UtcNow
                        });
                    }
                    else if (employee.PayPeriodId == 2 && DateTime.UtcNow.DayOfWeek == DayOfWeek.Monday && !employee.Transactions.Any(x => x.TransactionTypeId == 2 && x.TransactionDate == DateTime.UtcNow))
                    {
                        var startTransactionDate = DateTime.UtcNow.AddDays(-7);
                        var endTransactionDate = DateTime.UtcNow.AddDays(-1);

                        var amount = employee.EarningTypeId switch
                        {
                            1 => employee.EarningAmount,
                            2 => employee.RehearsalEmployees.Select(x => x.Rehearsal).Count(x => x.Date >= startTransactionDate && x.Date < DateTime.UtcNow) * employee.EarningAmount,
                            _ => employee.RehearsalEmployees.Select(x => x.Rehearsal).Where(x => x.Date >= startTransactionDate && x.Date < DateTime.UtcNow)
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
                            Description = startTransactionDate.Year == endTransactionDate.Year ? 
                                startTransactionDate.Month == endTransactionDate.Month ?
                                $"{startTransactionDate.Day} - {endTransactionDate.Day}.{endTransactionDate.Month}.{DateTime.UtcNow.Year}." :
                                $"{startTransactionDate.Day}.{startTransactionDate.Month} - {endTransactionDate.Day}. {endTransactionDate.Month}.{DateTime.UtcNow.Year}." :
                                $"{startTransactionDate.Day}.{startTransactionDate.Month}.{startTransactionDate.Year} - {endTransactionDate.Day}.{endTransactionDate.Month}.{endTransactionDate.Year}.",
                            Amount = amount,
                            Paid = false,
                            TransactionDate = DateTime.UtcNow
                        });
                    }
                    else if (employee.PayPeriodId == 3 && DateTime.UtcNow.Day == 1 && !employee.Transactions.Any(x => x.TransactionTypeId == 2 && x.TransactionDate == DateTime.UtcNow))
                    {
                        var transactionDate = DateTime.UtcNow.AddDays(-1);
                        var amount = employee.EarningTypeId switch
                        {
                            1 => employee.EarningAmount,
                            2 => employee.RehearsalEmployees.Select(x => x.Rehearsal).Count(x => x.Date >= new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month - 1, 1) && x.Date < DateTime.UtcNow) * employee.EarningAmount,
                            _ => employee.RehearsalEmployees.Select(x => x.Rehearsal).Where(x => x.Date >= new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month - 1, 1) && x.Date < DateTime.UtcNow)
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
                            Description = $"{transactionDate.Month}.{DateTime.UtcNow.Year}.",
                            Amount = amount,
                            Paid = false,
                            TransactionDate = DateTime.UtcNow
                        });
                    }
                }

                _context.EarningUpdate.Add(new EarningUpdate
                {
                    Day = DateTime.UtcNow.Day,
                    Month = DateTime.UtcNow.Month,
                    Year = DateTime.UtcNow.Year
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
