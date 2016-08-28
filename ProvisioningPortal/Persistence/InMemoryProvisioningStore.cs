using System.Linq;
using System.Collections.Generic;
using System;

namespace CloudProvisioningPortal.Persistence
{
    public class InMemoryProvisioningStore
    {
        private List<VmProvisioningRequest> _requests;

        public InMemoryProvisioningStore()
        {
            _requests = new List<VmProvisioningRequest>();
        }

        public VmProvisioningRequest Get(Guid requestId)
        {
            return _requests.FirstOrDefault(r => r.RequestId == requestId);
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