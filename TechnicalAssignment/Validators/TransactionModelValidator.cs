using FluentValidation;
using System;
using TechnicalAssignment.Models;

namespace TechnicalAssignment.Validators
{
    public class TransactionModelValidator: AbstractValidator<TransactionModel>
    {
        public TransactionModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Amount)
                .NotNull()
                .Must(x => decimal.TryParse(x, out _))
                .WithMessage("Invalid currency code");

            RuleFor(x => x.CurrencyCode)
                .NotNull()
                .Length(3);

            RuleFor(x => x.Date)
                .NotNull()
                .Must(x => DateTime.TryParse(x, out _));

            RuleFor(x => x.Status)
                .NotNull()
                .Must(x => Enum.TryParse<XmlTransactionStatuses>(x, out _));
        }
    }
}
