using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("Id")]
        public async Task<IActionResult> GetTransactionById([FromQuery] string id)
        {
            var request = new GetTransactionByIdRequest
            {
                Id = id
            };

            var result = await _mediator.Send(request);

            return GetResponse(result);
        }

        [HttpGet("Status")]
        public async Task<IActionResult> GetTransactionsByStatus([FromQuery]string status)
        {
            var request = new GetTransactionByStatusRequest
            {
                Status = status
            };

            var result = await _mediator.Send(request);

            return GetResponse(result);
        }

        [HttpGet("Date")]
        public async Task<IActionResult> GetTransactionsByDate([FromQuery] DateTime fromDateTime, [FromQuery] DateTime toDateTime)
        {
            var request = new GetTransactionByDateRangeRequest
            {
                FromDateTime = fromDateTime,
                ToDateTime = toDateTime
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
                        return Ok();
                    }

                case StatusCodes.Status500InternalServerError:
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
            }

            return View("Index");
        }

        private IActionResult GetResponse<T>(TransactionResponseDataModel<T> responseModel)
        {
            if(responseModel.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(responseModel.Data);
            }

            return GetResponse(responseModel as TransactionResponseModel);
        }
    }
}
