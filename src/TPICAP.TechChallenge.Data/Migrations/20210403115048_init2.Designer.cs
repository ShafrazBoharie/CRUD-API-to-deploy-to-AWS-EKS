﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPICAP.TechChallenge.Data;

namespace TPICAP.TechChallenge.Data.Migrations
{
    [DbContext(typeof(PeopleContext))]
    [Migration("20210403115048_init2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TPICAP.TechChallenge.Data.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SalutationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalutationId");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1992, 4, 8, 23, 42, 19, 59, DateTimeKind.Local).AddTicks(2693),
                            FirstName = "Jerald",
                            LastName = "Pouros",
                            SalutationId = 2
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1968, 5, 14, 9, 21, 47, 1, DateTimeKind.Local).AddTicks(9463),
                            FirstName = "Matt",
                            LastName = "Hirthe",
                            SalutationId = 5
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1951, 6, 22, 3, 4, 36, 369, DateTimeKind.Local).AddTicks(6408),
                            FirstName = "Pat",
                            LastName = "Bradtke",
                            SalutationId = 5
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1967, 2, 10, 23, 18, 26, 741, DateTimeKind.Local).AddTicks(5206),
                            FirstName = "Shelley",
                            LastName = "DuBuque",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 5,
                            DateOfBirth = new DateTime(1997, 11, 28, 19, 57, 39, 117, DateTimeKind.Local).AddTicks(8341),
                            FirstName = "Valerie",
                            LastName = "Runolfsdottir",
                            SalutationId = 1
                        },
                        new
                        {
                            Id = 6,
                            DateOfBirth = new DateTime(1960, 2, 25, 19, 56, 9, 327, DateTimeKind.Local).AddTicks(2440),
                            FirstName = "Myron",
                            LastName = "Upton",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 7,
                            DateOfBirth = new DateTime(1975, 10, 10, 18, 10, 40, 960, DateTimeKind.Local).AddTicks(5175),
                            FirstName = "Darrell",
                            LastName = "Schinner",
                            SalutationId = 5
                        },
                        new
                        {
                            Id = 8,
                            DateOfBirth = new DateTime(1996, 2, 26, 20, 36, 52, 281, DateTimeKind.Local).AddTicks(370),
                            FirstName = "Francis",
                            LastName = "Schultz",
                            SalutationId = 7
                        },
                        new
                        {
                            Id = 9,
                            DateOfBirth = new DateTime(1997, 1, 18, 3, 4, 47, 140, DateTimeKind.Local).AddTicks(9558),
                            FirstName = "Eugene",
                            LastName = "Gutkowski",
                            SalutationId = 3
                        },
                        new
                        {
                            Id = 10,
                            DateOfBirth = new DateTime(1973, 6, 4, 13, 0, 51, 564, DateTimeKind.Local).AddTicks(9953),
                            FirstName = "Tracey",
                            LastName = "Casper",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 11,
                            DateOfBirth = new DateTime(1994, 10, 25, 18, 19, 5, 237, DateTimeKind.Local).AddTicks(1675),
                            FirstName = "Kirk",
                            LastName = "Sporer",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 12,
                            DateOfBirth = new DateTime(1962, 12, 23, 18, 37, 50, 447, DateTimeKind.Local).AddTicks(1020),
                            FirstName = "Opal",
                            LastName = "Steuber",
                            SalutationId = 8
                        },
                        new
                        {
                            Id = 13,
                            DateOfBirth = new DateTime(1990, 4, 16, 5, 32, 38, 429, DateTimeKind.Local).AddTicks(932),
                            FirstName = "Marjorie",
                            LastName = "Davis",
                            SalutationId = 2
                        },
                        new
                        {
                            Id = 14,
                            DateOfBirth = new DateTime(1986, 1, 8, 5, 18, 15, 650, DateTimeKind.Local).AddTicks(2541),
                            FirstName = "Kent",
                            LastName = "Skiles",
                            SalutationId = 3
                        },
                        new
                        {
                            Id = 15,
                            DateOfBirth = new DateTime(1983, 1, 12, 18, 55, 7, 207, DateTimeKind.Local).AddTicks(8473),
                            FirstName = "Lola",
                            LastName = "Parisian",
                            SalutationId = 5
                        },
                        new
                        {
                            Id = 16,
                            DateOfBirth = new DateTime(1965, 1, 8, 6, 59, 57, 917, DateTimeKind.Local).AddTicks(7255),
                            FirstName = "Ernesto",
                            LastName = "D'Amore",
                            SalutationId = 2
                        },
                        new
                        {
                            Id = 17,
                            DateOfBirth = new DateTime(1968, 6, 17, 16, 8, 42, 493, DateTimeKind.Local).AddTicks(581),
                            FirstName = "Delores",
                            LastName = "Simonis",
                            SalutationId = 4
                        },
                        new
                        {
                            Id = 18,
                            DateOfBirth = new DateTime(1988, 10, 22, 1, 21, 10, 428, DateTimeKind.Local).AddTicks(9826),
                            FirstName = "Marguerite",
                            LastName = "Haley",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 19,
                            DateOfBirth = new DateTime(1980, 8, 8, 12, 15, 22, 133, DateTimeKind.Local).AddTicks(6915),
                            FirstName = "Joe",
                            LastName = "Murphy",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 20,
                            DateOfBirth = new DateTime(1961, 10, 1, 21, 23, 28, 524, DateTimeKind.Local).AddTicks(5339),
                            FirstName = "Alberta",
                            LastName = "Gutmann",
                            SalutationId = 3
                        },
                        new
                        {
                            Id = 21,
                            DateOfBirth = new DateTime(1996, 10, 6, 19, 24, 31, 536, DateTimeKind.Local).AddTicks(5526),
                            FirstName = "Gwendolyn",
                            LastName = "Terry",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 22,
                            DateOfBirth = new DateTime(1959, 1, 4, 3, 42, 55, 373, DateTimeKind.Local).AddTicks(6584),
                            FirstName = "Joy",
                            LastName = "Marks",
                            SalutationId = 3
                        },
                        new
                        {
                            Id = 23,
                            DateOfBirth = new DateTime(1992, 9, 12, 2, 48, 35, 99, DateTimeKind.Local).AddTicks(5913),
                            FirstName = "Lee",
                            LastName = "Tromp",
                            SalutationId = 7
                        },
                        new
                        {
                            Id = 24,
                            DateOfBirth = new DateTime(1989, 11, 14, 16, 11, 11, 365, DateTimeKind.Local).AddTicks(211),
                            FirstName = "Carroll",
                            LastName = "Kohler",
                            SalutationId = 2
                        },
                        new
                        {
                            Id = 25,
                            DateOfBirth = new DateTime(1955, 6, 20, 18, 7, 47, 596, DateTimeKind.Local).AddTicks(1356),
                            FirstName = "Frederick",
                            LastName = "Hahn",
                            SalutationId = 7
                        },
                        new
                        {
                            Id = 26,
                            DateOfBirth = new DateTime(1966, 3, 30, 21, 45, 24, 460, DateTimeKind.Local).AddTicks(1688),
                            FirstName = "Candice",
                            LastName = "Ziemann",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 27,
                            DateOfBirth = new DateTime(1952, 8, 29, 7, 59, 12, 32, DateTimeKind.Local).AddTicks(7291),
                            FirstName = "Ramiro",
                            LastName = "Gulgowski",
                            SalutationId = 6
                        },
                        new
                        {
                            Id = 28,
                            DateOfBirth = new DateTime(1966, 10, 23, 22, 25, 3, 421, DateTimeKind.Local).AddTicks(726),
                            FirstName = "Roy",
                            LastName = "Thiel",
                            SalutationId = 1
                        },
                        new
                        {
                            Id = 29,
                            DateOfBirth = new DateTime(1960, 1, 21, 7, 35, 41, 125, DateTimeKind.Local).AddTicks(4686),
                            FirstName = "Roman",
                            LastName = "Crona",
                            SalutationId = 2
                        },
                        new
                        {
                            Id = 30,
                            DateOfBirth = new DateTime(1962, 2, 9, 0, 50, 38, 799, DateTimeKind.Local).AddTicks(2778),
                            FirstName = "Sophia",
                            LastName = "Hoeger",
                            SalutationId = 4
                        });
                });

            modelBuilder.Entity("TPICAP.TechChallenge.Data.Entities.Salutation", b =>
                {
                    b.Property<int>("SalutationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SalutationName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SalutationId");

                    b.ToTable("Salutations");

                    b.HasData(
                        new
                        {
                            SalutationId = 1,
                            SalutationName = "Mr"
                        },
                        new
                        {
                            SalutationId = 2,
                            SalutationName = "Mrs"
                        },
                        new
                        {
                            SalutationId = 3,
                            SalutationName = "Miss"
                        },
                        new
                        {
                            SalutationId = 4,
                            SalutationName = "Dr"
                        },
                        new
                        {
                            SalutationId = 5,
                            SalutationName = "Sir"
                        },
                        new
                        {
                            SalutationId = 6,
                            SalutationName = "Lord"
                        },
                        new
                        {
                            SalutationId = 7,
                            SalutationName = "Lady"
                        },
                        new
                        {
                            SalutationId = 8,
                            SalutationName = "Prof"
                        });
                });

            modelBuilder.Entity("TPICAP.TechChallenge.Data.Entities.Person", b =>
                {
                    b.HasOne("TPICAP.TechChallenge.Data.Entities.Salutation", "Salutation")
                        .WithMany()
                        .HasForeignKey("SalutationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salutation");
                });
#pragma warning restore 612, 618
        }
    }
}
