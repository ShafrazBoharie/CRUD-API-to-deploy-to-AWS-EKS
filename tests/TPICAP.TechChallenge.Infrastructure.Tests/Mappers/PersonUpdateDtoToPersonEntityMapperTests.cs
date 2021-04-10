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
    public class PersonUpdateDtoToPersonEntityMapperTests
    {
        private readonly PersonUpdateDtoToPersonEntityMapper _mapper;
        private readonly Mock<ISalutationRepository> _salutationRepo;

        public PersonUpdateDtoToPersonEntityMapperTests()
        {
            _salutationRepo = new Mock<ISalutationRepository>();
            _mapper = new PersonUpdateDtoToPersonEntityMapper(_salutationRepo.Object);
        }

        [Fact]
        public async Task ShouldMap_CreatePersonForUpdateDto_To_PersonEntity()
        {
            var personForUpdateDto = CreatePersonForUpdateDto();
            _salutationRepo.Setup(x => x.GetSalutationByName(It.IsAny<string>()))
                .ReturnsAsync(new Salutation {SalutationName = "Mr", SalutationId = 1});

            var result = await _mapper.Map(personForUpdateDto);

            result.Should().BeEquivalentTo(ExpectedResult(personForUpdateDto));
        }

        [Fact]
        public async Task When_PersonUpdateDtoToPersonEntity_Is_Null_ShouldReturn_Null()
        {
            _salutationRepo.Setup(x => x.GetSalutationByName(It.IsAny<string>()))
                .ReturnsAsync(new Salutation { SalutationName = "Mr", SalutationId = 1 });

            var result =await _mapper.Map(null);
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