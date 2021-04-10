using System;
using System.ComponentModel.DataAnnotations;

namespace TPICAP.TechChallenge.Model.Models
{
    public record PersonForUpdateDto
    {
        // Id is optional to support upsert
        public int Id { get; set; }
        [Required]
        public string FirstName { get; init; }
        [Required]
        public string LastName { get; init; }
        [Required]
        public DateTime DOB { get; init; }
        [Required]
        public string Salutation { get; init; }
    }
}