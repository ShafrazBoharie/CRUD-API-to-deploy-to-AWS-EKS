using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TPICAP.TechChallenge.Data.Entities;

namespace TPICAP.TechChallenge.Data
{
    public class PeopleContext : DbContext
    {
        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Salutation> Salutations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salutation>().HasData(
                new List<Salutation>
                {
                    new() {SalutationId = 1, SalutationName = "Mr"},
                    new() {SalutationId = 2, SalutationName = "Mrs"},
                    new() {SalutationId = 3, SalutationName = "Miss"},
                    new() {SalutationId = 4, SalutationName = "Dr"},
                    new() {SalutationId = 5, SalutationName = "Sir"},
                    new() {SalutationId = 6, SalutationName = "Lord"},
                    new() {SalutationId = 7, SalutationName = "Lady"},
                    new() {SalutationId = 8, SalutationName = "Prof"}
                }
            );

            modelBuilder.Entity<Person>().HasData(
                new Bogus.Faker<Person>()
                    .RuleFor(x => x.Id, x => 1 + x.IndexVariable++)
                    .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                    .RuleFor(x => x.LastName, x => x.Person.LastName)
                    .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                    .RuleFor(x => x.SalutationId, x => x.Random.Int(1, 8))
                    .Generate(30)
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}