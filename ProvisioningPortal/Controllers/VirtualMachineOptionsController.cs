using Microsoft.AspNetCore.Mvc;

namespace CloudProvisioningPortal.Controllers
{
    [Route("virtualmachineoptions")]
    public class VirtualMachineOptionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
