using FluentValidation;
using TechnicalAssignment.Models;

namespace TechnicalAssignment.Validators.Contracts
{
    public interface ITransactionValidatorFactory
    {
        IValidator<TransactionModel> Create(string fileFormat);
    }
}
