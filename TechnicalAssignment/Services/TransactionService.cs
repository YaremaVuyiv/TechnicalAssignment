using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public TransactionService(
            ITransactionParserFactory transactionParserFactory,
            ITransactionValidatorFactory transactionValidatorFactory)
        {
            _transactionParserFactory = transactionParserFactory;
            _transactionValidatorFactory = transactionValidatorFactory;
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
                Data = transactions
            };
        }
    }
}
