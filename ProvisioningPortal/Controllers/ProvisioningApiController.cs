using System;
using System.Linq;
using CloudProvisioningPortal.Jobs;
using CloudProvisioningPortal.Models;
using CloudProvisioningPortal.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CloudProvisioningPortal.Controllers
{
    [Route("api")]
    public class ProvisioningApiController : Controller
    {
        private readonly InMemoryProvisioningStore _store;
        private readonly IScheduler _scheduler;
        private readonly ILogger _logger;

        public ProvisioningApiController (
            InMemoryProvisioningStore store, 
            IScheduler scheduler,
            ILoggerFactory loggerFactory)
        {
            _store = store;
            _scheduler = scheduler;
            _logger = loggerFactory.CreateLogger<ProvisioningApiController>();
        }

        [HttpGet]
        [Route("provisioningstatus")]
        public IActionResult Index()
        {
            var allRegisteredRequests = _store.GetAll().OrderByDescending(r => r.CreationDate).Select(r => new VmProvisioningRequestListItem()
            {
                RequestId = r.RequestId,
                Requestor = r.Requestor,
                VmSize = r.VmSize,
                Status = r.Status,
                CreationDate = r.CreationDate
            });

            return new ObjectResult(allRegisteredRequests);

        }

        [HttpGet]
        [Route("requeststatus/{id}")]
        public IActionResult RequestStatus(Guid id)
        {
            var request = _store.Get(id);
            if (request == null)
            {
                return NotFound();
            }

            return new ObjectResult(request.Status);

        }

        
        [HttpPost]
        [Route("requestvm")]
        public IActionResult RequestVm([FromBody] Models.VmProvisioningRequest requestDetails)
        {
            if (requestDetails == null)
            {
                return BadRequest();
            }

            var provisioningRequest = new Persistence.VmProvisioningRequest(requestDetails.Requestor, requestDetails.VmSize);
            _store.Register(provisioningRequest);

            IJobDetail requestFulfillmentJob = JobBuilder.Create<FulfillRequestJob>()
		        .WithIdentity("vmrequest_" + provisioningRequest.RequestId.ToString(), "vmprovisioning")
                .UsingJobData("requestId", provisioningRequest.RequestId.ToString())
		        .Build();
            
            ITrigger requestFulfillmentJobTrigger = TriggerBuilder.Create()
                .WithIdentity("requestFulFillmentJobTrigger_" + provisioningRequest.RequestId.ToString(), "vmprovisioning")
                .StartNow()
                .Build();

            _scheduler.ScheduleJob(requestFulfillmentJob, requestFulfillmentJobTrigger);

            var provisioningResult = new VmProvisioningResponse 
            {
                 RequestId = provisioningRequest.RequestId,
                 Status = provisioningRequest.Status
            };
            
            return new ObjectResult(provisioningResult);
        }
    }
}
