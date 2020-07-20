using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechnicalAssignment.Mediatr.Requests;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Controllers
{
    [Route("Transaction")]
    public class TransactionController : Controller
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Index")]
        public async Task<IActionResult> Index(IFormFile uploadFile)
        {
            var request = new CreateTransactionRequest
            {
                FileData = uploadFile
            };

            var result = await _mediator.Send(request);

            return GetResponse(result);
        }

        private IActionResult GetResponse(TransactionResponseModel responseModel)
        {
            switch (responseModel.StatusCode)
            {
                case StatusCodes.Status415UnsupportedMediaType:
                    {
                        return View("Error");
                    }

                case StatusCodes.Status400BadRequest:
                    {
                        return BadRequest(responseModel);
                    }

                case StatusCodes.Status200OK:
                    {
                        return Content("Congrats");
                    }

                case StatusCodes.Status500InternalServerError:
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
            }

            return View("Index");
        }
    }
}
