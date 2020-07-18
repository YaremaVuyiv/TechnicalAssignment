using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Mime;

namespace TechnicalAssignment.Controllers
{
    [Route("Transaction")]
    public class TransactionController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Index")]
        public IActionResult Index(IFormFile uploadFile)
        {
            var fileExtension = Path.GetExtension(uploadFile.FileName);
            if (!Constants.AllowedFileExtensions.Contains(fileExtension))
            {
                return View("Error");
            }

            return Ok();
        }
    }
}
