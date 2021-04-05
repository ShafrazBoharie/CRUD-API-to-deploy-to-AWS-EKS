using System;
using System.Linq;
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

        [Fact]
        public void WhenTheSalutationsAreExistInTheDatabase_Repo_ShouldReturnSalutationCollection()
        {
            _sut.Salutations().Count().Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData("Mr")]
        [InlineData("Mrs")]
        [InlineData("Miss")]
        [InlineData("Dr")]
        [InlineData("Sir")]
        [InlineData("Lord")]
        [InlineData("Lady")]
        public void WhenSalutationIsExistInTheDatabase_And_WhenCheckingForExistenceOfSalutation_ShouldReturn_True(
            string salutationName)
        {
            _sut.IsSalutationExist(salutationName).Should().BeTrue();
        }

        [Theory]
        [InlineData("No Title")]
        [InlineData("Not Existing Title")]
        public void WhenSalutationIsNotExistInTheDatabase_And_WhenCheckingForExistenceOfSalutation_ItShouldReturn_False(
            string salutationName)
        {
            _sut.IsSalutationExist(salutationName).Should().BeFalse();
        }

        [Fact]
        public void WhenCheckingForSalutationIsExist_WithTheNullParameter_ItShouldThrow_ArgumentNullException()
        {
            _sut.Invoking(x => x.IsSalutationExist(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("Mr")]
        [InlineData("Mrs")]
        [InlineData("Dr")]
        public void WhenRetrievingSalutationObject_WithExistingSalutationName_ItShouldReturns_ValidSalutationObject(
            string salutationName)
        {
            var result = _sut.GetSalutationByName(salutationName);

            result.SalutationName.Should().Be(salutationName);
        }


        [Theory]
        [InlineData("xxx")]
        [InlineData("NoTitle")]
        public void WhenRetrievingSalutationObject_WithNonExistingSalutationName_ItShouldReturns_Null(
            string salutationName)
        {
            var result = _sut.GetSalutationByName(salutationName);

            result.Should().BeNull();
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenRetrievingSalutationObject_WithNullParameter_ItShouldThrow_ArgumentNullException(
            string salutationName)
        {
            _sut.Invoking(x => x.GetSalutationByName(salutationName)).Should().Throw<ArgumentNullException>();
        }
    }
}