using System;
using System.Collections.Generic;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.Infrastructure.Services
{
    public class HateoasLinksCreator : IHateoasLinksCreator
    {
        public IEnumerable<LinkDto> CreateLinksForPerson(Func<string, object, string> urlLink, int personId,
            string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
                links.Add(
                    new LinkDto
                    {
                        Method = "GET",
                        Href = urlLink("GetPerson", new {personId}),
                        Rel = "self"
                    });
            else
                links.Add(
                    new LinkDto
                    {
                        Method = "GET",
                        Href = urlLink("GetPerson", new {personId, fields}),
                        Rel = "self"
                    });

            links.Add(
                new LinkDto
                {
                    Method = "DELETE",
                    Href = urlLink("DeletePerson", new {personId}),
                    Rel = "delete_person"
                });

            links.Add(
                new LinkDto
                {
                    Method = "POST",
                    Href = urlLink("CreatePerson", new object()),
                    Rel = "create_person"
                });

            links.Add(
                new LinkDto
                {
                    Method = "PUT",
                    Href = urlLink("UpdatePerson", new object()),
                    Rel = "update_person"
                });

            return links;
        }

        public IEnumerable<LinkDto> CreateLinksForPersonsCollection(Func<string, object, string> urlLink,
            PersonsResourceParameters personsResourceParameters, bool hasNext, bool hasPrevious)
        {
            var baseUrl = urlLink("GetPersons", null);

            var links = new List<LinkDto>();

            links.Add(
                new LinkDto
                {
                    Href =
                        $"{baseUrl}?pageSize={personsResourceParameters.PageSize}&pageNumber={personsResourceParameters.PageNumber}",
                    Rel = "self",
                    Method = "GET"
                });

            if (hasNext)
                links.Add(new LinkDto
                {
                    Href =
                        $"{baseUrl}?pageSize={personsResourceParameters.PageSize}&pageNumber={personsResourceParameters.PageNumber + 1}",
                    Rel = "nextPage",
                    Method = "GET"
                });

            if (hasPrevious)
                links.Add(new LinkDto
                {
                    Href =
                        $"{baseUrl}?pageSize={personsResourceParameters.PageSize}&pageNumber={personsResourceParameters.PageNumber-1}",
                    Rel = "previousPage",
                    Method = "GET"
                });

            return links;
        }
    }
}