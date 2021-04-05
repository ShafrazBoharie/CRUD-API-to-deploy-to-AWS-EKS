using Bogus;
using FluentAssertions;
using Moq;
using TPICAP.TechChallenge.Data.Entities;
using TPICAP.TechChallenge.Data.Services;
using TPICAP.TechChallenge.Infrastructure.Mappers;
using TPICAP.TechChallenge.Model.Models;
using Xunit;
using Person = TPICAP.TechChallenge.Data.Entities.Person;

namespace TPICAP.TechChallenge.Infrastructure.Tests.Mappers
{
    public class PersonCreationDtoToPersonEntityMapperTests
    {
        private readonly PersonCreationDtoToPersonEntityMapper mapper;
        private readonly Mock<ISalutationRepository> salutationRepo;

        public PersonCreationDtoToPersonEntityMapperTests()
        {
            salutationRepo = new Mock<ISalutationRepository>();
            mapper = new PersonCreationDtoToPersonEntityMapper(salutationRepo.Object);
            ConfigureSalutationMock();
        }

        [Fact]
        public void ShouldMap_CreatePersonForCreationDto_To_PersonEntity()
        {
            var personCreationDto = CreatePersonForCreationDto();
            var result = mapper.Map(personCreationDto);

            result.Should().BeEquivalentTo(ExpectedResult(personCreationDto));
        }

        [Fact]
        public void When_CreatePersonForCreationDto_Is_Null_ShouldReturn_Null()
        {
            var result = mapper.Map(null);
            result.Should().BeNull();
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

        private void ConfigureSalutationMock()
        {
            salutationRepo.Setup(x => x.GetSalutationByName(It.IsAny<string>()))
                .Returns(new Salutation {SalutationId = 1, SalutationName = "Mr"});
        }

        private Person ExpectedResult(PersonForCreationDto dto)
        {
            return new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DOB,
                Salutation = new Salutation {SalutationId = 1, SalutationName = "Mr"}
            };
        }
    }
}