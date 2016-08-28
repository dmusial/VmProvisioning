using System;

namespace ProvisioningPortalConnector.Models
{
    public class CheckStatusRequestInputs
    {
        public Guid RequestId { get; set; }
    }

    public class CheckStatusRequest
    {
        public  CheckStatusRequestInputs Inputs { get; set; }
    }
}