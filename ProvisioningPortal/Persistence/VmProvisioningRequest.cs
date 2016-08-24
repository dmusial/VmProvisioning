using System;

namespace CloudProvisioningPortal.Persistence
{
    public class VmProvisioningRequest
    {
        public VmProvisioningRequest(string requestor, string vmSize)
        {
            RequestId = Guid.NewGuid();
            Requestor = requestor;
            VmSize = vmSize;
            Status = "Registered (Awaiting Completion)";
            CreationDate = DateTime.Now;
        }

        public Guid RequestId { get; private set; }
        public string Requestor { get; private set; }
        public string VmSize { get; private set; }
        public string Status { get; private set; }
        public DateTime CreationDate { get; set; }
    }
}