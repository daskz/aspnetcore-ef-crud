using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnetvue.Data.Models;
using Dotnetvue.Web.Models;
using Dotnetvue.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dotnetvue.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class FinanceRequestsController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public FinanceRequestsController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet]
        public IActionResult GetFinanceRequests(int offset, int fetch)
        {
            return Ok(_financeService.GetFinanceRequests(offset, fetch));
        }

        [HttpGet("{Id}")]
        public IActionResult GetFinanceRequests(Guid id)
        {
            var financeRequestWrapper = _financeService.GetFinanceRequest(id);
            if (financeRequestWrapper.Status.Code != FinanceRequestOperationStatus.Success)
            {
                return BadRequest(new MessageResponse(financeRequestWrapper.Status.Code));
            }
            return Ok(financeRequestWrapper.FinanceRequest);
        }

        [HttpPost]
        public IActionResult SaveFinanceRequest(FinanceRequest request)
        {
            var financeRequestWrapper = _financeService.SaveFinanceRequest(request);
            if (financeRequestWrapper.Status.Code != FinanceRequestOperationStatus.Success)
            {
                return BadRequest(new MessageResponse(financeRequestWrapper.Status.Code));
            }
            return Ok(financeRequestWrapper.FinanceRequest);
        }

        [HttpDelete("{Id}")]
        public IActionResult SaveFinanceRequest(Guid id)
        {
            var operationStatus = _financeService.DeleteFinanceRequest(id);
            if (operationStatus.Code != FinanceRequestOperationStatus.Success)
            {
                return BadRequest(new MessageResponse(operationStatus.Code));
            }
            return Ok();
        }
    }
}