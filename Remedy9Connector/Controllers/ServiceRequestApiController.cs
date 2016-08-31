using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Remedy9Connector.Models;
using Remedy9Connector.Requests;

namespace Remedy9Connector.Controllers
{
    [Route("api")]
    public class ServiceRequestApiController : Controller
    {
        private const string _baseRemedyAddress = "http://remedy90.lukaszpiech.pl:8008";

        private readonly ILogger _logger;

        public ServiceRequestApiController (ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ServiceRequestApiController>();
        }

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
            var incidentNumber = GetIncidentNumber(token, createdTicketId);

            return new ObjectResult(new UserServiceRequestResult(incidentNumber));
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
                var authorizationRequest = new AuthorizationRequest(
                    _baseRemedyAddress, 
                    "Allen", 
                    "password"); // later will come from outside
                var response = httpClient.SendAsync(authorizationRequest).Result;

                return response.Content.ReadAsStringAsync().Result;
            }
        }

        private string CreateUserServiceRequest(string token, UserServiceRequest userServiceRequest)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new CreateUserServiceRequestRequest(
                    _baseRemedyAddress,
                    token,
                    userServiceRequest.Inputs.Description,
                    userServiceRequest.Inputs.RefrenceTaskId);

                var response = httpClient.SendAsync(request).Result;

                var ticketLocation = response.Headers.GetValues("Location").First();
                var ticketId = ticketLocation.Substring(ticketLocation.LastIndexOf('/') + 1);

                return ticketId;
            }
        }

        private string GetIncidentNumber(string token, string ticketId)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new GetIncidentNumberRequest(_baseRemedyAddress, token, ticketId);

                var response = httpClient.SendAsync(request).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic ticketDetails = JsonConvert.DeserializeObject(responseContent);

                return ticketDetails.values["Incident Number"];
            }
        }

        private string GetTicketStatus(string token, string ticketId)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new GetIncidentStatusRequest(_baseRemedyAddress, token, ticketId);

                var response = httpClient.SendAsync(request).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic ticketDetails = JsonConvert.DeserializeObject(responseContent);

                return ticketDetails.values.Status;
            }
        }
    }
}