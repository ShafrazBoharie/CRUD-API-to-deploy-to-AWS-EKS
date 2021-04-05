using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TPICAP.TechChallenge.Data.Entities;
using TPICAP.TechChallenge.Data.Services;
using Xunit;

namespace TPICAP.TechChallenge.Data.Tests.Services
{
    public class PersonRepositoryTests : IDisposable
    {
        private readonly PeopleContext _context;
        private readonly PersonRepository _sut;

        public PersonRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<PeopleContext>();
            builder.UseInMemoryDatabase("InMemoryDBPerson");
            _context = new PeopleContext(builder.Options);
            _context.Database.EnsureCreated();


            var salutationRepo = new SalutationRepository(_context);
            _sut = new PersonRepository(_context, salutationRepo);
        }


        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void GivenThePersonsTableIsEmpty_Should_Return_EmptyCollectionOfPersons()
        {
            DeleteAllPersonsRecords();

            _sut.GetPersons(1, 1, true).Should().BeEmpty();
        }

        [Fact]
        public void GivenPersonsTableHasRecords_Should_Return_PersonsCollection()
        {
            var numberOfTests = 3;
            AddTestPersonsToDatabase(_context, numberOfTests);

            var personsCountInDatabase = _context.People.Count();

            var persons = _sut.GetPersons(1, 1, true);

            persons.Count().Should().Be(personsCountInDatabase);
        }


        [Fact]
        public void GivenPersonTableHasRecords_WithValidPersonId_ShouldReturn_Person()
        {
            AddTestPersonsToDatabase(_context, 1);
            var expectedPersonId = _context.People.FirstOrDefault().Id;

            var result = _sut.GetPerson(expectedPersonId);

            result.Id.Should().Be(expectedPersonId);
        }

        [Theory]
        [InlineData(9999)]
        public void GivenPersonTableHasRecords_WhenPersonId_NotExist_ShouldReturn_Null(int expectedPersonId)
        {
            AddTestPersonsToDatabase(_context, 1);

            var result = _sut.GetPerson(expectedPersonId);

            result.Should().BeNull();
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenPersonTableHasRecords_WithInValidPersonId_ShouldReturn_Null(int expectedPersonId)
        {
            AddTestPersonsToDatabase(_context, 1);
            _sut.Invoking(x => x.GetPerson(expectedPersonId)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GivenPersonToBeAdded_Is_Null_Should_Throw_ArgumentNullException()
        {
            _sut.Invoking(x => x.AddPerson(null)).Should().Throw<ArgumentNullException>();
        }


        [Fact]
        public void GivenPersonToBeAdded_IsValid_Should_InsertThePerson_ToTheDatabase()
        {
            var person = GeneratePersons(1).First();

            _sut.AddPerson(person);
            var result = _sut.Save();

            var insertedPerson = _context.People.FirstOrDefault(x => x.DateOfBirth == person.DateOfBirth
                                                                     && x.FirstName == person.FirstName &&
                                                                     x.LastName == person.LastName &&
                                                                     x.Salutation.Equals(person.Salutation));

            result.Should().BeTrue();
            insertedPerson.Should().NotBeNull();
        }


        [Fact]
        public void GivenThePersonParameterIsNull_WhenDeletingThePerson_Should_ThrowNullException()
        {
            _sut.Invoking(x => x.DeletePerson(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GivenThePersonObjectIsAvailable_WhenDeletingThePerson_ShouldDeleteThePersonSuccessfully()
        {
            var person = GeneratePersons(1).First();

            _sut.AddPerson(person);
            var result = _sut.Save();

            _sut.DeletePerson(person);
            _sut.Save();

            _context.People.FirstOrDefault(x => x.Id == person.Id).Should().BeNull();
        }

        [Theory]
        [InlineData("NewFirstName")]
        [InlineData("XYZ")]
        public void GivenNewPersonData_WhenUpdatingThePerson_ShouldUpdatedOnTheDatabase(string firstName)
        {
            var person = GeneratePersons(1).First();
            _sut.AddPerson(person);
            _sut.Save();

            person.FirstName = firstName;
            _sut.UpdatePerson(person);
            var result = _sut.Save();

            result.Should().BeTrue();
            _context.People.FirstOrDefault(x => x.Id == person.Id).FirstName.Should().Be(firstName);
        }

        [Fact]
        public void GivenPersonId_WhenCheckingFOrPersonIsExist_Shouldreturn_True()
        {
            var person = GeneratePersons(1).First();
            _sut.AddPerson(person);
            _sut.Save();

            _sut.PersonExist(person.Id).Should().BeTrue();
        }

        [Theory]
        [InlineData(999)]
        public void GivenInvalidPersonId_WhenCheckingFOrPersonIsExist_Shouldreturn_False(int personId)
        {
            _sut.PersonExist(personId).Should().BeFalse();
        }

        [Fact]
        public void GivenPersonIsExist_WhenCheckingFOrPersonIsExist_Shouldreturn_True()
        {
            var person = GeneratePersons(1).First();
            _sut.AddPerson(person);
            _sut.Save();

            _sut.PersonExist(person).Should().BeTrue();
        }


        [Fact]
        public void GivenNullPerson_WhenCheckingFOrPersonIsExist_Shouldreturn_False()
        {
            _sut.Invoking(x => x.PersonExist(null)).Should().Throw<ArgumentNullException>();
        }


        private void DeleteAllPersonsRecords()
        {
            foreach (var p in _context.People) _context.Remove(p);
            _context.SaveChanges();
        }


        private void AddTestPersonsToDatabase(PeopleContext context, int noOfPersons)
        {
            var persons = GeneratePersons(noOfPersons);
            ;

            context.People.AddRange(persons);
            context.SaveChanges();
        }

        private IEnumerable<Person> GeneratePersons(int noOfPersons)
        {
            var salutations = _context.Salutations.ToList();

            return new Bogus.Faker<Person>()
                //  .RuleFor(x => x.SalutationId, context.People.Any() ? context.People.Max(m => m.SalutationId) + 1 : 1)
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Salutation, f => f.PickRandom(salutations))
                .Generate(noOfPersons);
        }
    }
}