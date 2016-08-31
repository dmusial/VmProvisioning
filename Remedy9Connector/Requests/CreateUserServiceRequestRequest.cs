using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Remedy9Connector.Requests
{
    public class CreateUserServiceRequestRequest : HttpRequestMessage
    {
        private const string _createRequestApi = "/api/arsys/v1/entry/HPD:IncidentInterface_Create";

        public CreateUserServiceRequestRequest(string baseAddress, string token, string description, string referenceTaskId)
        {
            var authToken = string.Format("AR-JWT {0}", token);
            var requestContent = new StringContent(BuildContent(description, referenceTaskId), Encoding.UTF8, "application/json");

            this.RequestUri = new Uri(baseAddress.Trim('/') + _createRequestApi);
            this.Method = HttpMethod.Post;
            this.Content = requestContent;
            this.Headers.Add("Authorization", authToken);
        }

        private string BuildContent(string description, string referenceTaskId)
        {
            var userServiceRequestDetails = new
                {
                    values = new 
                    {
                        First_Name = "Capgemini",
                        Last_Name = "MyCloud",
                        Description = description,
                        Impact = "3-Moderate/Limited",
                        Urgency = "3-Medium",
                        Status = "Assigned",
                        Reported_Source = "Systems Management" ,
                        Service_Type = "User Service Request",
                        z1D_Action = "CREATE",
                        External_System = "Capgemini MyCloud",
                        External_ID = referenceTaskId
                    }
                };

                
            var userServiceRequestDetailsJson = JsonConvert.SerializeObject(userServiceRequestDetails)
                .Replace("Reported_Source", "Reported Source")
                .Replace("External_System", "External System")
                .Replace("External_ID", "External ID");

            return userServiceRequestDetailsJson;
        }
    }
}