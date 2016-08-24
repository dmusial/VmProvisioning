namespace ProvisioningPortalConnector.Models
{
    public class VmProvisioningRequestInputs
    {
        public string VmSize { get; set; }
        public string Requestor { get; set; }
    }

    public class VmProvisioningRequest
    {
        public  VmProvisioningRequestInputs Inputs { get; set; }
    }
}