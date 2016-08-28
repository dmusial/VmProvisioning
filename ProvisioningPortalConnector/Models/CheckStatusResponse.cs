using System.Collections.Generic;

namespace ProvisioningPortalConnector.Models
{
    public class CheckStatusResponse
    {
        public CheckStatusResponse(string status)
        {
            Outputs = new Dictionary<string, object>();
            Outputs.Add("status", status);
        }

        public Dictionary<string, object> Outputs { get; private set; }
    }
}