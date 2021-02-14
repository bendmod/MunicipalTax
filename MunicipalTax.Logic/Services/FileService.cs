using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using MunicipalTax.Logic.Interfaces.Services;
using MunicipalTax.Logic.Mapping;
using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;
using MunicipalTax.Public.Interfaces.v1.Request;
using TinyCsvParser;
using Duration = MunicipalTax.Public.Interfaces.v1.Models.Duration;

namespace MunicipalTax.Logic.Services
{
    public class FileService : IFileService
    {
        private readonly ITaxService _taxService;

        public FileService(ITaxService taxService)
        {
            _taxService = taxService;
        }

        public UploadStatus AddTaxFromFile(IFormFile file)
        {
            if (string.IsNullOrEmpty(file.FileName) 
                || !file.FileName.Contains("csv"))
            {
                return UploadStatus.Invalid;
            }

            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
            CsvTaxMapping csvTaxMapper = new CsvTaxMapping();
            CsvParser<CsvTaxRecord> csvParser = new CsvParser<CsvTaxRecord>(csvParserOptions, csvTaxMapper);

            var taxRecords = csvParser
                .ReadFromStream(file.OpenReadStream(), Encoding.ASCII)
                .ToList();

            List<AddStatus> statuses = new List<AddStatus>();

            foreach (var mappingResult in taxRecords)
            {
                if (mappingResult.IsValid)
                {
                    statuses.Add(AddTaxRecord(mappingResult.Result));
                }

                statuses.Add(AddStatus.Invalid);
            }

            return AggregateUploadStatus(statuses);
        }

        private AddStatus AddTaxRecord(CsvTaxRecord taxRecord)
        {
            AddTaxRequest request = new AddTaxRequest
                {
                    MunicipalityName = taxRecord.MunicipalityName,
                    AppliedTax = new Public.Interfaces.v1.Models.AppliedTax
                    { 
                        TaxType = ParseTaxType(taxRecord.TaxType),
                        TaxRate = taxRecord.TaxRate
                    },
                    Duration = new Duration
                    {
                        PeriodStart = taxRecord.PeriodStart,
                        PeriodEnd = taxRecord.PeriodEnd
                    }
            };

            return _taxService.AddTax(request).AddStatus;
        }
        
        private static TaxTypes ParseTaxType(string tax)
        {
            return (TaxTypes)Enum.Parse(typeof(TaxTypes), tax);
        }

        private UploadStatus AggregateUploadStatus(List<AddStatus> statuses)
        {
            if (statuses.All(s => s == AddStatus.Added))
                return UploadStatus.FullyAdded;

            if (statuses.All(s => s == AddStatus.Invalid))
                return UploadStatus.Invalid;

            return UploadStatus.PartiallyAdded;
        }
    }
}
