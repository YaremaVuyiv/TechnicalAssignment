using FluentValidation;
using System;
using TechnicalAssignment.Models;
using TechnicalAssignment.Validators.Contracts;

namespace TechnicalAssignment.Validators
{
    public class TransactionValidatorFactory : ITransactionValidatorFactory
    {
        public IValidator<TransactionModel> Create(string fileExtension)
        {
            switch (fileExtension)
            {
                case Constants.CsvFileExtension:
                    {
                        return new CsvTransactionModelValidator();
                    }

                case Constants.XmlFileExtension:
                    {
                        return new XmlTransactionModelValidator();
                    }

                default:
                    {
                        throw new ArgumentException("Unsupported extension type", nameof(fileExtension));
                    }
            }
        }
    }
}
