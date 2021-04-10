using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TPICAP.TechChallenge.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1992, 4, 8, 23, 42, 19, 59, DateTimeKind.Local).AddTicks(2693), "Jerald", "Pouros", 2 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1968, 5, 14, 9, 21, 47, 1, DateTimeKind.Local).AddTicks(9463), "Matt", "Hirthe", 5 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1951, 6, 22, 3, 4, 36, 369, DateTimeKind.Local).AddTicks(6408), "Pat", "Bradtke", 5 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1967, 2, 10, 23, 18, 26, 741, DateTimeKind.Local).AddTicks(5206), "Shelley", "DuBuque", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateOfBirth", "FirstName", "LastName" },
                values: new object[] { new DateTime(1997, 11, 28, 19, 57, 39, 117, DateTimeKind.Local).AddTicks(8341), "Valerie", "Runolfsdottir" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1960, 2, 25, 19, 56, 9, 327, DateTimeKind.Local).AddTicks(2440), "Myron", "Upton", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1975, 10, 10, 18, 10, 40, 960, DateTimeKind.Local).AddTicks(5175), "Darrell", "Schinner", 5 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1996, 2, 26, 20, 36, 52, 281, DateTimeKind.Local).AddTicks(370), "Francis", "Schultz", 7 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1997, 1, 18, 3, 4, 47, 140, DateTimeKind.Local).AddTicks(9558), "Eugene", "Gutkowski", 3 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1973, 6, 4, 13, 0, 51, 564, DateTimeKind.Local).AddTicks(9953), "Tracey", "Casper", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1994, 10, 25, 18, 19, 5, 237, DateTimeKind.Local).AddTicks(1675), "Kirk", "Sporer", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1962, 12, 23, 18, 37, 50, 447, DateTimeKind.Local).AddTicks(1020), "Opal", "Steuber", 8 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1990, 4, 16, 5, 32, 38, 429, DateTimeKind.Local).AddTicks(932), "Marjorie", "Davis", 2 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1986, 1, 8, 5, 18, 15, 650, DateTimeKind.Local).AddTicks(2541), "Kent", "Skiles", 3 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1983, 1, 12, 18, 55, 7, 207, DateTimeKind.Local).AddTicks(8473), "Lola", "Parisian", 5 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1965, 1, 8, 6, 59, 57, 917, DateTimeKind.Local).AddTicks(7255), "Ernesto", "D'Amore", 2 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1968, 6, 17, 16, 8, 42, 493, DateTimeKind.Local).AddTicks(581), "Delores", "Simonis", 4 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1988, 10, 22, 1, 21, 10, 428, DateTimeKind.Local).AddTicks(9826), "Marguerite", "Haley", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1980, 8, 8, 12, 15, 22, 133, DateTimeKind.Local).AddTicks(6915), "Joe", "Murphy", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1961, 10, 1, 21, 23, 28, 524, DateTimeKind.Local).AddTicks(5339), "Alberta", "Gutmann", 3 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1996, 10, 6, 19, 24, 31, 536, DateTimeKind.Local).AddTicks(5526), "Gwendolyn", "Terry", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1959, 1, 4, 3, 42, 55, 373, DateTimeKind.Local).AddTicks(6584), "Joy", "Marks", 3 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1992, 9, 12, 2, 48, 35, 99, DateTimeKind.Local).AddTicks(5913), "Lee", "Tromp", 7 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1989, 11, 14, 16, 11, 11, 365, DateTimeKind.Local).AddTicks(211), "Carroll", "Kohler", 2 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1955, 6, 20, 18, 7, 47, 596, DateTimeKind.Local).AddTicks(1356), "Frederick", "Hahn", 7 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1966, 3, 30, 21, 45, 24, 460, DateTimeKind.Local).AddTicks(1688), "Candice", "Ziemann", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1952, 8, 29, 7, 59, 12, 32, DateTimeKind.Local).AddTicks(7291), "Ramiro", "Gulgowski", 6 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateOfBirth", "FirstName", "LastName" },
                values: new object[] { new DateTime(1966, 10, 23, 22, 25, 3, 421, DateTimeKind.Local).AddTicks(726), "Roy", "Thiel" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1960, 1, 21, 7, 35, 41, 125, DateTimeKind.Local).AddTicks(4686), "Roman", "Crona", 2 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1962, 2, 9, 0, 50, 38, 799, DateTimeKind.Local).AddTicks(2778), "Sophia", "Hoeger", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1952, 9, 27, 16, 38, 5, 704, DateTimeKind.Local).AddTicks(56), "Curtis", "Rutherford", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1980, 4, 9, 1, 53, 15, 467, DateTimeKind.Local).AddTicks(3774), "Emmett", "Murazik", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1984, 10, 2, 6, 5, 27, 654, DateTimeKind.Local).AddTicks(314), "Elvira", "Turner", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1955, 8, 29, 15, 28, 29, 565, DateTimeKind.Local).AddTicks(8899), "Lela", "Zemlak", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateOfBirth", "FirstName", "LastName" },
                values: new object[] { new DateTime(1983, 8, 20, 19, 29, 29, 338, DateTimeKind.Local).AddTicks(2262), "Angel", "Rippin" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1955, 6, 7, 13, 45, 8, 667, DateTimeKind.Local).AddTicks(3909), "Dora", "Yost", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1993, 7, 19, 6, 42, 49, 619, DateTimeKind.Local).AddTicks(4471), "Denise", "Waters", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1984, 2, 4, 17, 30, 56, 796, DateTimeKind.Local).AddTicks(1495), "Frank", "Runolfsson", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(2000, 7, 31, 12, 2, 38, 280, DateTimeKind.Local).AddTicks(3214), "Phillip", "Cronin", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1969, 5, 10, 9, 0, 19, 71, DateTimeKind.Local).AddTicks(4960), "Gladys", "Fritsch", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1997, 7, 1, 14, 11, 4, 14, DateTimeKind.Local).AddTicks(6466), "Tony", "Blick", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(2000, 8, 19, 15, 35, 44, 917, DateTimeKind.Local).AddTicks(4066), "Joanna", "Carter", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1995, 6, 19, 6, 56, 27, 13, DateTimeKind.Local).AddTicks(4082), "Irene", "Runolfsson", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1986, 9, 9, 0, 17, 0, 583, DateTimeKind.Local).AddTicks(4466), "Muriel", "Abernathy", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1990, 1, 14, 17, 55, 0, 909, DateTimeKind.Local).AddTicks(9520), "Bennie", "Lehner", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1996, 10, 12, 6, 29, 39, 95, DateTimeKind.Local).AddTicks(4553), "Laurie", "O'Hara", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1989, 9, 12, 14, 13, 33, 549, DateTimeKind.Local).AddTicks(539), "Olive", "Purdy", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1952, 5, 26, 17, 47, 23, 461, DateTimeKind.Local).AddTicks(8085), "Seth", "Krajcik", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1987, 7, 21, 1, 58, 50, 552, DateTimeKind.Local).AddTicks(6072), "Beverly", "Dicki", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1997, 4, 14, 21, 44, 40, 161, DateTimeKind.Local).AddTicks(1183), "Claire", "Greenfelder", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1988, 3, 30, 0, 17, 25, 226, DateTimeKind.Local).AddTicks(5543), "Moses", "Hodkiewicz", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1964, 2, 7, 2, 0, 14, 979, DateTimeKind.Local).AddTicks(6112), "Catherine", "Graham", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1954, 10, 8, 23, 42, 45, 470, DateTimeKind.Local).AddTicks(4827), "Clifford", "Senger", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1974, 12, 21, 2, 44, 1, 231, DateTimeKind.Local).AddTicks(4819), "Dave", "Bernier", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1964, 1, 14, 19, 37, 45, 857, DateTimeKind.Local).AddTicks(5430), "Isabel", "Mraz", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1980, 6, 18, 3, 12, 6, 443, DateTimeKind.Local).AddTicks(9039), "Geoffrey", "Lehner", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1958, 7, 28, 5, 10, 53, 685, DateTimeKind.Local).AddTicks(7533), "Gertrude", "Price", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateOfBirth", "FirstName", "LastName" },
                values: new object[] { new DateTime(1963, 2, 11, 21, 15, 18, 573, DateTimeKind.Local).AddTicks(9173), "Emilio", "Rath" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(2000, 11, 6, 4, 19, 14, 661, DateTimeKind.Local).AddTicks(8196), "Johnnie", "Deckow", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateOfBirth", "FirstName", "LastName", "SalutationId" },
                values: new object[] { new DateTime(1963, 11, 23, 9, 50, 53, 88, DateTimeKind.Local).AddTicks(6732), "Shari", "Gutkowski", 1 });
        }
    }
}
