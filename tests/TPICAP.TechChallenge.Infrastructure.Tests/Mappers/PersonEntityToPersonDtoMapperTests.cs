using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TPICAP.TechChallenge.Data.Entities;
using TPICAP.TechChallenge.Infrastructure.Mappers;
using TPICAP.TechChallenge.Model.Models;
using Xunit;

namespace TPICAP.TechChallenge.Infrastructure.Tests.Mappers
{
    public class PersonEntityToPersonDtoMapperTests
    {
        private readonly PersonEntityToPersonDtoMapper _mapper;

        public PersonEntityToPersonDtoMapperTests()
        {
            _mapper = new PersonEntityToPersonDtoMapper();
        }

        [Fact]
        public void ShouldMap_PersonEntityToPersonDto()
        {
            var personEntity = CreatePersonEntity();
            var result = _mapper.Map(personEntity);

            result.Should().BeEquivalentTo(ExpectedResultSingleObject(personEntity));
        }

        [Fact]
        public void ShouldMap_PersonEntityCollectionToPersonDto()
        {
            var personEntityCollection = CreatePersonEntityCollection();
            var result = _mapper.Map(personEntityCollection);

            result.Should().BeEquivalentTo(ExpectedResultOfCollectionEntity(personEntityCollection));
        }

        [Fact]
        public void When_PersonEntityCollection_Is_Empty_ShouldReturn_EmptyCollection()
        {
            var result = _mapper.Map(new List<Person>());
            result.Should().BeEmpty();
        }

        [Fact]
        public void When_PersonEntity_Is_Empty_ShouldReturn_Null()
        {
            Person person = null;
            var result = _mapper.Map(person);
            result.Should().BeNull();
        }

        private Person CreatePersonEntity()
        {
            return new Bogus.Faker<Person>()
                .RuleFor(x => x.Id, 1)
                .RuleFor(x => x.FirstName, x => x.Name.FindName())
                .RuleFor(x => x.LastName, x => x.Name.LastName())
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Salutation, new Salutation {SalutationId = 1, SalutationName = "Mr"})
                .Generate();
        }

        private IEnumerable<Person> CreatePersonEntityCollection()
        {
            return new Bogus.Faker<Person>()
                .RuleFor(x => x.Id, x => x.IndexVariable)
                .RuleFor(x => x.FirstName, x => x.Name.FindName())
                .RuleFor(x => x.LastName, x => x.Name.LastName())
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Salutation, new Salutation {SalutationId = 1, SalutationName = "Mr"})
                .Generate(3);
        }

        private PersonDto ExpectedResultSingleObject(Person person)
        {
            return new()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Salutation = person.Salutation.SalutationName
            };
        }

        private IEnumerable<PersonDto> ExpectedResultOfCollectionEntity(IEnumerable<Person> person)
        {
            return person.Select(person => new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Salutation = person.Salutation.SalutationName
            });
        }
    }
}