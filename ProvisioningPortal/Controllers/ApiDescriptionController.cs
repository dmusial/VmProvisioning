using Microsoft.AspNetCore.Mvc;

namespace CloudProvisioningPortal.Controllers
{
    [Route("ourapi")]
    public class ApiDescriptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
