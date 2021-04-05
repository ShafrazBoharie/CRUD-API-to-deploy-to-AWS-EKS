using TPICAP.TechChallenge.Data.Entities;
using TPICAP.TechChallenge.Data.Services;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.Infrastructure.Mappers
{
    public class PersonUpdateDtoToPersonEntityMapper
    {
        private readonly ISalutationRepository _salutationRepository;

        public PersonUpdateDtoToPersonEntityMapper(ISalutationRepository salutationRepository)
        {
            _salutationRepository = salutationRepository;
        }

        public virtual Person Map(PersonForUpdateDto personUpdateDto)
        {
            if (personUpdateDto == null) return null;
            return new Person()
            {
                Id = personUpdateDto.Id,
                FirstName = personUpdateDto.FirstName,
                LastName = personUpdateDto.LastName,
                DateOfBirth = personUpdateDto.DOB,
                Salutation = _salutationRepository.GetSalutationByName(personUpdateDto.Salutation) ?? null
            };
        }
    }
}