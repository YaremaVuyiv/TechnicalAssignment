using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssignment.Models;
using TechnicalAssignment.Parsers.Contracts;
using TechnicalAssignment.ResponseModels;
using TechnicalAssignment.Services.Contracts;
using TechnicalAssignment.Validators.Contracts;

namespace TechnicalAssignment.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionParserFactory _transactionParserFactory;
        private readonly ITransactionValidatorFactory _transactionValidatorFactory;
        private readonly IMapper _mapper;

        public TransactionService(
            ITransactionParserFactory transactionParserFactory,
            ITransactionValidatorFactory transactionValidatorFactory,
            IMapper mapper)
        {
            _transactionParserFactory = transactionParserFactory;
            _transactionValidatorFactory = transactionValidatorFactory;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDataModel> ParseAndValidateTransactionAsync(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var parser = _transactionParserFactory.Create(fileExtension);

            var transactions = await parser.Parse(file);

            var validator = _transactionValidatorFactory.Create(fileExtension);

            var strBuilder = new StringBuilder();
            transactions.ToList().ForEach(x =>
            {
                var validationResult = validator.Validate(x);

                if (!validationResult.IsValid)
                {
                    validationResult.Errors.ToList().ForEach(error =>
                    {
                        strBuilder.AppendLine(error.ErrorMessage);
                    });
                }
            });

            var errorMessage = strBuilder.ToString();

            return new TransactionResponseDataModel
            {
                StatusCode = string.IsNullOrEmpty(errorMessage) ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                ErrorMessage = errorMessage,
                Data = MapTransaction(fileExtension, transactions)
            };
        }

        private IEnumerable<TransactionBaseModel> MapTransaction(string fileExtension, IEnumerable<TransactionModel> transactions)
        {
            switch (fileExtension)
            {
                case Constants.CsvFileExtension:
                    {
                        return _mapper.Map<IEnumerable<CsvTransactionModel>>(transactions);
                    }

                case Constants.XmlFileExtension:
                    {
                        return _mapper.Map<IEnumerable<XmlTransactionModel>>(transactions);
                    }

                default:
                    {
                        throw new ArgumentException("Unsupported extension type", nameof(fileExtension));
                    }
            }
        }
    }
}
