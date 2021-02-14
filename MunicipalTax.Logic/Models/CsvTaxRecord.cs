namespace MunicipalTax.Logic.Models
{
    public class CsvTaxRecord
    {
        public string MunicipalityName { get; set; }
        public string TaxType { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        public decimal TaxRate { get; set; }
    }
}
