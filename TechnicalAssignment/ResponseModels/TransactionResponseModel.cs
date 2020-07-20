namespace TechnicalAssignment.ResponseModels
{
    public class TransactionResponseModel
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
            }
        }
    }
}
