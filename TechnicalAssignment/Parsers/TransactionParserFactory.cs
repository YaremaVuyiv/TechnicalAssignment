using System;
using TechnicalAssignment.Parsers.Contracts;

namespace TechnicalAssignment.Parsers
{
    public class TransactionParserFactory : ITransactionParserFactory
    {
        public ITransactionParser Create(string fileExtension)
        {
            switch (fileExtension)
            {
                case Constants.CsvFileExtension:
                    {
                        return new CsvTransactionParser();
                    }

                case Constants.XmlFileExtension:
                    {
                        return new XmlTransactionParser();
                    }

                default:
                    {
                        throw new ArgumentException("Unsupported extension type", nameof(fileExtension));
                    }
            }
        }
    }
}
