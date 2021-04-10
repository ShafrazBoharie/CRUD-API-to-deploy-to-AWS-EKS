using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Moq;
using TPICAP.TechChallenge.Data.Entities;
using TPICAP.TechChallenge.Data.Services;
using TPICAP.TechChallenge.Infrastructure.Mappers;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Infrastructure.Services;
using TPICAP.TechChallenge.Model.Models;
using Xunit;
using Person = TPICAP.TechChallenge.Data.Entities.Person;

namespace TPICAP.TechChallenge.Infrastructure.Tests.Services
{
    public class PersonServiceTests
    {
        private readonly Mock<IPersonRepository> _personRepository;
        private readonly Mock<ISalutationRepository> _salutationRepository;
        private readonly PersonService _sut;

        public PersonServiceTests()
        {
            _salutationRepository = new Mock<ISalutationRepository>();
            ConfigureSalutationRepositoryMock();

            _personRepository = new Mock<IPersonRepository>();
            var personEntityToPersonDtoMapper = new PersonEntityToPersonDtoMapper();
            var personCreationDtoToPersonEntityMapper =
                new PersonCreationDtoToPersonEntityMapper(_salutationRepository.Object);
            var personUpdateDtoToPersonEntityMapper =
                new PersonUpdateDtoToPersonEntityMapper(_salutationRepository.Object);

            _sut = new PersonService(_personRepository.Object, personEntityToPersonDtoMapper,
                personCreationDtoToPersonEntityMapper, personUpdateDtoToPersonEntityMapper);
        }

        private void ConfigureSalutationRepositoryMock()
        {
            _salutationRepository.Setup(x => x.GetSalutationByName("Mr"))
                .ReturnsAsync(new Salutation {SalutationId = 1, SalutationName = "Mr"});
        }

        [Fact]
        public async Task WhenPersonIsAvailableInTheDatabase_ShouldReturnPersonsCollection()
        {
            _personRepository
                .Setup(x => x.GetPersons(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>(),
                    It.IsAny<string>())).ReturnsAsync(GeneratePersons());

            var results = await _sut.GetPersons(new PersonsResourceParameters());

            results.Count().Should().Be(3);
        }

        [Fact]
        public async Task WhenPersonsNotAvailableInTheDatabase_ShouldReturnEmptyCollection()
        {
            var personsResourceParameters = new PersonsResourceParameters
                {PageSize = 10, OrderBy = "Firstname", PageNumber = 1};
            var results = await _sut.GetPersons(personsResourceParameters);

            results.Count().Should().Be(0);
        }

        [Fact]
        public async Task WhenPersonIdIsExistInTheDatabase_ShouldReturnPersonDto()
        {
            var personEntityToReturn = GeneratePersons().First();
            _personRepository.Setup(x => x.GetPerson(It.IsAny<int>())).ReturnsAsync(personEntityToReturn);
            var personIdtoRetrieve = personEntityToReturn.Id;

            var results = await _sut.GetPerson(personIdtoRetrieve, "");

            results.Should().NotBeNull();
            results.Id.Should().Be(personIdtoRetrieve);
        }

        [Fact]
        public async Task WhenPersonIdIsNotExistExistInTheDatabase_ShouldReturnNull()
        {
            var results = await _sut.GetPerson(999, "");

            results.Should().BeNull();
        }

        [Fact]
        public async Task WhenInsertingAPerson_PersonEntityIsNotValid_ShouldThrowException()
        {
            await _sut.Invoking(x => x.AddPerson(null)).Should().ThrowAsync<InvalidDataException>()
                .WithMessage("Unable To Map To Person Entity");
        }

        [Fact]
        public async Task WhenInsertingAPerson_PersonEntityIsSuccessfullyCalled_AddPersonMethodShouldBeCalledOnce()
        {
            var personForCreationDto = CreatePersonForCreationDto();
            _personRepository.Setup(x => x.AddPerson(It.IsAny<Person>())).ReturnsAsync(GeneratePersons().FirstOrDefault);

            var result = await _sut.AddPerson(personForCreationDto);

            _personRepository.Verify(x => x.AddPerson(It.IsAny<Person>()), Times.Once);
        }

        [Fact]
        public async Task WhenAPersonIsNotExist_WhenDeletingThePerson_Will_ReturnFalse()
        {
            var result = await _sut.DeletePerson(999);

            result.Should().BeFalse();
        }


        [Fact]
        public async Task WhenAPersonIsExist_WhenDeletingThePerson_Will_ReturnTrue()
        {
            var person = GeneratePersons().First();

            _personRepository.Setup(x => x.GetPerson(It.IsAny<int>())).ReturnsAsync(person);

            var result = await _sut.DeletePerson(person.Id);

            _personRepository.Verify(x => x.DeletePerson(It.IsAny<Person>()), Times.Once);
            result.Should().BeTrue();
        }


        [Fact]
        public async Task WhenEntityIsAlreadyExist_ItShouldUpdateTheEntity()
        {
            var personForUpdateDto = CreatePersonForUpdateDto();
            _personRepository.Setup(x => x.GetPerson(It.IsAny<int>())).ReturnsAsync(GeneratePersons().First);

            var result = await _sut.UpdatePerson(personForUpdateDto);
            _personRepository.Verify(x => x.AddPerson(It.IsAny<Person>()), Times.Once);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task WhenEntityIsNotExist_ItShouldCreateNewEntity()
        {
            var personForUpdateDto = CreatePersonForUpdateDto();
            _personRepository.Setup(x => x.PersonExist(It.IsAny<int>())).ReturnsAsync(true);
            _personRepository.Setup(x => x.GetPerson(It.IsAny<int>())).ReturnsAsync(GeneratePersons().First);

            var result = await _sut.UpdatePerson(personForUpdateDto);

            _personRepository.Verify(x => x.UpdatePerson(It.IsAny<Person>()), Times.Once);

            result.Should().NotBeNull();
        }

        private IEnumerable<Person> GeneratePersons()
        {
            return new Bogus.Faker<Person>()
                .RuleFor(x => x.Id, x => x.IndexVariable)
                .RuleFor(x => x.FirstName, x => x.Name.FindName())
                .RuleFor(x => x.LastName, x => x.Name.LastName())
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Salutation, new Salutation {SalutationId = 1, SalutationName = "Mr"})
                .Generate(3);
        }

        private PersonForCreationDto CreatePersonForCreationDto()
        {
            return new Faker<PersonForCreationDto>()
                .RuleFor(x => x.FirstName, x => x.Name.FindName())
                .RuleFor(x => x.LastName, x => x.Name.LastName())
                .RuleFor(x => x.DOB, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Salutation, "Mr")
                .Generate();
        }

        private PersonForUpdateDto CreatePersonForUpdateDto()
        {
            return new Faker<PersonForUpdateDto>()
                .RuleFor(x => x.Id, x => 1)
                .RuleFor(x => x.FirstName, x => x.Name.FindName())
                .RuleFor(x => x.LastName, x => x.Name.LastName())
                .RuleFor(x => x.DOB, x => x.Person.DateOfBirth)
                .RuleFor(x => x.Salutation, "Mr")
                .Generate();
        }
    }
}