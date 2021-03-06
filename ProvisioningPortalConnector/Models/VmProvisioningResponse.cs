using System;
using System.Collections.Generic;

namespace ProvisioningPortalConnector.Models
{
    public class VmProvisioningResponse
    {
        public VmProvisioningResponse(Guid requestId)
        {
            Outputs = new Dictionary<string, object>();
            Outputs.Add("requestId", requestId);
        }

        public Dictionary<string, object> Outputs { get; private set; }
        
    }
}