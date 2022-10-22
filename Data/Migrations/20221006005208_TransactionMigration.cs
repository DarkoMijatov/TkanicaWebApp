using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TkanicaWebApp.Data.Migrations
{
    public partial class TransactionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Creditor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(30)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    Email = table.Column<string>(type: "varchar(30)", nullable: true),
                    IdNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    TaxNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creditor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Debtor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(30)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    Email = table.Column<string>(type: "varchar(30)", nullable: true),
                    IdNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    TaxNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debtor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    Direction = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Bank = table.Column<string>(type: "varchar(30)", nullable: true),
                    CreditorId = table.Column<int>(type: "int", nullable: true),
                    DebtorId = table.Column<int>(type: "int", nullable: true),
                    BalanceId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountNumber_Creditor_CreditorId",
                        column: x => x.CreditorId,
                        principalTable: "Creditor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountNumber_Debtor_DebtorId",
                        column: x => x.DebtorId,
                        principalTable: "Debtor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    AccountNumberId = table.Column<int>(type: "int", nullable: true),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balance_AccountNumber_AccountNumberId",
                        column: x => x.AccountNumberId,
                        principalTable: "AccountNumber",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    DebtorId = table.Column<int>(type: "int", nullable: true),
                    CreditorId = table.Column<int>(type: "int", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(14,2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    BalanceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Balance_BalanceId",
                        column: x => x.BalanceId,
                        principalTable: "Balance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Creditor_CreditorId",
                        column: x => x.CreditorId,
                        principalTable: "Creditor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_Debtor_DebtorId",
                        column: x => x.DebtorId,
                        principalTable: "Debtor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountNumber",
                columns: new[] { "Id", "BalanceId", "Bank", "BankAccountNumber", "CreatedAt", "CreditorId", "DebtorId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, "Poštanska štedionica A.D.", "200-3169580101844-71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, "Uprava za trezor", "840-49151763-68", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Balance",
                columns: new[] { "Id", "AccountNumberId", "CreatedAt", "IsCash", "Name", "UpdatedAt" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "kasa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TransactionType",
                columns: new[] { "Id", "CreatedAt", "Direction", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)1, "članarina", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)-1, "zarada", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)1, "donacija", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)1, "uplata gotovine", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)-1, "isplata gotovine", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)1, "priliv na račun", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)-1, "odliv sa računa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Balance",
                columns: new[] { "Id", "AccountNumberId", "CreatedAt", "IsCash", "Name", "UpdatedAt" },
                values: new object[] { 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "račun banka", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Balance",
                columns: new[] { "Id", "AccountNumberId", "CreatedAt", "IsCash", "Name", "UpdatedAt" },
                values: new object[] { 3, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "račun trezor", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_AccountNumber_CreditorId",
                table: "AccountNumber",
                column: "CreditorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountNumber_DebtorId",
                table: "AccountNumber",
                column: "DebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_AccountNumberId",
                table: "Balance",
                column: "AccountNumberId",
                unique: true,
                filter: "[AccountNumberId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BalanceId",
                table: "Transaction",
                column: "BalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CreditorId",
                table: "Transaction",
                column: "CreditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DebtorId",
                table: "Transaction",
                column: "DebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_EmployeeId",
                table: "Transaction",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_MemberId",
                table: "Transaction",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionTypeId",
                table: "Transaction",
                column: "TransactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "TransactionType");

            migrationBuilder.DropTable(
                name: "AccountNumber");

            migrationBuilder.DropTable(
                name: "Creditor");

            migrationBuilder.DropTable(
                name: "Debtor");
        }
    }
}
