using System.Collections.Generic;
using System.Linq;
using TPICAP.TechChallenge.Data.Entities;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.Infrastructure.Mappers
{
    public class PersonEntityToPersonDtoMapper
    {
        public virtual IEnumerable<PersonDto> Map(IEnumerable<Person> persons)
        {
            return persons.Select(p => Map(p));
        }

        public virtual PersonDto Map(Person person)
        {
            if (person == null) return null;

            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Salutation = person.Salutation.SalutationName
            };
        }
    }
}