using System.Collections.Generic;
using TPICAP.TechChallenge.Data.Entities;

namespace TPICAP.TechChallenge.Data.Services
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons(int pageSize, int pageNumber, bool isAscending, string orderBy = "LastName", string searchQuery = "");

        Person GetPerson(int personId);

        void AddPerson(Person person);

        void DeletePerson(Person person);

        void UpdatePerson(Person person);

        bool PersonExist(int personId);

        bool PersonExist(Person person);

        bool Save();

    }
}