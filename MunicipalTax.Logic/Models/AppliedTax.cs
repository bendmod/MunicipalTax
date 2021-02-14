using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Logic.Models
{
    public class AppliedTax
    {
        public decimal TaxRate { get; set; }
        public TaxTypes TaxType { get; set; }

        public string GetTaxTypeString()
        {
            return TaxType.ToString("G");
        }
    }
}
