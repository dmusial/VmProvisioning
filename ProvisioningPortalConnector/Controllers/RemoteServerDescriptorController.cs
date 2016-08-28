using Microsoft.AspNetCore.Mvc;
using ProvisioningPortalConnector.Models;

namespace ProvisioningPortalConnector.Controllers
{
    [Route("remoteserver")]
    public class RemoteServerDescriptorController : Controller
    {
        [Route("remoteserverhealth")]
        public string RemoteServerHealth()
        {
            return "OK";
        }

        [Route("remoteserverdescriptor")]
        public IActionResult RemoteServerDescriptor()
        {
            RemoteServerDescriptor serverDescriptor = new RemoteServerDescriptor();
            var connectorDescriptor = new ConnectorDescriptor();
            connectorDescriptor.Name = "ProvisioningService";
            connectorDescriptor.Type = "Capgemini.ProvisioningPortal.Connector";
            connectorDescriptor.Path = "api";

            connectorDescriptor.ConnectionInstanceProperties.Add(new ConnectionInstancePropertyDescriptor()
            {
                Name = "url",
                DisplayName = "URL",
                HelpText = "Url to the provisioning service",
                Type = "String",
                Required = true
            });

            connectorDescriptor.Actions.Add(BuildRequestStatusActionDescriptor());
            connectorDescriptor.Actions.Add(BuildRequestVmActionDescriptor());
            serverDescriptor.Connectors.Add(connectorDescriptor);

            return new ObjectResult(serverDescriptor);
        }

        private ActionDescriptor BuildRequestStatusActionDescriptor()
        {
            var requestVmAction = new ActionDescriptor();
            requestVmAction.Name = "requeststatus";
            requestVmAction.DisplayName = "Check VM Request Status";
            requestVmAction.Path = "requeststatus";
            requestVmAction.Inputs.Add(new InputDescriptor() 
            {
                Name = "RequestId",
                Type = "String",
                Required = true
            });

            requestVmAction.Output = new InputDescriptor()
            {
                Name = "Status",
                Type = "String",
                Required = true
            };

            return requestVmAction;
        }

        private ActionDescriptor BuildRequestVmActionDescriptor()
        {
            var requestVmAction = new ActionDescriptor();
            requestVmAction.Name = "requestvm";
            requestVmAction.DisplayName = "Request New Virtual Machine";
            requestVmAction.Path = "requestvm";
            requestVmAction.Inputs.Add(new InputDescriptor() 
            {
                Name = "VmSize",
                Type = "String",
                Required = true
            });
            
            requestVmAction.Inputs.Add(new InputDescriptor() 
            {
                Name = "Requestor",
                Type = "String",
                Required = true
            });

            requestVmAction.Output = new InputDescriptor()
            {
                Name = "RequestId",
                Type = "String",
                Required = true
            };

            return requestVmAction;
        }
    }
}