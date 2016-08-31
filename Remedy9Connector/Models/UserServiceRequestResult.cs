using System.Collections.Generic;

namespace Remedy9Connector.Models
{
    public class UserServiceRequestResult
    {
        public UserServiceRequestResult(string ticketId)
        {
            Outputs = new Dictionary<string, object>();
            Outputs.Add("ticketId", ticketId);
        }

        public Dictionary<string, object> Outputs { get; private set; }
        
    }
}