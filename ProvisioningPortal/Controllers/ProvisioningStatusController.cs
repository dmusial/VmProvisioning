using CloudProvisioningPortal.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace CloudProvisioningPortal.Controllers
{
    [Route("provisioningstatus")]
    public class ProvisioningStatusController : Controller
    {
        private readonly InMemoryProvisioningStore _store;

        public ProvisioningStatusController(InMemoryProvisioningStore store)
        {
            _store = store;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
