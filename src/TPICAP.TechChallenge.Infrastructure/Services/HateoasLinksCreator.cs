using System;
using System.Collections.Generic;
using TPICAP.TechChallenge.Infrastructure.Helpers;
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

        public IEnumerable<LinkDto> CreateLinksForPersons(Func<string, object, string> urlLink,
            PersonsResourceParameters personsResourceParameters, bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto
                {
                    Href = CreatePersonsResourceUri(urlLink, personsResourceParameters, ResourceUriType.Current),
                    Rel = "self",
                    Method = "GET"
                });

            if (hasNext)
                new LinkDto
                {
                    Href = CreatePersonsResourceUri(urlLink, personsResourceParameters, ResourceUriType.NextPage),
                    Rel = "nextPage",
                    Method = "GET"
                };

            if (hasPrevious)
                new LinkDto
                {
                    Href = CreatePersonsResourceUri(urlLink, personsResourceParameters, ResourceUriType.PreviousPage),
                    Rel = "previousPage",
                    Method = "GET"
                };

            return links;
        }


        private string CreatePersonsResourceUri(Func<string, object, string> urlLink,
            PersonsResourceParameters personsResourceParameters,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return urlLink("GetPersons",
                        new
                        {
                            fields = personsResourceParameters.Fields,
                            orderBy = personsResourceParameters.OrderBy,
                            pageNumber = personsResourceParameters.PageNumber - 1,
                            pageSize = personsResourceParameters.PageSize,
                            searchQuery = personsResourceParameters.SearchQuery
                        });
                case ResourceUriType.NextPage:
                    return urlLink("GetPersons",
                        new
                        {
                            fields = personsResourceParameters.Fields,
                            orderBy = personsResourceParameters.OrderBy,
                            pageNumber = personsResourceParameters.PageNumber + 1,
                            pageSize = personsResourceParameters.PageSize,
                            searchQuery = personsResourceParameters.SearchQuery
                        });
                case ResourceUriType.Current:
                default:
                    return urlLink("GetPersons",
                        new
                        {
                            fields = personsResourceParameters.Fields,
                            orderBy = personsResourceParameters.OrderBy,
                            pageNumber = personsResourceParameters.PageNumber,
                            pageSize = personsResourceParameters.PageSize,
                            searchQuery = personsResourceParameters.SearchQuery
                        });
            }
        }
    }
}