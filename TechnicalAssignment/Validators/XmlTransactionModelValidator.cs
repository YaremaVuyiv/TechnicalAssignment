using FluentValidation;
using System;
using TechnicalAssignment.Models;

namespace TechnicalAssignment.Validators
{
    public class XmlTransactionModelValidator: AbstractValidator<TransactionModel>
    {
        public XmlTransactionModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Amount)
                .NotNull()
                .WithMessage(x => $"Amount is missing in {x.Id ?? string.Empty} transaction")
                .Must(x => decimal.TryParse(x, out _))
                .WithMessage(x => $"Invalid amount for {x.Id ?? string.Empty} transaction"); ;

            RuleFor(x => x.CurrencyCode)
                .NotNull()
                .WithMessage(x => $"Currency code is missing for {x.Id ?? string.Empty} transaction")
                .Length(3)
                .WithMessage(x => $"Invalid currency code for {x.Id ?? string.Empty} transaction");

            RuleFor(x => x.Date)
                .NotNull()
                .WithMessage(x => $"Transaction date is missing for {x.Id ?? string.Empty} transaction")
                .Must(x => DateTime.TryParse(x, out _))
                .WithMessage(x => $"Invalid transaction date for {x.Id ?? string.Empty} transaction"); ;

            RuleFor(x => x.Status)
                .NotNull()
                .WithMessage(x => $"Status is missing for {x.Id ?? string.Empty} transaction")
                .Must(x => Enum.TryParse<XmlTransactionStatuses>(x, out _))
                .WithMessage(x => $"Invalid status for {x.Id ?? string.Empty} transaction"); ;
        }
    }
}
