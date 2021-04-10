using System;
using System.ComponentModel.DataAnnotations;

namespace TPICAP.TechChallenge.Model.Models
{
    public record PersonForCreationDto
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public DateTime DOB { get; set; }

        [Required] public string Salutation { get; set; }
    }
}