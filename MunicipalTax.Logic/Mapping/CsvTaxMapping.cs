using MunicipalTax.Logic.Models;
using TinyCsvParser.Mapping;

namespace MunicipalTax.Logic.Mapping
{
    public class CsvTaxMapping : CsvMapping<CsvTaxRecord>
    {
        public CsvTaxMapping () : base()
        {
            MapProperty(0, x => x.MunicipalityName);
            MapProperty(1, x => x.TaxType);
            MapProperty(2, x => x.PeriodStart);
            MapProperty(3, x => x.PeriodEnd);
            MapProperty(4, x => x.TaxRate);
        }
    }
}
