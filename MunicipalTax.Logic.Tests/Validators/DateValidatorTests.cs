using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using MunicipalTax.Logic.Models;
using MunicipalTax.Logic.Validators;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using NUnit.Framework;

namespace MunicipalTax.Logic.Tests.Validators
{
    [TestFixture]
    public class DateValidatorTests
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public void ValidateAndConvertDateString_ValidDate_ReturnDateTime()
        {
            var date = "14-02-2021";

            var sut = _fixture.Create<DateValidator>();

            var result = sut.ValidateAndConvertDateString(date);

            Assert.IsNotNull(result);
        }

        [TestCase("14-02-2021", "14-02-2021", TaxTypes.Daily)]
        [TestCase("15-02-2021", "21-02-2021", TaxTypes.Weekly)]
        [TestCase("01-03-2021", "31-03-2021", TaxTypes.Monthly)]
        [TestCase("01-01-2021", "31-12-2021", TaxTypes.Yearly)]
        public void ValidateDuration_ValidInput_ReturnTrue(string startDate, string endDate, TaxTypes taxType)
        {
            var duration = _fixture.Build<Duration>()
                .With(d => d.StartDate, DateTime.Parse(startDate))
                .With(d => d.EndDate, DateTime.Parse(endDate))
                .Create();

            var sut = _fixture.Create<DateValidator>();

            var result = sut.ValidateDuration(duration, taxType);

            Assert.IsTrue(result);
        }

        [TestCase("14-02-2021", "16-02-2021", TaxTypes.Daily)]
        [TestCase("15-02-2021", "25-02-2021", TaxTypes.Weekly)]
        [TestCase("01-03-2021", "30-04-2021", TaxTypes.Monthly)]
        [TestCase("01-01-2021", "31-10-2021", TaxTypes.Yearly)]
        [TestCase("01-01-2021", "31-10-2022", TaxTypes.Yearly)]
        public void ValidateDuration_InvalidInput_ReturnFalse(string startDate, string endDate, TaxTypes taxType)
        {
            var duration = _fixture.Build<Duration>()
                .With(d => d.StartDate, DateTime.Parse(startDate))
                .With(d => d.EndDate, DateTime.Parse(endDate))
                .Create();

            var sut = _fixture.Create<DateValidator>();

            var result = sut.ValidateDuration(duration, taxType);

            Assert.IsFalse(result);
        }
    }
}