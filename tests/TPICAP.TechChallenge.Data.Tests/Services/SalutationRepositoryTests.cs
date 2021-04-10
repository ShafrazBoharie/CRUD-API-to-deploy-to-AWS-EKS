using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TPICAP.TechChallenge.Data.Services;
using Xunit;

namespace TPICAP.TechChallenge.Data.Tests.Services
{
    public class SalutationRepositoryTests
    {
        private readonly SalutationRepository _sut;

        public SalutationRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<PeopleContext>();
            builder.UseInMemoryDatabase("InMemoryDB");
            var context = new PeopleContext(builder.Options);
            context.Database.EnsureCreated();

            _sut = new SalutationRepository(context);
        }

        [Theory]
        [InlineData("xxx")]
        [InlineData("NoTitle")]
        public async Task WhenRetrievingSalutationObject_WithNonExistingSalutationName_ItShouldReturns_Null(
            string salutationName)
        {
            var result = await _sut.GetSalutationByName(salutationName);

            result.Should().BeNull();
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task WhenRetrievingSalutationObject_WithNullParameter_ItShouldThrow_ArgumentNullException(
            string salutationName)
        {
            await _sut.Invoking(x => x.GetSalutationByName(salutationName)).Should()
                .ThrowAsync<ArgumentNullException>();
        }
    }
}