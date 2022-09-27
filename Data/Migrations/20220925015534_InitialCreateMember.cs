using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TkanicaWebApp.Data.Migrations
{
    public partial class InitialCreateMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipFee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    MemberGroupId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipFee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipFee_MemberGroup_MemberGroupId",
                        column: x => x.MemberGroupId,
                        principalTable: "MemberGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(20)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(30)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "varchar(10)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    Address = table.Column<string>(type: "varchar(100)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: true),
                    School = table.Column<string>(type: "varchar(100)", nullable: true),
                    Class = table.Column<string>(type: "varchar(20)", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true),
                    FacebookProfileUrl = table.Column<string>(type: "varchar(150)", nullable: true),
                    InstagramProfileUrl = table.Column<string>(type: "varchar(150)", nullable: true),
                    TikTokProfileUrl = table.Column<string>(type: "varchar(150)", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MembershipFeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_MembershipFee_MembershipFeeId",
                        column: x => x.MembershipFeeId,
                        principalTable: "MembershipFee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MemberGroup",
                columns: new[] { "Id", "Active", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 1, true, new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(5983), "Prvi ansambl", new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6030) });

            migrationBuilder.InsertData(
                table: "MemberGroup",
                columns: new[] { "Id", "Active", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 2, true, new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6034), "Dečji ansambl", new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6036) });

            migrationBuilder.InsertData(
                table: "MemberGroup",
                columns: new[] { "Id", "Active", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 3, false, new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6039), "Rekreativna grupa", new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(6041) });

            migrationBuilder.InsertData(
                table: "MembershipFee",
                columns: new[] { "Id", "Amount", "CreatedAt", "MemberGroupId", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1000m, new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7615), 1, "Prvi ansambl", new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7632) },
                    { 2, 500m, new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7636), 1, "Prvi ansambl popust", new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7638) },
                    { 3, 800m, new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7640), 2, "Dečji ansambl", new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7642) },
                    { 4, 400m, new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7645), 2, "Dečji ansambl popust", new DateTime(2022, 9, 25, 3, 55, 34, 398, DateTimeKind.Local).AddTicks(7647) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_MembershipFeeId",
                table: "Member",
                column: "MembershipFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipFee_MemberGroupId",
                table: "MembershipFee",
                column: "MemberGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "MembershipFee");

            migrationBuilder.DropTable(
                name: "MemberGroup");
        }
    }
}
