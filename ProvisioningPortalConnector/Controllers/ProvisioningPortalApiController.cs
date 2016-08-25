using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProvisioningPortalConnector.Models;

namespace ProvisioningPortalConnector.Controllers
{
    [Route("api")]
    public class ProvisioningPortalApiController : Controller
    {
        [HttpPost]
        [Route("requestvm")]
        public IActionResult RequestVm([FromBody] VmProvisioningRequest requestDetails)
        {
            if (requestDetails == null)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                var request = new 
                {
                    VmSize = requestDetails.Inputs.VmSize,
                    Requestor = requestDetails.Inputs.Requestor
                };
                
                var jsonRequest = JsonConvert.SerializeObject(request);
                var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("http://provisioningportal:5000/api/requestvm", requestContent).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                dynamic deserializedReponse = JsonConvert.DeserializeObject(responseContent);
                var provisioningResult = new VmProvisioningResponse((Guid)deserializedReponse.requestId, (string)deserializedReponse.status);

                return new ObjectResult(provisioningResult);
            }
        }
    }
}