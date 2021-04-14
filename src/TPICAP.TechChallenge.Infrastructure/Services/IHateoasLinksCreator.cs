using System;
using System.Collections.Generic;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.Infrastructure.Services
{
    public interface IHateoasLinksCreator
    {
        IEnumerable<LinkDto> CreateLinksForPerson(Func<string, object, string> urlLink, int personId, string fields);

        IEnumerable<LinkDto> CreateLinksForPersonsCollection(Func<string, object, string> urlLink,
            PersonsResourceParameters personsResourceParameters, bool hasNext, bool hasPrevious);
    }
}