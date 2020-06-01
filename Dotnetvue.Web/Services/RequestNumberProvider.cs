using System;
using Dotnetvue.Data.Models;

namespace Dotnetvue.Web.Services
{
    public class RequestNumberProvider : IRequestNumberProvider
    {
        public string Generate(FinanceRequest request)
        {
            return $"KZ{DateTime.Now:ddMMyyyyhhmmss}{request.GetHashCode()}";
        }
    }
}