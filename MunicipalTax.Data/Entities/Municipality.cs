using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MunicipalTax.Data.Entities
{
    public class Municipality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        public List<Tax> TaxList { get; set; }
    }
}
