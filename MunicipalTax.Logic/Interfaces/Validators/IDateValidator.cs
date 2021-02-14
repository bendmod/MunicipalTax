using System;
using MunicipalTax.Logic.Models;
using MunicipalTax.Public.Interfaces.v1.Models.Enums;

namespace MunicipalTax.Logic.Interfaces.Validators
{
    public interface IDateValidator
    {
        DateTime? ValidateAndConvertDateString(string date);
        bool ValidateDuration(Duration duration, TaxTypes taxType);
    }
}
