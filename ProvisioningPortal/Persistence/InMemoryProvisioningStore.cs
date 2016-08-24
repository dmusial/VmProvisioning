using System.Collections.Generic;

namespace CloudProvisioningPortal.Persistence
{
    public class InMemoryProvisioningStore
    {
        private List<VmProvisioningRequest> _requests;

        public InMemoryProvisioningStore()
        {
            _requests = new List<VmProvisioningRequest>();
        }

        public List<VmProvisioningRequest> GetAll()
        {
            return _requests;
        }

        public void Register(VmProvisioningRequest request)
        {
            _requests.Add(request);
        }
    }
}