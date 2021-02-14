using MunicipalTax.Public.Interfaces.v1.Models;

namespace MunicipalTax.Public.Interfaces.v1.Request
{
    public class AddTaxRequest
    {
        public string MunicipalityName { get; set; }
        public Duration Duration { get; set; }
        public AppliedTax AppliedTax { get; set; }
    }
}
