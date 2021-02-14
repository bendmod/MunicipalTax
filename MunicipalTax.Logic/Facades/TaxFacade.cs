using AutoMapper;
using MunicipalTax.Logic.Interfaces.Facades;
using MunicipalTax.Logic.Interfaces.Services;
using MunicipalTax.Public.Interfaces.v1.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using MunicipalTax.Public.Interfaces.v1.Request;
using MunicipalTax.Public.Interfaces.v1.Response;

namespace MunicipalTax.Logic.Facades
{
    public class TaxFacade : ITaxFacade
    {
        private readonly ITaxService _taxService;
        private readonly IMapper _mapper;

        public TaxFacade(ITaxService taxService, IMapper mapper)
        {
            _taxService = taxService;
            _mapper = mapper;
        }

        public GetTaxResponse GetTax(string municipalityName, string date)
        {
            var foundTax = _taxService.FindAppliedTax(municipalityName, date);
            if (foundTax != null)
            {
                AppliedTax appliedTax = _mapper.Map<AppliedTax>(foundTax);
                return new GetTaxResponse
                {
                    MunicipalityName = municipalityName,
                    Date = date,
                    AppliedTax = appliedTax,
                    Message = "Tax rate found"
                };
            }
            
            return new GetTaxResponse
            {
                MunicipalityName = municipalityName,
                Date = date,
                Message = "Either municipality not present or wrong date provided"
            };

        }

        public AddTaxResponse AddTax(AddTaxRequest request)
        {
            var response = _taxService.AddTax(request);
            if (string.IsNullOrEmpty(response.Message))
            {
                response.Message = BuildResponseMessage(response.AddStatus);
            }

            return response;
        }

        private string BuildResponseMessage(AddStatus status)
        {
            if (status == AddStatus.Added)
            {
                return "Tax information is added to the database";
            }

            if (status == AddStatus.AlreadyExist)
            {
                return "Tax information already exist in the database";
            }

            return "Invalid tax information";
        }
    }
}
