using System.Collections.Generic;

namespace ProvisioningPortalConnector.Models
{
    public class ActionDescriptor
    {
        public ActionDescriptor()
        {
            Inputs = new List<InputDescriptor>();
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Path { get; set; }
        public List<InputDescriptor> Inputs { get; set; }
        public InputDescriptor Output { get; set; }
    }
}