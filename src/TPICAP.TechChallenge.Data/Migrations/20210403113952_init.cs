using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TPICAP.TechChallenge.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salutations",
                columns: table => new
                {
                    SalutationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalutationName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salutations", x => x.SalutationId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalutationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Salutations_SalutationId",
                        column: x => x.SalutationId,
                        principalTable: "Salutations",
                        principalColumn: "SalutationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Salutations",
                columns: new[] { "SalutationId", "SalutationName" },
                values: new object[,]
                {
                    { 1, "Mr" },
                    { 2, "Mrs" },
                    { 3, "Miss" },
                    { 4, "Dr" },
                    { 5, "Sir" },
                    { 6, "Lord" },
                    { 7, "Lady" },
                    { 8, "Prof" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[,]
                {
                    { 1, new DateTime(1952, 9, 27, 16, 38, 5, 704, DateTimeKind.Local).AddTicks(56), "Curtis", "Rutherford", 1 },
                    { 28, new DateTime(1963, 2, 11, 21, 15, 18, 573, DateTimeKind.Local).AddTicks(9173), "Emilio", "Rath", 1 },
                    { 27, new DateTime(1958, 7, 28, 5, 10, 53, 685, DateTimeKind.Local).AddTicks(7533), "Gertrude", "Price", 1 },
                    { 26, new DateTime(1980, 6, 18, 3, 12, 6, 443, DateTimeKind.Local).AddTicks(9039), "Geoffrey", "Lehner", 1 },
                    { 25, new DateTime(1964, 1, 14, 19, 37, 45, 857, DateTimeKind.Local).AddTicks(5430), "Isabel", "Mraz", 1 },
                    { 24, new DateTime(1974, 12, 21, 2, 44, 1, 231, DateTimeKind.Local).AddTicks(4819), "Dave", "Bernier", 1 },
                    { 23, new DateTime(1954, 10, 8, 23, 42, 45, 470, DateTimeKind.Local).AddTicks(4827), "Clifford", "Senger", 1 },
                    { 22, new DateTime(1964, 2, 7, 2, 0, 14, 979, DateTimeKind.Local).AddTicks(6112), "Catherine", "Graham", 1 },
                    { 21, new DateTime(1988, 3, 30, 0, 17, 25, 226, DateTimeKind.Local).AddTicks(5543), "Moses", "Hodkiewicz", 1 },
                    { 20, new DateTime(1997, 4, 14, 21, 44, 40, 161, DateTimeKind.Local).AddTicks(1183), "Claire", "Greenfelder", 1 },
                    { 19, new DateTime(1987, 7, 21, 1, 58, 50, 552, DateTimeKind.Local).AddTicks(6072), "Beverly", "Dicki", 1 },
                    { 18, new DateTime(1952, 5, 26, 17, 47, 23, 461, DateTimeKind.Local).AddTicks(8085), "Seth", "Krajcik", 1 },
                    { 17, new DateTime(1989, 9, 12, 14, 13, 33, 549, DateTimeKind.Local).AddTicks(539), "Olive", "Purdy", 1 },
                    { 16, new DateTime(1996, 10, 12, 6, 29, 39, 95, DateTimeKind.Local).AddTicks(4553), "Laurie", "O'Hara", 1 },
                    { 15, new DateTime(1990, 1, 14, 17, 55, 0, 909, DateTimeKind.Local).AddTicks(9520), "Bennie", "Lehner", 1 },
                    { 14, new DateTime(1986, 9, 9, 0, 17, 0, 583, DateTimeKind.Local).AddTicks(4466), "Muriel", "Abernathy", 1 },
                    { 13, new DateTime(1995, 6, 19, 6, 56, 27, 13, DateTimeKind.Local).AddTicks(4082), "Irene", "Runolfsson", 1 },
                    { 12, new DateTime(2000, 8, 19, 15, 35, 44, 917, DateTimeKind.Local).AddTicks(4066), "Joanna", "Carter", 1 },
                    { 11, new DateTime(1997, 7, 1, 14, 11, 4, 14, DateTimeKind.Local).AddTicks(6466), "Tony", "Blick", 1 },
                    { 10, new DateTime(1969, 5, 10, 9, 0, 19, 71, DateTimeKind.Local).AddTicks(4960), "Gladys", "Fritsch", 1 },
                    { 9, new DateTime(2000, 7, 31, 12, 2, 38, 280, DateTimeKind.Local).AddTicks(3214), "Phillip", "Cronin", 1 },
                    { 8, new DateTime(1984, 2, 4, 17, 30, 56, 796, DateTimeKind.Local).AddTicks(1495), "Frank", "Runolfsson", 1 },
                    { 7, new DateTime(1993, 7, 19, 6, 42, 49, 619, DateTimeKind.Local).AddTicks(4471), "Denise", "Waters", 1 },
                    { 6, new DateTime(1955, 6, 7, 13, 45, 8, 667, DateTimeKind.Local).AddTicks(3909), "Dora", "Yost", 1 },
                    { 5, new DateTime(1983, 8, 20, 19, 29, 29, 338, DateTimeKind.Local).AddTicks(2262), "Angel", "Rippin", 1 },
                    { 4, new DateTime(1955, 8, 29, 15, 28, 29, 565, DateTimeKind.Local).AddTicks(8899), "Lela", "Zemlak", 1 },
                    { 3, new DateTime(1984, 10, 2, 6, 5, 27, 654, DateTimeKind.Local).AddTicks(314), "Elvira", "Turner", 1 },
                    { 2, new DateTime(1980, 4, 9, 1, 53, 15, 467, DateTimeKind.Local).AddTicks(3774), "Emmett", "Murazik", 1 },
                    { 29, new DateTime(2000, 11, 6, 4, 19, 14, 661, DateTimeKind.Local).AddTicks(8196), "Johnnie", "Deckow", 1 },
                    { 30, new DateTime(1963, 11, 23, 9, 50, 53, 88, DateTimeKind.Local).AddTicks(6732), "Shari", "Gutkowski", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_SalutationId",
                table: "People",
                column: "SalutationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Salutations");
        }
    }
}
