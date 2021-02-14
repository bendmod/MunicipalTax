using System;
using MunicipalTax.Logic.Interfaces.Validators;
using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Logic.Validators
{
    public class DateValidator : IDateValidator
    {
        public DateTime? ValidateAndConvertDateString(string date)
        {
            try
            {
                DateTime result = DateTime.Parse(date);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool ValidateDuration(Duration duration, TaxTypes taxType)
        {
            switch (taxType)
            {
                case TaxTypes.Daily:
                    if (duration.StartDate.Date != duration.EndDate.Date)
                    {
                        return false;
                    }
                    break;
                case TaxTypes.Weekly:
                    if (!(duration.EndDate - duration.StartDate).TotalDays.Equals(6))
                        return false;
                    break;
                case TaxTypes.Monthly:
                    if (duration.StartDate.Month != duration.EndDate.Month
                        || duration.StartDate.Year != duration.EndDate.Year
                        || duration.StartDate.Day != 1
                        || duration.EndDate.Date.Day < 28)
                    {
                        return false;
                    }
                    break;
                case TaxTypes.Yearly:
                    if (duration.StartDate.Year != duration.EndDate.Year
                        || duration.StartDate.Month != 1
                        || duration.StartDate.Day != 1
                        || duration.EndDate.Month != 12
                        || duration.EndDate.Day != 31)
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }
    }
}
