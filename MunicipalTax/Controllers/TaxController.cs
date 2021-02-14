using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MunicipalTax.Logic.Interfaces.Facades;
using MunicipalTax.Public.Interfaces.v1.Request;
using MunicipalTax.Public.Interfaces.v1.Response;

namespace MunicipalTax.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> _logger;
        private readonly ITaxFacade _taxFacade;

        public TaxController(ILogger<TaxController> logger, ITaxFacade taxFacade)
        {
            _logger = logger;
            _taxFacade = taxFacade;
        }

        [HttpGet]
        public GetTaxResponse Get(string municipalityName, string date)
        {
            return _taxFacade.GetTax(municipalityName, date);
        }

        [HttpPost]
        public AddTaxResponse Post(AddTaxRequest request)
        {
            return _taxFacade.AddTax(request);
        }
        
    }
}
