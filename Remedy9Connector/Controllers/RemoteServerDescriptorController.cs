using Microsoft.AspNetCore.Mvc;
using Remedy9Connector.Models;

namespace Remedy9Connector.Controllers
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
            connectorDescriptor.Name = "RemedyITSM9";
            connectorDescriptor.Type = "Capgemini.RemedyITSM9.ServiceRequest.Connector";
            connectorDescriptor.Path = "api";

            connectorDescriptor.ConnectionInstanceProperties.Add(new ConnectionInstancePropertyDescriptor()
            {
                Name = "url",
                DisplayName = "URL",
                HelpText = "Url to the Remedy 9 ITSM API",
                Type = "String",
                Required = true
            });

            connectorDescriptor.Actions.Add(BuildCreateUserServiceRequestActionDescriptor());
            connectorDescriptor.Actions.Add(BuildUserServiceRequestStatusActionDescriptor());
            serverDescriptor.Connectors.Add(connectorDescriptor);

            return new ObjectResult(serverDescriptor);
        }

        private ActionDescriptor BuildCreateUserServiceRequestActionDescriptor()
        {
            var requestVmAction = new ActionDescriptor();
            requestVmAction.Name = "createuserservicerequest";
            requestVmAction.DisplayName = "Create a new user service request";
            requestVmAction.Path = "userservicerequest";

            requestVmAction.Inputs.Add(new InputDescriptor() 
            {
                Name = "Requestor",
                Type = "String",
                Required = true
            });
            requestVmAction.Inputs.Add(new InputDescriptor() 
            {
                Name = "Description",
                Type = "String",
                Required = true
            });
            requestVmAction.Inputs.Add(new InputDescriptor() 
            {
                Name = "ReferenceTaskId",
                Type = "String",
                Required = true
            });

            requestVmAction.Output = new InputDescriptor()
            {
                Name = "TicketId",
                Type = "String",
                Required = true
            };

            return requestVmAction;
        }

        private ActionDescriptor BuildUserServiceRequestStatusActionDescriptor()
        {
            var requestVmAction = new ActionDescriptor();
            requestVmAction.Name = "checkuserservicerequeststatus";
            requestVmAction.DisplayName = "Check user service request status";
            requestVmAction.Path = "userservicerequeststatus";

            requestVmAction.Inputs.Add(new InputDescriptor() 
            {
                Name = "TicketId",
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
    }
}