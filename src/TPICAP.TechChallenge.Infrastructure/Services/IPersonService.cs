using System.Threading.Tasks;
using TPICAP.TechChallenge.Infrastructure.Helpers;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.Infrastructure.Services
{
    public interface IPersonService
    {
        Task<PagedList<PersonDto>> GetPersons(PersonsResourceParameters personsResourceParameters);

        Task<PersonDto> GetPerson(int personId, string fields = "");

        Task<PersonDto> AddPerson(PersonForCreationDto personForCreationDto);

        Task<bool> DeletePerson(int personId);

        Task<PersonDto> UpdatePerson(PersonForUpdateDto personForUpdateDto);
    }
}