﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TkanicaWebApp.Data;

#nullable disable

namespace TkanicaWebApp.Data.Migrations
{
    [DbContext(typeof(TkanicaWebAppContext))]
    partial class TkanicaWebAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TkanicaWebApp.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("EarningAmount")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("EarningType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(30)");

                    b.Property<decimal?>("OtherExpenses")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<string>("OtherExpensesDescription")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PayPeriod")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.EmployeeMemberGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("MemberGroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("MemberGroupId");

                    b.ToTable("EmployeeMemberGroup");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("City")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Class")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfEntry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FacebookProfileUrl")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("InstagramProfileUrl")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(30)");

                    b.Property<int>("MembershipFeeId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(10)");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("School")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TikTokProfileUrl")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("YearsOfExperience")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MembershipFeeId");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.MemberGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MemberGroup");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            CreatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(5983),
                            Name = "Prvi ansambl",
                            UpdatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6030)
                        },
                        new
                        {
                            Id = 2,
                            Active = true,
                            CreatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6034),
                            Name = "Dečji ansambl",
                            UpdatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6036)
                        },
                        new
                        {
                            Id = 3,
                            Active = false,
                            CreatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6039),
                            Name = "Rekreativna grupa",
                            UpdatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6041)
                        });
                });

            modelBuilder.Entity("TkanicaWebApp.Models.MembershipFee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MemberGroupId");

                    b.ToTable("MembershipFee");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 1000m,
                            CreatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7615),
                            MemberGroupId = 1,
                            Name = "Prvi ansambl",
                            UpdatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7632)
                        },
                        new
                        {
                            Id = 2,
                            Amount = 500m,
                            CreatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7636),
                            MemberGroupId = 1,
                            Name = "Prvi ansambl popust",
                            UpdatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7638)
                        },
                        new
                        {
                            Id = 3,
                            Amount = 800m,
                            CreatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7640),
                            MemberGroupId = 2,
                            Name = "Dečji ansambl",
                            UpdatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7642)
                        },
                        new
                        {
                            Id = 4,
                            Amount = 400m,
                            CreatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7645),
                            MemberGroupId = 2,
                            Name = "Dečji ansambl popust",
                            UpdatedAt = new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7647)
                        });
                });

            modelBuilder.Entity("TkanicaWebApp.Models.EmployeeMemberGroup", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.Employee", "Employee")
                        .WithMany("EmployeeMemberGroups")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TkanicaWebApp.Models.MemberGroup", "MemberGroup")
                        .WithMany("EmployeeMemberGroups")
                        .HasForeignKey("MemberGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("MemberGroup");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Member", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.MembershipFee", "MembershipFee")
                        .WithMany("Members")
                        .HasForeignKey("MembershipFeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MembershipFee");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.MembershipFee", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.MemberGroup", "MemberGroup")
                        .WithMany("MembershipFees")
                        .HasForeignKey("MemberGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MemberGroup");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Employee", b =>
                {
                    b.Navigation("EmployeeMemberGroups");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.MemberGroup", b =>
                {
                    b.Navigation("EmployeeMemberGroups");

                    b.Navigation("MembershipFees");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.MembershipFee", b =>
                {
                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
