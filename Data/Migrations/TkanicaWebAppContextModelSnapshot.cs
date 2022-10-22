﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TkanicaWebApp.Data;

#nullable disable

namespace TkanicaWebApp.Migrations
{
    [DbContext(typeof(TkanicaWebAppContext))]
    partial class TkanicaWebAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Serbian_Latin_100_CI_AI_KS_WS_SC_UTF8")
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TkanicaWebApp.Models.AccountNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BalanceId")
                        .HasColumnType("int");

                    b.Property<string>("Bank")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("BankAccountNumber")
                        .HasColumnType("varchar(30)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("AccountNumber");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BalanceId = 2,
                            Bank = "Poštanska štedionica A.D.",
                            BankAccountNumber = "200-3169580101844-71",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            BalanceId = 3,
                            Bank = "Uprava za trezor",
                            BankAccountNumber = "840-49151763-68",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Balance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AccountNumberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCash")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumberId")
                        .IsUnique()
                        .HasFilter("[AccountNumberId] IS NOT NULL");

                    b.ToTable("Balance");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCash = true,
                            Name = "kasa",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            AccountNumberId = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCash = false,
                            Name = "račun banka",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            AccountNumberId = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCash = false,
                            Name = "račun trezor",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("City")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("IdNumber")
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("IsCompany")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Logo")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Website")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.EarningType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EarningType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "fiksno",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "po probi",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "po članu",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

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

                    b.Property<int>("EarningTypeId")
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

                    b.Property<int>("PayPeriodId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EarningTypeId");

                    b.HasIndex("PayPeriodId");

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
                });

            modelBuilder.Entity("TkanicaWebApp.Models.MembershipFeeDebtUpdate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MembershipFeeDebtUpdate");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.PayPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PayPeriod");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "dnevno",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "nedeljno",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "mesečno",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Rehearsal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Rehearsal");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.RehearsalEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("RehearsalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("RehearsalId");

                    b.ToTable("RehearsalEmployee");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.RehearsalMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("RehearsalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("RehearsalId");

                    b.ToTable("RehearsalMember");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("BalanceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreditorId")
                        .HasColumnType("int");

                    b.Property<int?>("DebtorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionNumber")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.HasIndex("CreditorId");

                    b.HasIndex("DebtorId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("MemberId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<short>("Direction")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TransactionType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Direction = (short)1,
                            Name = "članarina",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Direction = (short)-1,
                            Name = "zarada",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Direction = (short)1,
                            Name = "donacija",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Direction = (short)1,
                            Name = "uplata gotovine",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Direction = (short)-1,
                            Name = "isplata gotovine",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Direction = (short)1,
                            Name = "priliv na račun",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Direction = (short)-1,
                            Name = "odliv sa računa",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TkanicaWebApp.Models.AccountNumber", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.Client", "Client")
                        .WithMany("AccountNumbers")
                        .HasForeignKey("ClientId");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Balance", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.AccountNumber", "AccountNumber")
                        .WithOne("Balance")
                        .HasForeignKey("TkanicaWebApp.Models.Balance", "AccountNumberId");

                    b.Navigation("AccountNumber");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Employee", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.EarningType", "EarningType")
                        .WithMany("Employees")
                        .HasForeignKey("EarningTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TkanicaWebApp.Models.PayPeriod", "PayPeriod")
                        .WithMany("Employees")
                        .HasForeignKey("PayPeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EarningType");

                    b.Navigation("PayPeriod");
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

            modelBuilder.Entity("TkanicaWebApp.Models.RehearsalEmployee", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.Employee", "Employee")
                        .WithMany("RehearsalEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TkanicaWebApp.Models.Rehearsal", "Rehearsal")
                        .WithMany("RehearsalEmployees")
                        .HasForeignKey("RehearsalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Rehearsal");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.RehearsalMember", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.Member", "Member")
                        .WithMany("RehearsalMembers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TkanicaWebApp.Models.Rehearsal", "Rehearsal")
                        .WithMany("RehearsalMembers")
                        .HasForeignKey("RehearsalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Rehearsal");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Transaction", b =>
                {
                    b.HasOne("TkanicaWebApp.Models.Balance", "Balance")
                        .WithMany("Transactions")
                        .HasForeignKey("BalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TkanicaWebApp.Models.Client", "Creditor")
                        .WithMany("CreditorTransactions")
                        .HasForeignKey("CreditorId");

                    b.HasOne("TkanicaWebApp.Models.Client", "Debtor")
                        .WithMany("DebtorTransactions")
                        .HasForeignKey("DebtorId");

                    b.HasOne("TkanicaWebApp.Models.Employee", "Employee")
                        .WithMany("Transactions")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("TkanicaWebApp.Models.Member", "Member")
                        .WithMany("Transactions")
                        .HasForeignKey("MemberId");

                    b.HasOne("TkanicaWebApp.Models.TransactionType", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Balance");

                    b.Navigation("Creditor");

                    b.Navigation("Debtor");

                    b.Navigation("Employee");

                    b.Navigation("Member");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.AccountNumber", b =>
                {
                    b.Navigation("Balance");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Balance", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Client", b =>
                {
                    b.Navigation("AccountNumbers");

                    b.Navigation("CreditorTransactions");

                    b.Navigation("DebtorTransactions");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.EarningType", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Employee", b =>
                {
                    b.Navigation("EmployeeMemberGroups");

                    b.Navigation("RehearsalEmployees");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Member", b =>
                {
                    b.Navigation("RehearsalMembers");

                    b.Navigation("Transactions");
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

            modelBuilder.Entity("TkanicaWebApp.Models.PayPeriod", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.Rehearsal", b =>
                {
                    b.Navigation("RehearsalEmployees");

                    b.Navigation("RehearsalMembers");
                });

            modelBuilder.Entity("TkanicaWebApp.Models.TransactionType", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
