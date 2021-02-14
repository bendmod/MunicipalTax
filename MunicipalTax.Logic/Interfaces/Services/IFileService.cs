using Microsoft.AspNetCore.Http;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Logic.Interfaces.Services
{
    public interface IFileService
    {
        UploadStatus AddTaxFromFile(IFormFile file);
    }
}
