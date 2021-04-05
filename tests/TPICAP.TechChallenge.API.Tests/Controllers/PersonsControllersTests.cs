using System;
using System.Collections.Generic;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TPICAP.TechChallenge.API.Controllers;
using TPICAP.TechChallenge.Data.Services;
using TPICAP.TechChallenge.Infrastructure.Helpers;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Infrastructure.Services;
using TPICAP.TechChallenge.Model.Models;
using Xunit;

namespace TPICAP.TechChallenge.API.Tests.Controllers
{
    public class PersonsControllersTests
    {
        private readonly Mock<IHateoasLinksCreator> _hateoasLinksCreator;
        private readonly Mock<ILoggerFactory> _logger;
        private readonly Mock<IPersonService> _personService;
        private readonly Mock<IPropertyCheckerService> _propertyCheckService;
        private PersonsController _sut;

        public PersonsControllersTests()
        {
            _personService = new Mock<IPersonService>();
            _propertyCheckService = new Mock<IPropertyCheckerService>();
            _hateoasLinksCreator = new Mock<IHateoasLinksCreator>();
            _logger = new Mock<ILoggerFactory>();

            ConfigureController();
        }

        [Fact]
        public void ShouldReturnPersonsCollection()
        {
            var personResourceParameters = new PersonsResourceParameters();

            var personsCollection = PagedList<PersonDto>.Create(new Faker<PersonDto>().Generate(12), 1, 10);

            _personService.Setup(x => x.GetPersons(It.IsAny<PersonsResourceParameters>())).Returns(personsCollection);

            var result = _sut.GetPersons(personResourceParameters) as OkObjectResult;

            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }

        [Fact]
        public void ShouldReturnPersonsWhenIdIsExist_AndReturn_200()
        {
            var personResourceParameters = new PersonsResourceParameters();
            var person = new Faker<PersonDto>()
                .RuleFor(x => x.Id, 222)
                .Generate();

            _propertyCheckService.Setup(x => x.TypeHasProperties<PersonDto>(It.IsAny<string>())).Returns(true);
            _personService.Setup(x => x.GetPerson(It.IsAny<int>(), It.IsAny<string>())).Returns(person);

            var result = _sut.GetPerson(222, "") as OkObjectResult;

            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }


        [Theory]
        [InlineData(221)]
        [InlineData(223)]
        public void ShouldNotReturnPersonsWhenIdIsNotExist_andReturn404(int personIdToRetrieve)
        {
            var personResourceParameters = new PersonsResourceParameters();

            var person = new Faker<PersonDto>()
                .RuleFor(x => x.Id, 222)
                .Generate();

            _propertyCheckService.Setup(x => x.TypeHasProperties<PersonDto>(It.IsAny<string>())).Returns(true);

            var result = _sut.GetPerson(personIdToRetrieve, "").As<NotFoundResult>();

            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public void ShouldGetPersonHasFailure_GetPersonShouldReturnBadRequestError()
        {
            var personResourceParameters = new PersonsResourceParameters();

            _propertyCheckService.Setup(x => x.TypeHasProperties<PersonDto>(It.IsAny<string>())).Returns(true);
            _personService.Setup(x => x.GetPerson(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception());
            var result = _sut.GetPerson(222, "").As<BadRequestResult>();

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void ShouldThrowBadRequest_WhenInsertingInvalidPerson()
        {
            var result = _sut.CreatePerson(null).As<BadRequestResult>();

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void WhenInsertingValidPersonShouldReturn_204()
        {
            var person = new Faker<PersonForCreationDto>()
                .Generate();
            _hateoasLinksCreator.Setup(x => x.CreateLinksForPerson(
                It.IsAny<Func<string, object, string>>(),
                It.IsAny<int>()
                , It.IsAny<string>())).Returns(new List<LinkDto>());

            _personService.Setup(x => x.AddPerson(It.IsAny<PersonForCreationDto>()))
                .Returns(new Faker<PersonDto>().Generate());
            var personResourceParameters = new PersonsResourceParameters();

            var result = _sut.CreatePerson(person).As<CreatedAtRouteResult>();

            result.StatusCode.Should().Be(201);
        }


        [Fact]
        public void WhenUpdatingAPersonItShouldReturn_201()
        {
            var person = new Faker<PersonForUpdateDto>()
                .Generate();
            _hateoasLinksCreator.Setup(x => x.CreateLinksForPerson(
                It.IsAny<Func<string, object, string>>(),
                It.IsAny<int>()
                , It.IsAny<string>())).Returns(new List<LinkDto>());

            _personService.Setup(x => x.UpdatePerson(It.IsAny<PersonForUpdateDto>()))
                .Returns(new Faker<PersonDto>().Generate());
            var personResourceParameters = new PersonsResourceParameters();

            var result = _sut.UpdatePerson(person).As<CreatedAtRouteResult>();

            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public void WhenDeletingAPersonItShouldReturn_NoContent()
        {
            var person = new Faker<PersonForUpdateDto>()
                .Generate();

            _propertyCheckService.Setup(x => x.TypeHasProperties<PersonDto>(It.IsAny<string>())).Returns(true);
            _personService.Setup(x => x.GetPerson(It.IsAny<int>(), It.IsAny<string>())).Returns(new PersonDto());

            var personResourceParameters = new PersonsResourceParameters();

            var result = _sut.DeletePerson(111).As<NoContentResult>();

            _personService.Verify(x => x.DeletePerson(It.IsAny<int>()), Times.Once);

            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public void WhenDeletingAPersonPersonIdIsInvalid_BadRequest()
        {
            var result = _sut.DeletePerson(-1).As<NotFoundResult>();

            result.StatusCode.Should().Be(404);
        }


        private void ConfigureController()
        {
            var httpContext = new DefaultHttpContext();

            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);

            _sut = new PersonsController(_personService.Object, _propertyCheckService.Object,
                _hateoasLinksCreator.Object, _logger.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };

            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<string>())).Returns("http://testhost/api/");

            _sut.Url = mockUrlHelper.Object;
        }
    }
}