using System.IO;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Http;
using Moq;
using MunicipalTax.Logic.Interfaces.Services;
using MunicipalTax.Logic.Services;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using MunicipalTax.Public.Interfaces.v1.Request;
using NUnit.Framework;

namespace MunicipalTax.Logic.Tests.Services
{
    [TestFixture]
    public class FileServiceTests
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public void AddTaxFromFile_NotCsv_ReturnInvalid()
        {
            var fileMock = new Mock<IFormFile>();
            var fileName = "test.xml";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            var sut = _fixture.Create<FileService>();

            var result = sut.AddTaxFromFile(fileMock.Object);

            Assert.AreEqual(UploadStatus.Invalid, result);
        }

        [Test]
        public void AddTaxFromFile_Csv_ParseCsvAndCallTaxServiceAddTax()
        {
            var fileMock = new Mock<IFormFile>();
            var fileName = "test.csv";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            var fileContent = "MunicipalityName;TaxType;PeriodStart;PeriodEnd;Rate\n"
                              + "Copenhagen;Daily;23-05-2021;23-05-2021;0.2\n"
                              + "Copenhagen;Weekly;01-02-2021;08-02-2021;0.9\n";

            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(fileContent);
            streamWriter.Flush();
            memoryStream.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(memoryStream);
            fileMock.Setup(_ => _.Length).Returns(memoryStream.Length);

            var mockTaxService = Mock.Get(_fixture.Freeze<ITaxService>());

            var sut = _fixture.Create<FileService>();

            sut.AddTaxFromFile(fileMock.Object);

            mockTaxService.Verify(x => x.AddTax(It.IsAny<AddTaxRequest>()), Times.Exactly(2));
        }
    }
}
