using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TkanicaWebApp.Data.Migrations
{
    public partial class Rehearsal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rehearsal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rehearsal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RehearsalEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RehearsalId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehearsalEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RehearsalEmployee_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RehearsalEmployee_Rehearsal_RehearsalId",
                        column: x => x.RehearsalId,
                        principalTable: "Rehearsal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RehearsalMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RehearsalId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehearsalMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RehearsalMember_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RehearsalMember_Rehearsal_RehearsalId",
                        column: x => x.RehearsalId,
                        principalTable: "Rehearsal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalEmployee_EmployeeId",
                table: "RehearsalEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalEmployee_RehearsalId",
                table: "RehearsalEmployee",
                column: "RehearsalId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalMember_MemberId",
                table: "RehearsalMember",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalMember_RehearsalId",
                table: "RehearsalMember",
                column: "RehearsalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RehearsalEmployee");

            migrationBuilder.DropTable(
                name: "RehearsalMember");

            migrationBuilder.DropTable(
                name: "Rehearsal");
        }
    }
}
