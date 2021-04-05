using System;

namespace TPICAP.TechChallenge.Model.Models
{
    public record PersonDto
    {
        public int Id { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public DateTime DateOfBirth { get; init; }

        public string Salutation { get; init; }
    }
}