using Microsoft.AspNetCore.Http;
using MunicipalTax.Logic.Interfaces.Facades;
using MunicipalTax.Logic.Interfaces.Services;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using MunicipalTax.Public.Interfaces.v1.Response;

namespace MunicipalTax.Logic.Facades
{
    public class FileFacade : IFileFacade
    {
        private readonly IFileService _fileService;

        public FileFacade(IFileService fileService)
        {
            _fileService = fileService;
        }

        public UploadFileResponse UploadFile(IFormFile file)
        {
           var status = _fileService.AddTaxFromFile(file);

           return new UploadFileResponse
           {
               UploadStatus = status,
               Message = BuildResponseMessage(status)
           };
        }

        private string BuildResponseMessage(UploadStatus status)
        {
            if(status == UploadStatus.FullyAdded)
            {
                return "All Records from Csv file where added to the database";
            }

            if (status == UploadStatus.PartiallyAdded)
            {
                return "Some records where not added to the database";
            }

            return "No record was added to the database, either invalid file or invalid records";
        }
    }
}
