using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPICAP.TechChallenge.Data.Entities
{
    public class Person
    {
        [Key] public int Id { get; set; }

        [MaxLength(100)] [Required] public string FirstName { get; set; }

        [MaxLength(100)] [Required] public string LastName { get; set; }

        [Required] public DateTime DateOfBirth { get; set; }

        [ForeignKey("Salutation")] public virtual int SalutationId { get; set; }

        public virtual Salutation Salutation { get; set; }
    }
}