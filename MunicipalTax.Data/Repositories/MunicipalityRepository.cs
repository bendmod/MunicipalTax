using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MunicipalTax.Logic.Interfaces.Repositories;
using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using Tax = MunicipalTax.Data.Entities.Tax;

namespace MunicipalTax.Data.Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IMapper _mapper;

        public MunicipalityRepository(DataBaseContext dataBaseContext, IMapper mapper)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
        }

        public Municipality GetMunicipality(string name)
        {
            var foundMunicipality = _dataBaseContext.Municipalities
                .FirstOrDefault(m => m.MunicipalityName.Equals(name));

            return _mapper.Map<Municipality>(foundMunicipality);
        }

        public AddStatus AddMunicipalityAndTax(string municipalityName, AppliedTax appliedTax, Duration duration)
        {
            try
            {
                _dataBaseContext.Municipalities.Add(
                    new Entities.Municipality
                    {
                        MunicipalityName = municipalityName,
                        TaxList = BuildTaxList(appliedTax, duration)
                    });
                _dataBaseContext.SaveChanges();
            }
            catch (Exception e)
            {
                return AddStatus.Invalid;
            }

            return AddStatus.Added;
        }

        private List<Tax> BuildTaxList(AppliedTax appliedTax, Duration duration)
        {
            return new List<Tax>
            {
                new Tax
                {
                    StartDate = duration.StartDate,
                    EndDate = duration.EndDate,
                    Rate = appliedTax.TaxRate,
                    TaxType = appliedTax.GetTaxTypeString()
                }
            };
        }
    }
}
