using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Salutation> GetSalutationByName(string salutation)
        {
            if (string.IsNullOrEmpty(salutation)) throw new ArgumentNullException(nameof(salutation));

            return await _context.Salutations.FirstOrDefaultAsync(x => x.SalutationName == salutation) ?? null;
        }
    }
}