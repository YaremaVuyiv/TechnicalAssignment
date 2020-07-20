using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Repositories.Contracts;
using TechnicalAssignment.Mediatr.Requests;
using TechnicalAssignment.Models;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Mediatr.Handlers
{
    public class GetTransactionByStatusRequestHandler : IRequestHandler<GetTransactionByStatusRequest, TransactionResponseDataModel<GetTransactionResponseModel>>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public GetTransactionByStatusRequestHandler(
            ITransactionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDataModel<GetTransactionResponseModel>> Handle(GetTransactionByStatusRequest request, CancellationToken cancellationToken)
        {
            var response = new TransactionResponseDataModel<GetTransactionResponseModel>();

            try
            {
                var status = GetStatusByFirstLetter(request.Status);

                var entities = await _repository.GetTransactionByStatusAsync((int)status);

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

        private UnifiedTransactionStatuses GetStatusByFirstLetter(string status)
        {
            switch (status)
            {
                case "A":
                    return UnifiedTransactionStatuses.Approved;

                case "D":
                    return UnifiedTransactionStatuses.Done;

                case "R":
                    return UnifiedTransactionStatuses.Rejected;

                default:
                    throw new ArgumentException("Invalid status", nameof(status));
            }
        }
    }
}
