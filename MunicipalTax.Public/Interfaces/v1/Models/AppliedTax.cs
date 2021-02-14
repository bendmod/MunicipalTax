using System.Text.Json.Serialization;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Public.Interfaces.v1.Models
{
    public class AppliedTax
    {
        [JsonPropertyName("taxRate")]
        public decimal TaxRate { get; set; }
        [JsonPropertyName("taxType")]
        public TaxTypes TaxType { get; set; } 
    }
}
