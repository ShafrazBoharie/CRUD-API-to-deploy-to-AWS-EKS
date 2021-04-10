using System.ComponentModel.DataAnnotations;

namespace TPICAP.TechChallenge.Data.Entities
{
    public class Salutation
    {
        [Key] public int SalutationId { get; set; }

        [MaxLength(20)] [Required] public string SalutationName { get; set; }
    }
}