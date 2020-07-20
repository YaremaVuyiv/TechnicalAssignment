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
    public class GetTransactionByIdRequestHandler : IRequestHandler<GetTransactionByIdRequest, TransactionResponseDataModel<GetTransactionResponseModel>>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public GetTransactionByIdRequestHandler(
            ITransactionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDataModel<GetTransactionResponseModel>> Handle(GetTransactionByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new TransactionResponseDataModel<GetTransactionResponseModel>();

            try
            {
                var entity = await _repository.GetTransactionByIdAsync(request.Id);

                var transaction = _mapper.Map<GetTransactionResponseModel>(entity);

                response.Data = new List<GetTransactionResponseModel>
                {
                    transaction
                };
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
