using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TPICAP.TechChallenge.Data.Entities;

namespace TPICAP.TechChallenge.Data.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleContext _context;
        private readonly ISalutationRepository _salutationRepository;

        public PersonRepository(PeopleContext context, ISalutationRepository salutationRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _salutationRepository = salutationRepository ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Person> GetPersons(int pageSize, int pageNumber, bool isAscending,
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

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                // TODO : Provide a IQueryableExtension
                //PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(Person)).Find(orderBy, true);

                //if (prop == null)
                //{
                //    throw new ArgumentNullException();
                //}

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


        public Person GetPerson(int personId)
        {
            if (personId < 1) throw new ArgumentNullException(nameof(personId));

            return _context.People.Include(x => x.Salutation).FirstOrDefault(x => x.Id == personId);
        }

        public void AddPerson(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));

            _context.People.Add(person);
        }

        public void DeletePerson(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            _context.People.Remove(person);
        }

        public void UpdatePerson(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
        }

        public bool PersonExist(int personId)
        {
            return _context.People.Any(x => x.Id == personId);
        }

        public bool PersonExist(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));

            return _context.People
                .Any(x => x.FirstName == person.FirstName &&
                          x.LastName == person.LastName &&
                          x.DateOfBirth == person.DateOfBirth &&
                          x.Salutation.SalutationId == person.Salutation.SalutationId);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}