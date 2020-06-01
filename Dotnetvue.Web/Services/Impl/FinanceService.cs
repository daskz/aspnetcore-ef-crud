using System;
using System.Collections.Generic;
using System.Linq;
using Dotnetvue.Data;
using Dotnetvue.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnetvue.Web.Services.Impl
{
    public class FinanceService : IFinanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRequestNumberProvider _numberProvider;
        private readonly IUserService _userService;

        public FinanceService(ApplicationDbContext context, IRequestNumberProvider numberProvider, IUserService userService)
        {
            _context = context;
            _numberProvider = numberProvider;
            _userService = userService;
        }

        public List<FinanceRequest> GetFinanceRequests(int offset, int fetch)
        {
            var currentUser = _userService.GetCurrentUser();
            return _context.FinanceRequests
                .Where(r => r.Author.Id == currentUser.Id)
                .OrderBy(r => r.Purpose)
                .Skip(offset)
                .Take(fetch)
                .ToList();
        }

        public FinanceRequestWrapper GetFinanceRequest(Guid id)
        {
            var request = GetRequest(id);
            var validationResult = ValidateForCurrentUser(request);
            if (validationResult.Code != FinanceRequestOperationStatus.Success)
            {
                return new FinanceRequestWrapper { Status = validationResult };
            }

            return new FinanceRequestWrapper
            {
                FinanceRequest = request, 
                Status = new FinanceRequestOperationStatus(FinanceRequestOperationStatus.Success)
            };
        }

        public FinanceRequestWrapper SaveFinanceRequest(FinanceRequest financeRequest)
        {
            var request = GetOrCreateRequest(financeRequest);

            var validationResult = ValidateForCurrentUser(request);
            if (validationResult.Code != FinanceRequestOperationStatus.Success)
            {
                return new FinanceRequestWrapper {Status = validationResult};
            }
            
            request.InterestRate = financeRequest.InterestRate;
            request.Amount = financeRequest.Amount;
            request.Purpose = financeRequest.Purpose;
            request.MonthCount = financeRequest.MonthCount;
            _context.FinanceRequests.Update(request);
            _context.SaveChanges();

            return new FinanceRequestWrapper
            {
                FinanceRequest = request,
                Status = new FinanceRequestOperationStatus(FinanceRequestOperationStatus.Success)
            };
        }

        private FinanceRequest GetOrCreateRequest(FinanceRequest financeRequest)
        {
            if (financeRequest.Id.HasValue)
                return GetRequest(financeRequest.Id.Value);

            financeRequest.Author = _userService.GetCurrentUser();
            financeRequest.PublicNumber = _numberProvider.Generate(financeRequest);
            _context.FinanceRequests.Add(financeRequest);
            _context.SaveChanges();
            return financeRequest;
        }

        public FinanceRequestOperationStatus DeleteFinanceRequest(Guid id)
        {
            var request = GetRequest(id);

            var validationResult = ValidateForCurrentUser(request);
            if (validationResult.Code != FinanceRequestOperationStatus.Success)
            {
                return validationResult;
            }

            _context.FinanceRequests.Remove(request);
            _context.SaveChanges();

            return new FinanceRequestOperationStatus(FinanceRequestOperationStatus.Success);
        }

        private FinanceRequest GetRequest(Guid id)
        {
            return _context.FinanceRequests.Include(r => r.Author).SingleOrDefault(r => r.Id == id);
        }

        private FinanceRequestOperationStatus ValidateForCurrentUser(FinanceRequest request)
        {
            if (request == null)
            {
                return new FinanceRequestOperationStatus(FinanceRequestOperationStatus.RequestNotFound);
            }

            if (request.Author.Id != _userService.GetCurrentUser().Id)
            {
                return new FinanceRequestOperationStatus(FinanceRequestOperationStatus.Unauthorized);
            }

            return new FinanceRequestOperationStatus(FinanceRequestOperationStatus.Success);
        }
    }
}