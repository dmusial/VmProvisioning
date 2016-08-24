using System;

namespace CloudProvisioningPortal.Models
{
    public class VmProvisioningResponse
    {
        public Guid RequestId { get; set; }
        public string Status { get; set; }
    }
}