using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Public.Interfaces.v1.Response
{
    public class AddTaxResponse
    {
        public AddStatus AddStatus { get; set; }
        public string Message { get; set; }
    }
}
