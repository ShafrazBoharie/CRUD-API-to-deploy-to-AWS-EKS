using System.Threading.Tasks;
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
        private readonly PersonCreationDtoToPersonEntityMapper _mapper;
        private readonly Mock<ISalutationRepository> _salutationRepo;

        public PersonCreationDtoToPersonEntityMapperTests()
        {
            _salutationRepo = new Mock<ISalutationRepository>();
            _mapper = new PersonCreationDtoToPersonEntityMapper(_salutationRepo.Object);
        }

        [Fact]
        public async Task ShouldMap_CreatePersonForCreationDto_To_PersonEntity()
        {
            _salutationRepo.Setup(x => x.GetSalutationByName(It.IsAny<string>()))
                .ReturnsAsync(new Salutation { SalutationName = "Mr", SalutationId = 1 });

            var personCreationDto = CreatePersonForCreationDto();

            var result =await _mapper.Map(personCreationDto);

            result.Should().BeEquivalentTo(ExpectedResult(personCreationDto));
        }

        [Fact]
        public async Task When_CreatePersonForCreationDto_Is_Null_ShouldReturn_Null()
        {
            var result = await _mapper.Map(null);
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