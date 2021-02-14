using MunicipalTax.Logic.Interfaces.Repositories;
using MunicipalTax.Logic.Interfaces.Validators;

namespace MunicipalTax.Logic.Validators
{
    public class MunicipalityValidator : IMunicipalityValidator
    {
        private readonly IMunicipalityRepository _municipalityRepository;

        public MunicipalityValidator(IMunicipalityRepository municipalityRepository)
        {
            _municipalityRepository = municipalityRepository;
        }

        public bool IsMunicipalityExist(string name)
        {
            var municipality = _municipalityRepository.GetMunicipality(name);
            if (municipality == null)
            {
                return false;
            }

            return true;
        }
    }
}
