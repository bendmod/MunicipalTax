using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using MunicipalTax.Logic.Interfaces.Repositories;
using MunicipalTax.Logic.Interfaces.Validators;
using MunicipalTax.Logic.Models;
using MunicipalTax.Logic.Services;
using MunicipalTax.Logic.Validators;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using MunicipalTax.Public.Interfaces.v1.Request;
using NUnit.Framework;

namespace MunicipalTax.Logic.Tests.Services
{
    public class TaxServiceTests
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public void FindAppliedTax_TaxDateInRange_ReturnAppliedTax()
        {
            var date = "14-02-2021";
            var municipalityName = _fixture.Create<string>();
            var rate = _fixture.Create<decimal>();

            var duration = _fixture.Build<Duration>()
                .With(d => d.StartDate, new DateTime(2021,1,1))
                .With(d => d.EndDate, new DateTime(2021, 12, 31)).Create();
            AppliedTax appliedTax = _fixture.Build<AppliedTax>()
                .With(a => a.TaxType, TaxTypes.Yearly)
                .With(a => a.TaxRate, rate).Create();
            var taxList = _fixture.Build<Tax>()
                .With(t => t.Duration, duration)
                .With(t => t.AppliedTax, appliedTax)
                .CreateMany(1).ToList();

            _fixture.Inject<IDateValidator>(_fixture.Create<DateValidator>());

            var mockMunicipalityValidator = Mock.Get(_fixture.Freeze<IMunicipalityValidator>());
            mockMunicipalityValidator.Setup(q => q.IsMunicipalityExist(It.IsAny<string>())).Returns(true);

            var mockTaxRepository = Mock.Get(_fixture.Freeze<ITaxRepository>());
            mockTaxRepository.Setup(q => q.GetMunicipalityTaxes(It.IsAny<string>())).Returns(taxList);

            var sut = _fixture.Create<TaxService>();

            var result = sut.FindAppliedTax(municipalityName, date);
            
            Assert.AreEqual(rate, result.TaxRate);
        }

        [Test]
        public void AddTax_ValidRequest_CallTaxRepositoryAdd()
        {
            var request = _fixture.Create<AddTaxRequest>();
            var mockTaxRepository = Mock.Get(_fixture.Freeze<ITaxRepository>());

            var mockDateValidator = Mock.Get(_fixture.Freeze<IDateValidator>());
            mockDateValidator.Setup(q => q.ValidateDuration(It.IsAny<Duration>(), It.IsAny<TaxTypes>())).Returns(true);

            var sut = _fixture.Create<TaxService>();

            sut.AddTax(request);
            mockTaxRepository.Verify(x => x.AddTax(It.IsAny<string>(), It.IsAny<AppliedTax>(), It.IsAny<Duration>()), Times.Once);
        }
    }
}
