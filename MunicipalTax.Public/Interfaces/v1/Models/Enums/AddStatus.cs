using System.Text.Json.Serialization;

namespace MunicipalTax.Public.Interfaces.v1.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AddStatus
    {
        Added,
        AlreadyExist,
        Invalid
    }
}
