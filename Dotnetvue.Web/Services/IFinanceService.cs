using System;
using System.Collections.Generic;
using Dotnetvue.Data.Models;

namespace Dotnetvue.Web.Services
{
    public interface IFinanceService
    {
        public List<FinanceRequest> GetFinanceRequests(int offset, int fetch);
        public FinanceRequestWrapper GetFinanceRequest(Guid id);

        public FinanceRequestWrapper SaveFinanceRequest(FinanceRequest financeRequest);

        public FinanceRequestOperationStatus DeleteFinanceRequest(Guid id);
    }
}