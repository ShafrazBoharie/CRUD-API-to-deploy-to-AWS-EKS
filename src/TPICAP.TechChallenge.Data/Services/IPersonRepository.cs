using System.Collections.Generic;
using System.Threading.Tasks;
using TPICAP.TechChallenge.Data.Entities;

namespace TPICAP.TechChallenge.Data.Services
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersons(int pageSize, int pageNumber, bool isAscending, string orderBy = "LastName", string searchQuery = "");

        Task<Person> GetPerson(int personId);

        Task<Person> AddPerson(Person person);

        Task DeletePerson(Person person);

        Task<Person> UpdatePerson(Person person);

        Task<bool> PersonExist(int personId);

        Task<bool> PersonExist(Person person);

    }
}