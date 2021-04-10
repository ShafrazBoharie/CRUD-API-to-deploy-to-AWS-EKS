using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Infrastructure.Services;
using TPICAP.TechChallenge.Model.Models;
using Xunit;

namespace TPICAP.TechChallenge.Infrastructure.Tests.Services
{
    public class HateoasLinksCreatorTests
    {
        private readonly HateoasLinksCreator _sut;
        private readonly Mock<Func<string, object, string>> _urlLink;

        public HateoasLinksCreatorTests()
        {
            _sut = new HateoasLinksCreator();
            _urlLink = new Mock<Func<string, object, string>>();
            _urlLink.Setup(x => x.Invoke(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:1111");
        }

        [Theory]
        [InlineData("firstName,lastName", 22)]
        [InlineData(null, 33)]
        public void ForTheGivenPerson_HateoasLinkShouldBeCreated(string shapingFields, int personId)
        {
            var hateoasLinks = _sut.CreateLinksForPerson(_urlLink.Object, 111, null);
            hateoasLinks.Should()
                .BeEquivalentTo(ExpectedHateoasLinksForSinglePerson("http://localhost:1111", 21, null));
        }


        [Theory]
        [InlineData(10,1,true,false)]
        [InlineData(10,21, true, true)]
        public void ForTheGivenPersonCollection_HateoasLinkShouldBeCreated(int pageSize, int pageNumber,bool hasNext, bool hasPrevious)
        {
            var personsResourceParameters = new PersonsResourceParameters
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
            };

            var hateoasLinks = _sut.CreateLinksForPersonsCollection(_urlLink.Object, personsResourceParameters,  hasNext,hasPrevious);

            hateoasLinks.Should()
                .BeEquivalentTo(ExpectedHateoasLinksForPersonCollection("http://localhost:1111", pageSize,pageNumber,hasNext,hasPrevious));
        }

        private IEnumerable<LinkDto> ExpectedHateoasLinksForPersonCollection(string url,int pageSize, int pageNumber,bool hasNext, bool HasPrevious)
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto { Href =$"{url}?pageSize={pageSize}&pageNumber={pageNumber}", Rel = "self", Method = "GET"});
            if(hasNext)
                links.Add(new LinkDto { Href = $"{url}?pageSize={pageSize}&pageNumber={pageNumber+1}", Rel = "nextPage", Method = "GET" });
            if (HasPrevious)
                links.Add(new LinkDto { Href = $"{url}?pageSize={pageSize}&pageNumber={pageNumber - 1}", Rel = "previousPage", Method = "GET" });

            return links;
        }


        private IEnumerable<LinkDto> ExpectedHateoasLinksForSinglePerson(string url, int personId, string shapingFields)
        {
            if (!string.IsNullOrWhiteSpace(shapingFields)) shapingFields = $"?fields={shapingFields}";

            return new List<LinkDto>
            {
                new() {Href = $"{url}{shapingFields}", Rel = "self", Method = "GET"},
                new() {Href = $"{url}{shapingFields}", Rel = "delete_person", Method = "DELETE"},
                new() {Href = $"{url}{shapingFields}", Rel = "create_person", Method = "POST"},
                new() {Href = $"{url}{shapingFields}", Rel = "update_person", Method = "PUT"}
            };
        }
    }
}