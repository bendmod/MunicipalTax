using System;

namespace MunicipalTax.Logic.Models
{
    public class Duration
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsDateInsideDuration(DateTime date)
        {
            if (StartDate <= date && date <= EndDate)
            {
                return true;
            }

            return false;
        }
    }
}
