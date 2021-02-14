using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Public.Interfaces.v1.Response
{
    public class UploadFileResponse
    {
        public UploadStatus UploadStatus { get; set; }
        public string Message { get; set; }
    }
}
