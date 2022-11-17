using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TkanicaWebApp.Data.Migrations
{
    public partial class ClothingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClothingRegion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingRegion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClothingType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clothing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothingTypeId = table.Column<int>(type: "int", nullable: false),
                    ClothingRegionId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "varchar(10)", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clothing_ClothingRegion_ClothingRegionId",
                        column: x => x.ClothingRegionId,
                        principalTable: "ClothingRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clothing_ClothingType_ClothingTypeId",
                        column: x => x.ClothingTypeId,
                        principalTable: "ClothingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClothingRegion",
                columns: new[] { "Id", "AreaId", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(377), "Banat", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(383) },
                    { 2, 2, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(385), "Šumadija", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(386) },
                    { 3, 5, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(387), "Vranjsko polje", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(388) },
                    { 4, 5, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(389), "Pčinja", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(389) }
                });

            migrationBuilder.InsertData(
                table: "ClothingType",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(760), "anterija", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(761) },
                    { 2, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(763), "cipele", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(764) },
                    { 3, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(765), "čakšire", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(765) },
                    { 4, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(766), "čarape", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(767) },
                    { 5, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(768), "čizme", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(768) },
                    { 6, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(769), "dolama", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(770) },
                    { 7, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(771), "džuba", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(771) },
                    { 8, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(772), "futa", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(773) },
                    { 9, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(774), "gaće", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(774) },
                    { 10, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(775), "gunj", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(776) },
                    { 11, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(777), "haljina", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(777) },
                    { 12, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(778), "jelek", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(779) },
                    { 13, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(779), "kecelja", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(780) },
                    { 14, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(781), "košulja", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(781) },
                    { 15, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(782), "marama", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(783) },
                    { 16, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(784), "opanci", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(784) },
                    { 17, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(785), "pantalone", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(786) },
                    { 18, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(787), "pargar", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(787) },
                    { 19, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(788), "pojas", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(789) },
                    { 20, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(790), "prsluk", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(790) },
                    { 21, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(791), "suknja", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(792) },
                    { 22, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(793), "šajkača", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(793) },
                    { 23, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(794), "šalvare", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(795) },
                    { 24, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(796), "šubara", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(796) },
                    { 25, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(797), "torba", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(797) },
                    { 26, new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(798), "zubun", new DateTime(2022, 11, 17, 23, 46, 49, 533, DateTimeKind.Utc).AddTicks(799) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clothing_ClothingRegionId",
                table: "Clothing",
                column: "ClothingRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothing_ClothingTypeId",
                table: "Clothing",
                column: "ClothingTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clothing");

            migrationBuilder.DropTable(
                name: "ClothingRegion");

            migrationBuilder.DropTable(
                name: "ClothingType");
        }
    }
}
