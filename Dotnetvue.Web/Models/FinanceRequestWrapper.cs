using Dotnetvue.Data.Models;

namespace Dotnetvue.Web.Services
{
    public class FinanceRequestWrapper
    {
        public FinanceRequest FinanceRequest { get; set; }
        public FinanceRequestOperationStatus Status { get; set; }
    }
}