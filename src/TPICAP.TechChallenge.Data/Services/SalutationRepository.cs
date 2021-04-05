using System;
using System.Collections.Generic;
using System.Linq;
using TPICAP.TechChallenge.Data.Entities;

namespace TPICAP.TechChallenge.Data.Services
{
    public class SalutationRepository : ISalutationRepository
    {
        private readonly PeopleContext _context;

        public SalutationRepository(PeopleContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Salutation> Salutations()
        {
            return _context.Salutations;
        }

        public bool IsSalutationExist(string salutation)
        {
            if (string.IsNullOrEmpty(salutation)) throw new ArgumentNullException(nameof(salutation));

            return _context.Salutations.Any(x => x.SalutationName == salutation);
        }

        public Salutation GetSalutationByName(string salutation)
        {
            if (string.IsNullOrEmpty(salutation)) throw new ArgumentNullException(nameof(salutation));

            return _context.Salutations.FirstOrDefault(x => x.SalutationName == salutation) ?? null;
        }
    }
}