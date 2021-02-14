using System.Text.Json.Serialization;
using MunicipalTax.Public.Interfaces.v1.Models;

namespace MunicipalTax.Public.Interfaces.v1.Response
{
    public class GetTaxResponse
    {
        [JsonPropertyName("municipalityName")]
        public string MunicipalityName { get; set; }
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("appliedTax")]
        public AppliedTax AppliedTax { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
