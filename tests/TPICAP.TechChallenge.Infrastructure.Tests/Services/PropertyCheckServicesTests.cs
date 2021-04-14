using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TPICAP.TechChallenge.Data.Entities;
using TPICAP.TechChallenge.Data.Services;
using Xunit;

namespace TPICAP.TechChallenge.Infrastructure.Tests.Services
{
    public class PropertyCheckServicesTests
    {
        private readonly PropertyCheckerService _propertyCheckerService;
        
        public  PropertyCheckServicesTests()
        {
            _propertyCheckerService = new PropertyCheckerService();
        }

        [Theory]
        [InlineData("FirstName")]
        [InlineData("LastName, FirstName")]
        [InlineData("LastName, FirstName, Salutation")]
        public void WhereTheGivenObject_HasFieldsAsProperties_ShouldReturnTrue(string fields)
        {
            var hasPropertiesExist = _propertyCheckerService.TypeHasProperties<Person>(fields);

            hasPropertiesExist.Should().BeTrue();
        }


        [Theory]
        [InlineData("FieldNo")]
        [InlineData("NoField, FirstName")]
        public void WhereTheGivenObject_DoesNotHaveAllFieldsFieldsAsProperties_ShouldReturnFalse(string fields)
        {
            var hasPropertiesExist = _propertyCheckerService.TypeHasProperties<Person>(fields);

            hasPropertiesExist.Should().BeFalse();
        }








    }
}
