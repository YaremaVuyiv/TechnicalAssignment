using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechnicalAssignment.Data.Entities;
using TechnicalAssignment.Data.Repositories.Contracts;
using TechnicalAssignment.Mediatr.Requests;
using TechnicalAssignment.ResponseModels;
using TechnicalAssignment.Services.Contracts;

namespace TechnicalAssignment.Mediatr.Handlers
{
    public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionRequest, TransactionResponseModel>
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _repository;

        public CreateTransactionRequestHandler(
            ITransactionService transactionService,
            IMapper mapper,
            ITransactionRepository repository)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TransactionResponseModel> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            var response = new TransactionResponseModel();

            try
            {
                var transactionsData = await _transactionService.ParseAndValidateTransactionAsync(request.FileData);

                if (transactionsData.IsError)
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ErrorMessage = transactionsData.ErrorMessage;

                    return response;
                }

                var entities = _mapper.Map<IEnumerable<Transaction>>(transactionsData.Data);

                await _repository.CreateAsync(entities);

                response.StatusCode = StatusCodes.Status200OK;
            }
            catch(ArgumentException)
            {
                response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
            }
            catch (Exception)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }
    }
}
