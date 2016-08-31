using System.Collections.Generic;

namespace Remedy9Connector.Models
{
    public class UserServiceRequestStatusResponse
    {
        public UserServiceRequestStatusResponse(string status)
        {
            Outputs = new Dictionary<string, object>();
            Outputs.Add("status", status);
        }

        public Dictionary<string, object> Outputs { get; private set; }
        
    }
}