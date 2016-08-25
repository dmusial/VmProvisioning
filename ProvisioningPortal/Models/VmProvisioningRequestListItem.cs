using System;

namespace CloudProvisioningPortal.Models
{
    public class VmProvisioningRequestListItem
    {
        public Guid RequestId { get; set; }
        public string Requestor { get; set; }
        public string VmSize { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}