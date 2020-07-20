using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using TechnicalAssignment.Models;
using TechnicalAssignment.Parsers.Contracts;
using System.Linq;
using CsvHelper.Configuration;

namespace TechnicalAssignment.Parsers
{
    public class CsvTransactionParser : ITransactionParser
    {
        public async Task<IEnumerable<TransactionModel>> Parse(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var csvConfiguration = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = false,
                        TrimOptions = TrimOptions.Trim
                    };

                    using (var csvReader = new CsvReader(streamReader, csvConfiguration))
                    {
                        var transactions = await csvReader.GetRecordsAsync<TransactionModel>().ToListAsync();
                        return transactions;
                    }
                }
            }
        }
    }
}
