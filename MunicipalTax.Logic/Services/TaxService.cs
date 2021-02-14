using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MunicipalTax.Logic.Interfaces.Repositories;
using MunicipalTax.Logic.Interfaces.Services;
using MunicipalTax.Logic.Interfaces.Validators;
using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using MunicipalTax.Public.Interfaces.v1.Request;
using MunicipalTax.Public.Interfaces.v1.Response;

namespace MunicipalTax.Logic.Services
{
    public class TaxService : ITaxService
    {
        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly IDateValidator _dateValidator;
        private readonly ITaxRepository _taxRepository;
        private readonly IMapper _mapper;

        public TaxService(IMunicipalityValidator municipalityValidator, 
            IDateValidator dateValidator, 
            ITaxRepository taxRepository,
            IMapper mapper)
        {
            _municipalityValidator = municipalityValidator;
            _dateValidator = dateValidator;
            _taxRepository = taxRepository;
            _mapper = mapper;
        }

        public AppliedTax FindAppliedTax(string municipalityName, string date)
        {
            if (!_municipalityValidator.IsMunicipalityExist(municipalityName))
            {
                return null;
            }

            var convertedDate = _dateValidator.ValidateAndConvertDateString(date);
            if (convertedDate == null)
            {
                return null;
            }

            List<Tax> municipalityTaxes = _taxRepository.GetMunicipalityTaxes(municipalityName);

            var sortedTaxes = municipalityTaxes.OrderBy(t => t.AppliedTax.TaxType);
            foreach (var sortedTax in sortedTaxes)
            {
                if (sortedTax.Duration.IsDateInsideDuration(convertedDate.Value))
                {
                    return sortedTax.AppliedTax;
                }
            }

            return null;
        }

        public AddTaxResponse AddTax(AddTaxRequest request)
        {
            var appliedTax = _mapper.Map<AppliedTax>(request.AppliedTax);
                
            var duration = new Duration
            {
                StartDate = _dateValidator.ValidateAndConvertDateString(request.Duration.PeriodStart).GetValueOrDefault(),
                EndDate = _dateValidator.ValidateAndConvertDateString(request.Duration.PeriodEnd).GetValueOrDefault()
            };

            if (!_dateValidator.ValidateDuration(duration, appliedTax.TaxType))
            {
                return new AddTaxResponse
                {
                    AddStatus = AddStatus.Invalid,
                    Message = "Dates do not correspond to Tax Type"
                };
            }

            var status = _taxRepository.AddTax(request.MunicipalityName, appliedTax, duration);

            return new AddTaxResponse
            {
                AddStatus = status,
                Message = string.Empty
            };
        }
    }
}
