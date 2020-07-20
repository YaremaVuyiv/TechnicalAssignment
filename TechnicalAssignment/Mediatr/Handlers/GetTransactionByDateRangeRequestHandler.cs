using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Repositories.Contracts;
using TechnicalAssignment.Mediatr.Requests;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Mediatr.Handlers
{
    public class GetTransactionByDateRangeRequestHandler : IRequestHandler<GetTransactionByDateRangeRequest, TransactionResponseDataModel<GetTransactionResponseModel>>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public GetTransactionByDateRangeRequestHandler(
            ITransactionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDataModel<GetTransactionResponseModel>> Handle(GetTransactionByDateRangeRequest request, CancellationToken cancellationToken)
        {
            var response = new TransactionResponseDataModel<GetTransactionResponseModel>();

            try
            {
                var entities = await _repository.GetTransactionByDateRangeAsync(request.FromDateTime, request.ToDateTime);

                var transactions = _mapper.Map<IEnumerable<GetTransactionResponseModel>>(entities);

                response.Data = transactions;
                response.StatusCode = StatusCodes.Status200OK;
            }
            catch (Exception)
            {
                return new TransactionResponseDataModel<GetTransactionResponseModel>
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return response;
        }
    }
}
