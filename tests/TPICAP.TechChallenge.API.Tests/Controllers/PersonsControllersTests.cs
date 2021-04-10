using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task ShouldReturnPersonsCollection()
        {
            var personResourceParameters = new PersonsResourceParameters();

            var personsCollection = PagedList<PersonDto>.Create(new Faker<PersonDto>().Generate(12), 1, 10);

            _personService.Setup(x => x.GetPersons(It.IsAny<PersonsResourceParameters>())).ReturnsAsync(personsCollection);

            var result = await _sut.GetPersons(personResourceParameters) as OkObjectResult;

            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturnPersonsWhenIdIsExist_AndReturn_200()
        {
            var personResourceParameters = new PersonsResourceParameters();
            var person = new Faker<PersonDto>()
                .RuleFor(x => x.Id, 222)
                .Generate();

            _propertyCheckService.Setup(x => x.TypeHasProperties<PersonDto>(It.IsAny<string>())).Returns(true);
            _personService.Setup(x => x.GetPerson(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(person);

            var result =await _sut.GetPerson(222, "") as OkObjectResult;

            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();
        }


        [Theory]
        [InlineData(221)]
        [InlineData(223)]
        public async Task ShouldNotReturnPersonsWhenIdIsNotExist_andReturn404(int personIdToRetrieve)
        {
            var personResourceParameters = new PersonsResourceParameters();

            var person = new Faker<PersonDto>()
                .RuleFor(x => x.Id, 222)
                .Generate();

            _propertyCheckService.Setup(x => x.TypeHasProperties<PersonDto>(It.IsAny<string>())).Returns(true);

            var result =(await _sut.GetPerson(personIdToRetrieve, "")).As<NotFoundResult>();

            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task ShouldGetPersonHasFailure_GetPersonShouldReturnBadRequestError()
        {
            var personResourceParameters = new PersonsResourceParameters();

            _propertyCheckService.Setup(x => x.TypeHasProperties<PersonDto>(It.IsAny<string>())).Returns(true);
            _personService.Setup(x => x.GetPerson(It.IsAny<int>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            var result = (await _sut.GetPerson(222, "")).As<BadRequestResult>();

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task ShouldThrowBadRequest_WhenInsertingInvalidPerson()
        {
            var result =(await _sut.CreatePerson(null)).As<BadRequestResult>();

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task WhenInsertingValidPersonShouldReturn_204()
        {
            var person = new Faker<PersonForCreationDto>()
                .Generate();
            _hateoasLinksCreator.Setup(x => x.CreateLinksForPerson(
                It.IsAny<Func<string, object, string>>(),
                It.IsAny<int>()
                , It.IsAny<string>())).Returns(new List<LinkDto>());

            _personService.Setup(x => x.AddPerson(It.IsAny<PersonForCreationDto>()))
                .ReturnsAsync(new Faker<PersonDto>().Generate());
            var personResourceParameters = new PersonsResourceParameters();

            var result = (await _sut.CreatePerson(person)).As<CreatedAtRouteResult>();

            result.StatusCode.Should().Be(201);
        }


        [Fact]
        public async Task WhenUpdatingAPersonItShouldReturn_201()
        {
            var person = new Faker<PersonForUpdateDto>()
                .Generate();
            _hateoasLinksCreator.Setup(x => x.CreateLinksForPerson(
                It.IsAny<Func<string, object, string>>(),
                It.IsAny<int>()
                , It.IsAny<string>())).Returns(new List<LinkDto>());

            _personService.Setup(x => x.UpdatePerson(It.IsAny<PersonForUpdateDto>()))
                .ReturnsAsync(new Faker<PersonDto>().Generate());
            var personResourceParameters = new PersonsResourceParameters();

            var result = (await _sut.UpdatePerson(person)).As<CreatedAtRouteResult>();

            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task WhenDeletingAPersonItShouldReturn_NoContent()
        {
            var person = new Faker<PersonForUpdateDto>()
                .Generate();

            _propertyCheckService.Setup(x => x.TypeHasProperties<PersonDto>(It.IsAny<string>())).Returns(true);
            _personService.Setup(x => x.GetPerson(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new PersonDto());

            var personResourceParameters = new PersonsResourceParameters();

            var result = (await _sut.DeletePerson(111)).As<NoContentResult>();

            _personService.Verify(x => x.DeletePerson(It.IsAny<int>()), Times.Once);

            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task WhenDeletingAPersonPersonIdIsInvalid_BadRequest()
        {
            var result = (await _sut.DeletePerson(-1)).As<NotFoundResult>();

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