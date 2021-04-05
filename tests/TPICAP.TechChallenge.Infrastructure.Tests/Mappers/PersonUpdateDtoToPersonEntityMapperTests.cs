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
    public class PersonUpdateDtoToPersonEntityMapperTests
    {
        private readonly PersonUpdateDtoToPersonEntityMapper mapper;
        private readonly Mock<ISalutationRepository> salutationRepo;

        public PersonUpdateDtoToPersonEntityMapperTests()
        {
            salutationRepo = new Mock<ISalutationRepository>();
            mapper = new PersonUpdateDtoToPersonEntityMapper(salutationRepo.Object);
            ConfigureSalutationMock();
        }

        [Fact]
        public void ShouldMap_CreatePersonForUpdateDto_To_PersonEntity()
        {
            var personForUpdateDto = CreatePersonForUpdateDto();
            var result = mapper.Map(personForUpdateDto);

            result.Should().BeEquivalentTo(ExpectedResult(personForUpdateDto));
        }

        [Fact]
        public void When_PersonUpdateDtoToPersonEntity_Is_Null_ShouldReturn_Null()
        {
            var result = mapper.Map(null);
            result.Should().BeNull();
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

        private void ConfigureSalutationMock()
        {
            salutationRepo.Setup(x => x.GetSalutationByName(It.IsAny<string>()))
                .Returns(new Salutation {SalutationId = 1, SalutationName = "Mr"});
        }

        private Person ExpectedResult(PersonForUpdateDto dto)
        {
            return new()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DOB,
                Salutation = new Salutation {SalutationId = 1, SalutationName = "Mr"}
            };
        }
    }
}