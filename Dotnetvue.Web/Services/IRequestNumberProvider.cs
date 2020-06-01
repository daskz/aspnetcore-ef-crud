using Dotnetvue.Data.Models;

namespace Dotnetvue.Web.Services
{
    public interface IRequestNumberProvider
    {
        string Generate(FinanceRequest request);
    }
}