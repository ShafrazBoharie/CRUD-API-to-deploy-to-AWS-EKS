using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task GivenThePersonsTableIsEmpty_Should_Return_EmptyCollectionOfPersons()
        {
            DeleteAllPersonsRecords();

            var result = await _sut.GetPersons(1, 1, true);
                
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenPersonsTableHasRecords_Should_Return_PersonsCollection()
        {
            var numberOfTests = 3;
            AddTestPersonsToDatabase(_context, numberOfTests);

            var personsCountInDatabase = _context.People.Count();

            var persons =await  _sut.GetPersons(1, 1, true);

            var result = 
            persons.Count().Should().Be(personsCountInDatabase);
        }


        [Fact]
        public async Task GivenPersonTableHasRecords_WithValidPersonId_ShouldReturn_Person()
        {
            AddTestPersonsToDatabase(_context, 1);
            var expectedPersonId = _context.People.FirstOrDefault().Id;

            var result = await _sut.GetPerson(expectedPersonId);

            result.Id.Should().Be(expectedPersonId);
        }

        [Theory]
        [InlineData(9999)]
        public async Task GivenPersonTableHasRecords_WhenPersonId_NotExist_ShouldReturn_Null(int expectedPersonId)
        {
            AddTestPersonsToDatabase(_context, 1);

            var result = await _sut.GetPerson(expectedPersonId);

            result.Should().BeNull();
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GivenPersonTableHasRecords_WithInValidPersonId_ShouldReturn_Null(int expectedPersonId)
        {
            AddTestPersonsToDatabase(_context, 1);
             await _sut.Invoking(x =>x.GetPerson(expectedPersonId)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenPersonToBeAdded_Is_Null_Should_Throw_ArgumentNullException()
        {
            await _sut.Invoking(x => x.AddPerson(null)).Should().ThrowAsync<ArgumentNullException>();
        }


        [Fact]
        public async Task GivenPersonToBeAdded_IsValid_Should_InsertThePerson_ToTheDatabase()
        {
            var person = GeneratePersons(1).First();

            var result = await _sut.AddPerson(person);

            var insertedPerson = await _context.People.FirstOrDefaultAsync(x => x.DateOfBirth == person.DateOfBirth
                                                                     && x.FirstName == person.FirstName &&
                                                                     x.LastName == person.LastName &&
                                                                     x.Salutation.Equals(person.Salutation));
            result.Should().BeEquivalentTo(insertedPerson);
        }


        [Fact]
        public void GivenThePersonParameterIsNull_WhenDeletingThePerson_Should_ThrowNullException()
        {
            _sut.Invoking(x => x.DeletePerson(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenThePersonObjectIsAvailable_WhenDeletingThePerson_ShouldDeleteThePersonSuccessfully()
        {
            var person = GeneratePersons(1).First();

            await _sut.AddPerson(person);

            await _sut.DeletePerson(person);

            _context.People.FirstOrDefault(x => x.Id == person.Id).Should().BeNull();
        }

        [Theory]
        [InlineData("NewFirstName")]
        [InlineData("XYZ")]
        public async Task GivenNewPersonData_WhenUpdatingThePerson_ShouldUpdatedOnTheDatabase(string firstName)
        {
            var person = GeneratePersons(1).First();
            await _sut.AddPerson(person);

            person.FirstName = firstName;
            var result = await _sut.UpdatePerson(person);

            var personInDB = _context.People.FirstOrDefault(x => x.Id == person.Id);

            personInDB.FirstName.Should().Be(firstName);
            result.Should().BeEquivalentTo(personInDB);
        }

        [Fact]
        public async Task GivenPersonId_WhenCheckingFOrPersonIsExist_Shouldreturn_True()
        {
            var person = GeneratePersons(1).First();
            await _sut.AddPerson(person);

            var result = await _sut.PersonExist(person.Id);
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(999)]
        public async Task GivenInvalidPersonId_WhenCheckingFOrPersonIsExist_Shouldreturn_False(int personId)
        {
            var result = await _sut.PersonExist(personId);
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GivenPersonIsExist_WhenCheckingFOrPersonIsExist_Shouldreturn_True()
        {
            var person = GeneratePersons(1).First();
            await _sut.AddPerson(person);

            var result = await _sut.PersonExist(person);
            result.Should().BeTrue();
        }


        [Fact]
        public async Task GivenNullPerson_WhenCheckingFOrPersonIsExist_Shouldreturn_False()
        {
           await _sut.Invoking(x => x.PersonExist(null)).Should().ThrowAsync<ArgumentNullException>();
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
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Salutation, f => f.PickRandom(salutations))
                .Generate(noOfPersons);
        }
    }
}