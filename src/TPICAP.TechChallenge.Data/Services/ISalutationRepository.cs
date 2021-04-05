using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPICAP.TechChallenge.Data.Entities;

namespace TPICAP.TechChallenge.Data.Services
{
    public interface ISalutationRepository
    {
        IEnumerable<Salutation> Salutations();

        bool IsSalutationExist(string salutation);

        Salutation GetSalutationByName(string salutation);

    };
}
