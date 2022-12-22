﻿using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.Member)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Employee)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new User
            {
                Id = 1, Email = "darko.mijatov95@gmail.com", UserTypeId = 1, Password = "admin".ToSHA256String(), Verified = true
            });
        }
    }
}
