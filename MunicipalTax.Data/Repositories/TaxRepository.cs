using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MunicipalTax.Logic.Interfaces.Repositories;
using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Data.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly DataBaseContext _dataBaseContext;

        public TaxRepository(DataBaseContext dataBaseContext,
            IMunicipalityRepository municipalityRepository)
        {
            _dataBaseContext = dataBaseContext;
            _municipalityRepository = municipalityRepository;
        }

        public List<Tax> GetMunicipalityTaxes(string municipalityName)
        {
            var foundMunicipality = _dataBaseContext.Municipalities
                .Include(m => m.TaxList)
                .First(m => m.MunicipalityName.Equals(municipalityName));

            var result = new List<Tax>();
            foreach (var tax in foundMunicipality.TaxList)
            {
                result.Add(
                    new Tax
                    {
                        AppliedTax = new AppliedTax
                        {
                            TaxRate = tax.Rate,
                            TaxType = ParseTaxType(tax)
                        },
                        Duration = new Duration
                        {
                            StartDate = tax.StartDate,
                            EndDate = tax.EndDate
                        }
                    });
            }

            return result;
        }

        public AddStatus AddTax(string municipalityName, AppliedTax appliedTax, Duration duration)
        {
            var foundMunicipality = _dataBaseContext.Municipalities
                .Include(m => m.TaxList)
                .FirstOrDefault(m => m.MunicipalityName.Equals(municipalityName));

            if (foundMunicipality == null)
            {
                return _municipalityRepository.AddMunicipalityAndTax(municipalityName, appliedTax, duration);
            }

            if (IsTaxRecordExist(foundMunicipality.TaxList, appliedTax, duration))
            {
                return AddStatus.AlreadyExist;
            }

            foundMunicipality.TaxList.Add(
                new Entities.Tax
                    {
                        StartDate = duration.StartDate,
                        EndDate = duration.EndDate,
                        Rate = appliedTax.TaxRate,
                        TaxType = appliedTax.GetTaxTypeString()
                    });

            _dataBaseContext.SaveChanges();
            return AddStatus.Added;
        }

       
        private bool IsTaxRecordExist(List<Entities.Tax> taxList, AppliedTax appliedTax, Duration duration)
        {
            return taxList.Any(
                t => t.Rate.Equals(appliedTax.TaxRate)
                     && t.TaxType.Equals(appliedTax.GetTaxTypeString())
                     && t.StartDate.Date == duration.StartDate
                     && t.EndDate == duration.EndDate);
        }

        private static TaxTypes ParseTaxType(Entities.Tax tax)
        {
            return (TaxTypes)Enum.Parse(typeof(TaxTypes), tax.TaxType);
        }
    }
}
