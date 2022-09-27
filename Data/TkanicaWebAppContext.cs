﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TkanicaWebApp.Interfaces;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data
{
    public class TkanicaWebAppContext : DbContext
    {
        public TkanicaWebAppContext (DbContextOptions<TkanicaWebAppContext> options)
            : base(options)
        {
        }
        public DbSet<MemberGroup> MemberGroup { get; set; }
        public DbSet<MembershipFee> MembershipFee { get; set; }
        public DbSet<Member> Member { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            UpdateDateTime();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateDateTime();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateDateTime()
        {
            var entries = ChangeTracker
                 .Entries()
                 .Where(e => e.Entity is ITrackable && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((ITrackable)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((ITrackable)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }

        }
    }
}