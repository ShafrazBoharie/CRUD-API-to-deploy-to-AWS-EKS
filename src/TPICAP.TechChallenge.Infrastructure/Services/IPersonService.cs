using TPICAP.TechChallenge.Infrastructure.Helpers;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.Infrastructure.Services
{
    public interface IPersonService
    {
        PagedList<PersonDto> GetPersons(PersonsResourceParameters personsResourceParameters);

        PersonDto GetPerson(int personId, string fields = "");

        PersonDto AddPerson(PersonForCreationDto personForCreationDto);

        bool DeletePerson(int personId);

        PersonDto UpdatePerson(PersonForUpdateDto personForUpdateDto);
    }
}