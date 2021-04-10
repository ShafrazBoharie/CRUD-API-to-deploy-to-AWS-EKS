using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TPICAP.TechChallenge.Data.Services;
using TPICAP.TechChallenge.Infrastructure.Helpers;
using TPICAP.TechChallenge.Infrastructure.Mappers;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly PersonCreationDtoToPersonEntityMapper _personCreationDtoToPersonEntityMapper;
        private readonly PersonEntityToPersonDtoMapper _personEntityToPersonDtoMapper;
        private readonly IPersonRepository _personRepository;
        private readonly PersonUpdateDtoToPersonEntityMapper _personUpdateDtoToPersonEntityMapper;

        public PersonService(IPersonRepository personRepository,
            PersonEntityToPersonDtoMapper personEntityToPersonDtoMapper,
            PersonCreationDtoToPersonEntityMapper personCreationDtoToPersonEntityMapper,
            PersonUpdateDtoToPersonEntityMapper personUpdateDtoToPersonEntityMapper)
        {
            _personRepository = personRepository;
            _personEntityToPersonDtoMapper = personEntityToPersonDtoMapper;
            _personCreationDtoToPersonEntityMapper = personCreationDtoToPersonEntityMapper;
            _personUpdateDtoToPersonEntityMapper = personUpdateDtoToPersonEntityMapper;
        }

        public async Task<PagedList<PersonDto>> GetPersons(PersonsResourceParameters personsResourceParameters)
        {
            var personEntityCollection = (await _personRepository.GetPersons(personsResourceParameters.PageSize,
                personsResourceParameters.PageNumber,
                personsResourceParameters.IsAscending,
                personsResourceParameters.OrderBy)).ToList();

            if (!personEntityCollection.Any()) return new PagedList<PersonDto>();

            var personsDtoCollection = _personEntityToPersonDtoMapper.Map(personEntityCollection);

            return PagedList<PersonDto>.Create(personsDtoCollection, personsResourceParameters.PageNumber,
                personsResourceParameters.PageSize);
        }

        public async Task<PersonDto> GetPerson(int personId, string fields)
        {
            var personEntity =await _personRepository.GetPerson(personId);

            return _personEntityToPersonDtoMapper.Map(personEntity);
        }

        public async Task<PersonDto> AddPerson(PersonForCreationDto personForCreationDto)
        {
            var personEntity = await _personCreationDtoToPersonEntityMapper.Map(personForCreationDto);

            if (personEntity == null) throw new InvalidDataException("Unable To Map To Person Entity");

            var createdPerson = await _personRepository.AddPerson(personEntity);

            return _personEntityToPersonDtoMapper.Map(createdPerson);
        }

        public async Task<bool> DeletePerson(int personId)
        {
            var person =await _personRepository.GetPerson(personId);

            if (person == null) return false;

            await _personRepository.DeletePerson(person);
            return true;
        }

        public async Task<PersonDto> UpdatePerson(PersonForUpdateDto personForUpdateDto)
        {
            var entityToUpdate =await _personUpdateDtoToPersonEntityMapper.Map(personForUpdateDto);

            if (!await _personRepository.PersonExist(personForUpdateDto.Id))
            {
                if (entityToUpdate != null)
                {
                    entityToUpdate.Id = 0;
                    await _personRepository.AddPerson(entityToUpdate);

                    return _personEntityToPersonDtoMapper.Map(await _personRepository.GetPerson(entityToUpdate.Id));
                }
            }
            else
            {
                if (entityToUpdate != null)
                {
                    await _personRepository.UpdatePerson(entityToUpdate);

                    return _personEntityToPersonDtoMapper.Map(await _personRepository.GetPerson(entityToUpdate.Id));
                }
            }
            return null;
        }
    }
}