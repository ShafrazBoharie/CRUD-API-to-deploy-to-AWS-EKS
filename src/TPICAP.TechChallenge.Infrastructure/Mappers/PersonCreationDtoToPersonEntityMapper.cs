using System.Threading.Tasks;
using TPICAP.TechChallenge.Data.Entities;
using TPICAP.TechChallenge.Data.Services;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.Infrastructure.Mappers
{
    public class PersonCreationDtoToPersonEntityMapper
    {
        private readonly ISalutationRepository _salutationRepository;

        public PersonCreationDtoToPersonEntityMapper(ISalutationRepository salutationRepository)
        {
            _salutationRepository = salutationRepository;
        }

        public virtual async Task<Person> Map(PersonForCreationDto personCreationDto)
        {
            if (personCreationDto == null) return null;

            return new Person
            {
                FirstName = personCreationDto.FirstName,
                LastName = personCreationDto.LastName,
                DateOfBirth = personCreationDto.DOB,
                Salutation = await _salutationRepository.GetSalutationByName(personCreationDto.Salutation) ?? null
            };
        }
    }
}