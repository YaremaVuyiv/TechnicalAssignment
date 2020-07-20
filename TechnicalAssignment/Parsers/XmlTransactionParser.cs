using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechnicalAssignment.Models;
using TechnicalAssignment.Parsers.Contracts;

namespace TechnicalAssignment.Parsers
{
    public class XmlTransactionParser : ITransactionParser
    {
        public async Task<IEnumerable<TransactionModel>> Parse(IFormFile file)
        {
            using (var xmlReader = file.OpenReadStream())
            {
                var fileContent = await XElement.LoadAsync(xmlReader, LoadOptions.None, default);

                var transactions = fileContent.Elements().Select(x => {
                    var id = x.Attribute(XName.Get("id"))?.Value;
                    var transactionDate = x.Element(XName.Get("TransactionDate"))?.Value;
                    var paymentDetailsElement = x.Element(XName.Get("PaymentDetails"));
                    var amount = paymentDetailsElement?.Element(XName.Get("Amount"))?.Value;
                    var currencyElementValue = paymentDetailsElement?.Element(XName.Get("CurrencyCode"))?.Value;
                    var status = x.Element(XName.Get("Status"))?.Value;

                    return new TransactionModel
                    {
                        Id = id,
                        Amount = amount,
                        CurrencyCode = currencyElementValue,
                        Date = transactionDate,
                        Status = status
                    };
                });

                return transactions;
            }
        }
    }
}
