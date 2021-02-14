using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using MunicipalTax.Logic.Interfaces.Repositories;
using MunicipalTax.Logic.Models;
using MunicipalTax.Logic.Validators;
using NUnit.Framework;

namespace MunicipalTax.Logic.Tests.Validators
{
    [TestFixture]
    public class MunicipalityValidatorTests
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public void ValidateAndConvertDateString_NoMunicipalityFound_ReturnFalse()
        {
            var municipalityName = _fixture.Create<string>();

            var mockMunicipalityRepository = Mock.Get(_fixture.Freeze<IMunicipalityRepository>());
            mockMunicipalityRepository.Setup(q => q.GetMunicipality(It.IsAny<string>())).Returns<Municipality>(null);

            var sut = _fixture.Create<MunicipalityValidator>();

            var result = sut.IsMunicipalityExist(municipalityName);

            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateAndConvertDateString_MunicipalityFound_ReturnTrue()
        {
            var municipalityName = _fixture.Create<string>();
            var municipality = _fixture.Create<Municipality>();

            var mockMunicipalityRepository = Mock.Get(_fixture.Freeze<IMunicipalityRepository>());
            mockMunicipalityRepository.Setup(q => q.GetMunicipality(It.IsAny<string>())).Returns(municipality);

            var sut = _fixture.Create<MunicipalityValidator>();

            var result = sut.IsMunicipalityExist(municipalityName);

            Assert.IsTrue(result);
        }

    }
}
