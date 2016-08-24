using System;
using System.Collections.Generic;

namespace ProvisioningPortalConnector.Models
{
    public class VmProvisioningResponse
    {
        public VmProvisioningResponse(Guid requestId, string status)
        {
            Outputs = new Dictionary<string, object>();
            Outputs.Add("requestId", requestId);
            Outputs.Add("status", status);
        }

        public Dictionary<string, object> Outputs { get; private set; }
        
    }
}