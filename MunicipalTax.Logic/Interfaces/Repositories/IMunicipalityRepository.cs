using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Logic.Interfaces.Repositories
{
    public interface IMunicipalityRepository
    {
        Municipality GetMunicipality(string name);
        AddStatus AddMunicipalityAndTax(string municipalityName, AppliedTax appliedTax, Duration duration);
    }
}
