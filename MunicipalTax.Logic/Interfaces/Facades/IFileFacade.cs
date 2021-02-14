using Microsoft.AspNetCore.Http;
using MunicipalTax.Public.Interfaces.v1.Response;

namespace MunicipalTax.Logic.Interfaces.Facades
{
    public interface IFileFacade
    {
        UploadFileResponse UploadFile(IFormFile file);
    }
}
