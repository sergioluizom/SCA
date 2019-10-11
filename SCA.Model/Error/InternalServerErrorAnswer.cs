namespace SCA.Model.Error
{
    public class InternalServerErrorAnswer
    {
        public InternalServerErrorAnswer(string internalCode, string message, string developerMessage)
        {
            InternalCode = internalCode;
            Message = message;
            DeveloperMessage = developerMessage;
        }

        public string InternalCode { get; set; }
        public string Message { get; set; }
        public string DeveloperMessage { get; set; }
    }
}