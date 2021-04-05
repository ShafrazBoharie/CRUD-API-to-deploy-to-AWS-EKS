using System.IO;
using System.Linq;
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

        public PagedList<PersonDto> GetPersons(PersonsResourceParameters personsResourceParameters)
        {
            var personEntityCollection = _personRepository.GetPersons(personsResourceParameters.PageSize,
                personsResourceParameters.PageNumber,
                personsResourceParameters.IsAscending,
                personsResourceParameters.OrderBy);

            if (!personEntityCollection.Any()) return new PagedList<PersonDto>();

            var personsDtoCollection = _personEntityToPersonDtoMapper.Map(personEntityCollection);

            return PagedList<PersonDto>.Create(personsDtoCollection, personsResourceParameters.PageNumber,
                personsResourceParameters.PageSize);
        }

        public PersonDto GetPerson(int personId, string fields)
        {
            var personEntity = _personRepository.GetPerson(personId);

            return _personEntityToPersonDtoMapper.Map(personEntity);
        }

        public PersonDto AddPerson(PersonForCreationDto personForCreationDto)
        {
            var personEntity = _personCreationDtoToPersonEntityMapper.Map(personForCreationDto);

            if (personEntity == null) throw new InvalidDataException("Unable To Map To Person Entity");

            _personRepository.AddPerson(personEntity);
            _personRepository.Save();

            return _personEntityToPersonDtoMapper.Map(personEntity);
        }

        public bool DeletePerson(int personId)
        {
            var person = _personRepository.GetPerson(personId);

            if (person == null) return false;

            _personRepository.DeletePerson(person);
            _personRepository.Save();
            return true;
        }

        public PersonDto UpdatePerson(PersonForUpdateDto personForUpdateDto)
        {
            var entityToUpdate = _personUpdateDtoToPersonEntityMapper.Map(personForUpdateDto);

            if (!_personRepository.PersonExist(personForUpdateDto.Id))
            {
                if (entityToUpdate != null)
                {
                    entityToUpdate.Id = 0;
                    _personRepository.AddPerson(entityToUpdate);
                    _personRepository.Save();
                    return _personEntityToPersonDtoMapper.Map(_personRepository.GetPerson(entityToUpdate.Id));
                }
            }
            else
            {
                if (entityToUpdate != null)
                {
                    _personRepository.UpdatePerson(entityToUpdate);
                    _personRepository.Save();
                    return _personEntityToPersonDtoMapper.Map(_personRepository.GetPerson(entityToUpdate.Id));
                }
            }

            return null;
        }
    }
}