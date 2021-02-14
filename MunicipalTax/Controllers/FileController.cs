using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MunicipalTax.Logic.Interfaces.Facades;
using MunicipalTax.Public.Interfaces.v1.Response;

namespace MunicipalTax.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileFacade _fileFacade;

        public FileController(IFileFacade fileFacade)
        {
            _fileFacade = fileFacade;
        }

        // POST api/<FileController>
        [HttpPost]
        public UploadFileResponse UploadFile(IFormFile file)
        {
            return _fileFacade.UploadFile(file);
        }
    }
}
