using System;
using System.Net.Http;

namespace Remedy9Connector.Requests
{
    public class GetIncidentNumberRequest : HttpRequestMessage
    {
        private const string _requestApi = "/api/arsys/v1/entry/HPD:IncidentInterface_Create/";

        public GetIncidentNumberRequest(string baseAddress, string token, string ticketId)
        {
            this.RequestUri = new Uri(baseAddress.TrimEnd('/') + _requestApi + ticketId);
            this.Method = HttpMethod.Get;

            var authToken = string.Format("AR-JWT {0}", token);
            this.Headers.Add("Authorization", authToken);
        }
    }
}