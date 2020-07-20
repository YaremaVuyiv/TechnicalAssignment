namespace TechnicalAssignment.Parsers.Contracts
{
    public interface ITransactionParserFactory
    {
        ITransactionParser Create(string fileExtension);
    }
}
