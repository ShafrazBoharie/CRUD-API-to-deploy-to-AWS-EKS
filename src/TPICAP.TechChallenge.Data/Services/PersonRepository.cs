using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPICAP.TechChallenge.Data.Entities;

namespace TPICAP.TechChallenge.Data.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleContext _context;

        public PersonRepository(PeopleContext context, ISalutationRepository salutationRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Person>> GetPersons(int pageSize, int pageNumber, bool isAscending,
            string orderBy = "LastName", string searchQuery = "")
        {
            var personCollection = _context.People.Include(x => x.Salutation) as IQueryable<Person>;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var searchString = searchQuery.Trim();
                personCollection = personCollection.Where(x => x.FirstName.Contains(searchString)
                                                               || x.LastName.Contains(searchString)
                                                               || x.Salutation.SalutationName.Contains(searchString)
                                                               || x.DateOfBirth.ToShortDateString()
                                                                   .Contains(searchString)
                );
            }

            personCollection = SoryPersonsCollection(isAscending, orderBy, personCollection);

            return await personCollection.ToListAsync();
        }
        
        public async Task<Person> GetPerson(int personId)
        {
            if (personId < 1) throw new ArgumentNullException(nameof(personId));

            return await _context.People.Include(x => x.Salutation).FirstOrDefaultAsync(x => x.Id == personId);
        }

        public async Task<Person> AddPerson(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));

            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task DeletePerson(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            _context.People.Remove(person);
            await _context.SaveChangesAsync();

        }

        public async Task<Person> UpdatePerson(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> PersonExist(int personId)
        {
            return await _context.People.AnyAsync(x => x.Id == personId);
        }

        public async Task<bool> PersonExist(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));

            return await _context.People
                .AnyAsync(x => x.FirstName == person.FirstName &&
                          x.LastName == person.LastName &&
                          x.DateOfBirth == person.DateOfBirth &&
                          x.Salutation.SalutationId == person.Salutation.SalutationId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private static IQueryable<Person> SoryPersonsCollection(bool isAscending, string orderBy, IQueryable<Person> personCollection)
        {
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                if (isAscending)
                    switch (orderBy.ToLower())
                    {
                        case "lastname":
                            {
                                personCollection = personCollection.OrderBy(x => x.LastName);
                                break;
                            }
                        case "Dob":
                            {
                                personCollection = personCollection.OrderBy(x => x.DateOfBirth);
                                break;
                            }
                        case "salutation":
                            {
                                personCollection = personCollection.OrderBy(x => x.Salutation.SalutationName);
                                break;
                            }
                        default:
                            {
                                personCollection = personCollection.OrderBy(x => x.LastName);
                                break;
                            }
                    }
                else
                    switch (orderBy.ToLower())
                    {
                        case "lastname":
                            {
                                personCollection = personCollection.OrderByDescending(x => x.LastName);
                                break;
                            }
                        case "Dob":
                            {
                                personCollection = personCollection.OrderByDescending(x => x.DateOfBirth);
                                break;
                            }
                        case "salutation":
                            {
                                personCollection = personCollection.OrderByDescending(x => x.Salutation.SalutationName);
                                break;
                            }
                        default:
                            {
                                personCollection = personCollection.OrderByDescending(x => x.LastName);
                                break;
                            }
                    }
            }

            return personCollection;
        }

    }
}