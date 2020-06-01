namespace Dotnetvue.Web.Services
{
    public class FinanceRequestOperationStatus
    {
        public FinanceRequestOperationStatus(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
        public const string Success = "Успешно";
        public const string RequestNotFound = "Запрос не найден";
        public const string Unauthorized = "Доступ запрещен";
    }
}