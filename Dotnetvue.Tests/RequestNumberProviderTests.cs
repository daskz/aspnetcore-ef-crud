using System;
using Dotnetvue.Data.Models;
using Dotnetvue.Web.Services;
using Dotnetvue.Web.Services.Impl;
using Xunit;

namespace Dotnetvue.Tests
{
    public class RequestNumberProviderTests
    {
        [Fact]
        public void GenerateCorrectNumberTest()
        {
            var provider = new RequestNumberProvider();
            var financeRequest = new FinanceRequest
            {
                Id = null,
                Purpose = "TEST PURPOSE",
                Amount = 5000000,
                MonthCount = 36,
                InterestRate = 15
            };
            
            var dateString = DateTime.Now.ToString("ddMMyyyyhhmmss");
            var number = provider.Generate(financeRequest);

            Assert.Equal($"KZ{dateString}{financeRequest.GetHashCode()}", number);
        }
    }
}
