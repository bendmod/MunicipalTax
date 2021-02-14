using System.Text.Json.Serialization;

namespace MunicipalTax.Public.Interfaces.v1.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaxTypes
    {
        Daily,
        Weekly,
        Monthly,
        Yearly,
    }
}
