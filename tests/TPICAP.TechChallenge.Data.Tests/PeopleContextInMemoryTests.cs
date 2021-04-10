using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TPICAP.TechChallenge.Data.Entities;
using Xunit;

namespace TPICAP.TechChallenge.Data.Tests
{
    public class PeopleContextInMemoryTests : IDisposable
    {
        private readonly PeopleContext _context;

        public PeopleContextInMemoryTests()
        {
            var builder = new DbContextOptionsBuilder<PeopleContext>();
            builder.UseInMemoryDatabase("InMemoryDB");
            _context = new PeopleContext(builder.Options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public void WhenSalutationsSeeded_SalutationCount_ShouldBeMoreThanOne()
        {
            var salutationCount = _context.Salutations.Count();

            salutationCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public void PeopleContext_ShouldBeAbleToInsertPerson()
        {
            var countBeforeAddPerson = _context.People.Count();
            var person = AddaPersonToDatabase(_context);

            person.Id.Should().BeGreaterThan(0);

            _context.People.Count().Should().Be(countBeforeAddPerson + 1);
        }


        [Fact]
        public void PeopleContext_ShouldBeAbleToDeletePerson()
        {
            var person = AddaPersonToDatabase(_context);
            var createdPersonId = person.Id;

            _context.People.Remove(person);
            _context.SaveChanges();

            _context.People.FirstOrDefault(x=>x.Id==createdPersonId).Should().BeNull();
        }

        [Fact]
        public void PeopleContext_ShouldBeAbleToUpdatePerson()
        {
            var person = AddaPersonToDatabase(_context);
            var createdPersonId = person.Id;

            person.FirstName = "Updated FirstName";

            _context.Update(person);
            _context.SaveChanges();

            var updatedPerson = _context.People.FirstOrDefault(x => x.Id == createdPersonId);

            updatedPerson.FirstName.Should().Be("Updated FirstName");
        }

        private Person AddaPersonToDatabase(PeopleContext context)
        {
            var person = GeneratePersonObject(context);
            context.People.Add(person);
            context.SaveChanges();
            return person;
        }

        private Person GeneratePersonObject(PeopleContext context)
        {
            var salutation = context.Salutations.FirstOrDefault();

            var maxId = context.People.Any() ? context.People.Max(m => m.Id) + 1 : 1;
            return new Bogus.Faker<Person>()
                .RuleFor(x => x.Id, x=>maxId+x.IndexVariable++)
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Salutation, salutation)
                .Generate();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}