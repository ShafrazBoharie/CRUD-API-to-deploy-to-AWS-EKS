using System.Threading.Tasks;
using TPICAP.TechChallenge.Data.Entities;

namespace TPICAP.TechChallenge.Data.Services
{
    public interface ISalutationRepository
    {
        Task<Salutation> GetSalutationByName(string salutation);
    }
}