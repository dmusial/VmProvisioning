using System.Linq;
using CloudProvisioningPortal.Models;
using CloudProvisioningPortal.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace CloudProvisioningPortal.Controllers
{
    [Route("api")]
    public class ProvisioningApiController : Controller
    {
        private readonly InMemoryProvisioningStore _store;

        public ProvisioningApiController (InMemoryProvisioningStore store)
        {
            _store = store;
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

            var provisioningResult = new VmProvisioningResponse 
            {
                 RequestId = provisioningRequest.RequestId,
                 Status = provisioningRequest.Status
            };
            
            return new ObjectResult(provisioningResult);
        }
    }
}
