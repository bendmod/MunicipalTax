using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using MunicipalTax.Public.Interfaces.v1.Request;
using MunicipalTax.Public.Interfaces.v1.Response;

namespace MunicipalTax.Logic.Interfaces.Services
{
    public interface ITaxService
    {
        AppliedTax FindAppliedTax(string municipalityName, string date);
        AddTaxResponse AddTax(AddTaxRequest request);
    }
}
