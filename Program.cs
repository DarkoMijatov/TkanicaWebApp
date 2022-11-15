using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TkanicaWebApp.BackgroundJobs;
using TkanicaWebApp.Data;
namespace TkanicaWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TkanicaWebAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TkanicaWebAppContext") ?? throw new InvalidOperationException("Connection string 'TkanicaWebAppContext' not found."),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(MembershipFeeBackgroundJob));

                configure.AddJob<MembershipFeeBackgroundJob>(jobKey)
                    .AddTrigger(trigger =>
                        trigger.ForJob(jobKey)
                            .WithCronSchedule("0 0 0 20-28 * ?"));

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            builder.Services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(EarningBackgroundJob));

                configure.AddJob<EarningBackgroundJob>(jobKey)
                    .AddTrigger(trigger =>
                        trigger.ForJob(jobKey)
                            .WithCronSchedule("0 0 * * * ?"));

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            builder.Services.AddQuartzHostedService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{Id?}");

            app.Run();
        }
    }
}