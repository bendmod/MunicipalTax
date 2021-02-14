using MunicipalTax.Public.Interfaces.v1.Request;
using MunicipalTax.Public.Interfaces.v1.Response;

namespace MunicipalTax.Logic.Interfaces.Facades
{
    public interface ITaxFacade
    {
        GetTaxResponse GetTax(string municipalityName, string date);
        AddTaxResponse AddTax(AddTaxRequest request);
    }
}
