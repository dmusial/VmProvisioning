using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Remedy9Connector.Models;

namespace Remedy9Connector.Controllers
{
    [Route("api")]
    public class ServiceRequestApiController : Controller
    {
        [HttpPost]
        [Route("userservicerequest")]
        public IActionResult CreateUserServiceRequest([FromBody] UserServiceRequest userServiceRequest)
        {
            if (userServiceRequest == null)
            {
                return BadRequest();
            }

            var token = Authenticate();
            var createdTicketId = CreateUserServiceRequest(token, userServiceRequest);

            return new ObjectResult(new UserServiceRequestResult(createdTicketId));
        }

        [HttpPost]
        [Route("userservicerequeststatus")]
        public IActionResult CheckUserServiceRequestStatus([FromBody] UserServiceRequestStatus userServiceRequest)
        {
            if (userServiceRequest == null)
            {
                return BadRequest();
            }

            var token = Authenticate();
            var status = GetTicketStatus(token, userServiceRequest.Inputs.TicketId);

            return new ObjectResult(new UserServiceRequestStatusResponse(status));
        }

        private string Authenticate()
        {
            using (var httpClient = new HttpClient())
            {
                var loginAndPass = string.Format("username={0}&password={1}", "Allen", "password");
                var loginRequestContent = new StringContent(loginAndPass, Encoding.UTF8, "application/json");
                var loginUri = "http://remedy90.lukaszpiech.pl:8008/api/jwt/login";
                var response = httpClient.PostAsync(loginUri, loginRequestContent).Result;
                
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        private string CreateUserServiceRequest(string authenticationToken, UserServiceRequest userServiceRequest)
        {
            using (var httpClient = new HttpClient())
            {
                var userServiceRequestDetails = new
                {
                    Values = new 
                    {
                        First_Name = "Capgemini",
                        Last_Name = "MyCloud",
                        Description = userServiceRequest.Inputs.Description,
                        Impact = "3-Moderate/Limited",
                        Urgency = "3-Medium",
                        Status = "Assigned",
                        Reported_Source = "Systems Management" ,
                        Service_Type = "User Service Request",
                        z1D_Action = "CREATE",
                        External_System = "Capgemini MyCloud",
                        External_ID = userServiceRequest.Inputs.RefrenceTaskId
                    }
                };

                var authToken = string.Format("AR-JWT {0}", authenticationToken);
                var userServiceRequestDetailsJson = JsonConvert.SerializeObject(userServiceRequestDetails);
                var body = new StringContent(userServiceRequestDetailsJson, Encoding.UTF8, "application/json");
                
                var loginUri = "http://remedy90.lukaszpiech.pl:8008/api/arsys/v1/entry/HPD:IncidentInterface_Create";
                var response = httpClient.PostAsync(loginUri, body).Result;

                var ticketLocation = response.Headers.GetValues("Location").First();
                var ticketId = ticketLocation.Substring(ticketLocation.LastIndexOf('/'));

                return ticketId;
            }
        }

        private string GetTicketStatus(string token, string ticketId)
        {
            using (var httpClient = new HttpClient())
            {
                var statusCheckUri = string.Format("http://remedy90.lukaszpiech.pl:8008/api/arsys/v1/entry/HPD:IncidentInterface_Create/{0}", ticketId);
                var response = httpClient.GetAsync(statusCheckUri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic ticketDetails = JsonConvert.DeserializeObject(responseContent);

                return (string)ticketDetails.Values.Status;
            }
        }
    }
}