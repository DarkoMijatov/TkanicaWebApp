using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TkanicaWebApp.Data.Migrations
{
    public partial class ClientMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountNumber_Creditor_CreditorId",
                table: "AccountNumber");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountNumber_Debtor_DebtorId",
                table: "AccountNumber");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Creditor_CreditorId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Debtor_DebtorId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Creditor");

            migrationBuilder.DropTable(
                name: "Debtor");

            migrationBuilder.DropIndex(
                name: "IX_AccountNumber_CreditorId",
                table: "AccountNumber");

            migrationBuilder.DropColumn(
                name: "CreditorId",
                table: "AccountNumber");

            migrationBuilder.RenameColumn(
                name: "DebtorId",
                table: "AccountNumber",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountNumber_DebtorId",
                table: "AccountNumber",
                newName: "IX_AccountNumber_ClientId");

            migrationBuilder.CreateTable(
                name: "Client",
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
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountNumber_Client_ClientId",
                table: "AccountNumber",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Client_CreditorId",
                table: "Transaction",
                column: "CreditorId",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Client_DebtorId",
                table: "Transaction",
                column: "DebtorId",
                principalTable: "Client",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountNumber_Client_ClientId",
                table: "AccountNumber");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Client_CreditorId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Client_DebtorId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "AccountNumber",
                newName: "DebtorId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountNumber_ClientId",
                table: "AccountNumber",
                newName: "IX_AccountNumber_DebtorId");

            migrationBuilder.AddColumn<int>(
                name: "CreditorId",
                table: "AccountNumber",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Creditor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(30)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", nullable: true),
                    IdNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    TaxNumber = table.Column<string>(type: "varchar(10)", nullable: true),
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
                    Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(30)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", nullable: true),
                    IdNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    TaxNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debtor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountNumber_CreditorId",
                table: "AccountNumber",
                column: "CreditorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountNumber_Creditor_CreditorId",
                table: "AccountNumber",
                column: "CreditorId",
                principalTable: "Creditor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountNumber_Debtor_DebtorId",
                table: "AccountNumber",
                column: "DebtorId",
                principalTable: "Debtor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Creditor_CreditorId",
                table: "Transaction",
                column: "CreditorId",
                principalTable: "Creditor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Debtor_DebtorId",
                table: "Transaction",
                column: "DebtorId",
                principalTable: "Debtor",
                principalColumn: "Id");
        }
    }
}
