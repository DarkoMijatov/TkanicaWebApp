using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TkanicaWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothingReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothingId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingReservation_Clothing_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothingReservation_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ClothingRegion",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingRegion",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingRegion",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingRegion",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingReservation_ClothingId",
                table: "ClothingReservation",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothingReservation_ReservationId",
                table: "ClothingReservation",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_MemberId",
                table: "Reservation",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingReservation");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.UpdateData(
                table: "ClothingRegion",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(377), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(383) });

            migrationBuilder.UpdateData(
                table: "ClothingRegion",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(385), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(386) });

            migrationBuilder.UpdateData(
                table: "ClothingRegion",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(387), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(388) });

            migrationBuilder.UpdateData(
                table: "ClothingRegion",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(389), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(389) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(760), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(761) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(763), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(764) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(765), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(765) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(766), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(767) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(768), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(768) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(769), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(770) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(771), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(771) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(772), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(773) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(774), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(774) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(775), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(776) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(777), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(777) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(778), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(779) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(779), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(780) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(781), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(782), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(783) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(784), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(784) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(785), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(786) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(787), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(787) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(788), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(789) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(790), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(790) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(791), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(792) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(793), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(793) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(794), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(795) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(796), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(796) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(797), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(797) });

            migrationBuilder.UpdateData(
                table: "ClothingType",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(798), new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(799) });
        }
    }
}
