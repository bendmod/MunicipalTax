using System.Collections.Generic;
using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Logic.Interfaces.Repositories
{
    public interface ITaxRepository
    {
        List<Tax> GetMunicipalityTaxes(string municipalityName);
        AddStatus AddTax(string municipalityName, AppliedTax appliedTax, Duration duration);
    }
}
