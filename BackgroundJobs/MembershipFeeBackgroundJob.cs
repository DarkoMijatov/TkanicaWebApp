using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Transactions;
using TkanicaWebApp.Data;

namespace TkanicaWebApp.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class MembershipFeeBackgroundJob : IJob
    {
        private readonly TkanicaWebAppContext _dbContext;

        public MembershipFeeBackgroundJob(TkanicaWebAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            bool jobAlreadyDone = await _dbContext.MembershipFeeDebtUpdate
                .AnyAsync(x => x.Year == DateTime.UtcNow.Year && x.Month == DateTime.UtcNow.Month);

            if (DateTime.UtcNow.Day >= 20 && !jobAlreadyDone)
            {
                var month = DateTime.UtcNow.Month switch
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
                var members = await _dbContext.Member
                    .Include(x => x.MembershipFee)
                    .Include(x => x.Transactions)
                    .Include("Transactions.TransactionType")
                    .Where(x => x.Active)
                    .ToListAsync();
                foreach (var member in members)
                {
                    if (!member.Transactions.Any(x => x.TransactionTypeId == 1 && x.Description == $"{month} {DateTime.UtcNow.Year}."))
                    {
                        _dbContext.Transaction.Add(new Models.Transaction
                        {
                            TransactionTypeId = 1,
                            MemberId = member.Id,
                            BalanceId = 1,
                            Description = $"{month} {DateTime.UtcNow.Year}.",
                            Amount = member.MembershipFee.Amount,
                            Paid = false,
                            TransactionDate = DateTime.UtcNow
                        });
                    }
                }

                _dbContext.MembershipFeeDebtUpdate.Add(new Models.MembershipFeeDebtUpdate
                {
                    Month = DateTime.UtcNow.Month,
                    Year = DateTime.UtcNow.Year
                });
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
