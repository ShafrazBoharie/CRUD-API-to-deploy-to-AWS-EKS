using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TPICAP.TechChallenge.Data.Services;
using TPICAP.TechChallenge.Infrastructure.Extensions;
using TPICAP.TechChallenge.Infrastructure.Models;
using TPICAP.TechChallenge.Infrastructure.Services;
using TPICAP.TechChallenge.Model.Models;

namespace TPICAP.TechChallenge.API.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IHateoasLinksCreator _hateoasLinksCreator;
        private readonly ILogger _logger;
        private readonly IPersonService _personsService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public PersonsController(IPersonService personsService, IPropertyCheckerService propertyCheckerService,
            IHateoasLinksCreator hateoasLinksCreator, ILoggerFactory logger)
        {
            _personsService = personsService ?? throw new ArgumentNullException(nameof(personsService));
            _propertyCheckerService = propertyCheckerService;
            _hateoasLinksCreator = hateoasLinksCreator;
            _logger = logger.CreateLogger<PersonsController>();
        }

        [HttpGet(Name = "GetPersons")]
        public IActionResult GetPersons([FromQuery] PersonsResourceParameters personsResourceParameters)
        {
            var persons = _personsService.GetPersons(personsResourceParameters);

            var paginationMetadata = new
            {
                totalCount = persons.TotalCount,
                pageSize = persons.PageSize,
                currentPage = persons.CurrentPage,
                totalPages = persons.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            var links = _hateoasLinksCreator.CreateLinksForPersonsCollection(Url.Link, personsResourceParameters,
                persons.HasNext,
                persons.HasPrevious);

            var shapingFields = string.IsNullOrWhiteSpace(personsResourceParameters.Fields) ||
                                personsResourceParameters.Fields.ToLower().Contains("id")
                ? personsResourceParameters.Fields
                : personsResourceParameters.Fields + ",Id";

            var shapedPersons = persons.Select(x => x.ShapeData(shapingFields));

            var shapedPersonsWithLinks = shapedPersons.Select(person =>
            {
                var personsAsDictionary = person as IDictionary<string, object>;
                var personLinks =
                    _hateoasLinksCreator.CreateLinksForPerson(Url.Link, (int) personsAsDictionary["Id"], null);
                personsAsDictionary.Add("links", personLinks);
                return personsAsDictionary;
            });

            var linkedCollectionResource = new
            {
                value = shapedPersonsWithLinks,
                links
            };

            return Ok(linkedCollectionResource);
        }


        [HttpGet("{personId}", Name = "GetPerson")]
        public IActionResult GetPerson(int personId, string fields)
        {
            PersonDto person = null;

            if (!_propertyCheckerService.TypeHasProperties<PersonDto>
                (fields))
                return BadRequest();

            try
            {
                person = _personsService.GetPerson(personId);
            }
            catch
            {
                return BadRequest();
            }

            if (person == null) return NotFound();

            var fullResourceToReturn = person.ShapeData(fields) as IDictionary<string, object>;

            var hateoasLinks = _hateoasLinksCreator.CreateLinksForPerson(Url.Link, person.Id, fields);

            fullResourceToReturn.Add("links", hateoasLinks);

            return Ok(fullResourceToReturn);
        }


        [HttpPost(Name = "CreatePerson")]
        public IActionResult CreatePerson(PersonForCreationDto person)
        {
            if (person == null) return BadRequest();


            var personToReturn = _personsService.AddPerson(person);

            var links = _hateoasLinksCreator.CreateLinksForPerson(Url.Link, personToReturn.Id, null);

            var linkedResourceToReturn = personToReturn.ShapeData(null) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetPerson",
                new {personId = linkedResourceToReturn["Id"]},
                linkedResourceToReturn);
        }

        [HttpPut(Name = "UpdatePerson")]
        public IActionResult UpdatePerson(PersonForUpdateDto person)
        {
            if (person == null) return BadRequest();

            var updatedPerson = _personsService.UpdatePerson(person);

            var links = _hateoasLinksCreator.CreateLinksForPerson(Url.Link, updatedPerson.Id, null);

            var linkedResourceToReturn = updatedPerson.ShapeData(null) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetPerson",
                new {personId = linkedResourceToReturn["Id"]},
                linkedResourceToReturn);
        }

        [HttpDelete("{personId}", Name = "DeletePerson")]
        public IActionResult DeletePerson(int personId)
        {
            if (personId <= 0 || _personsService.GetPerson(personId) == null) return NotFound();

            _personsService.DeletePerson(personId);

            return NoContent();
        }
    }
}