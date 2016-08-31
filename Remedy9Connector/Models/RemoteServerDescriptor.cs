using System.Collections.Generic;

namespace Remedy9Connector.Models
{
    public class RemoteServerDescriptor
    {
        public RemoteServerDescriptor()
        {
            Connectors = new List<ConnectorDescriptor>();
        }
        
        public List<ConnectorDescriptor> Connectors { get; set; }
    }
}