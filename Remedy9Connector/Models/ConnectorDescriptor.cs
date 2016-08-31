using System.Collections.Generic;

namespace Remedy9Connector.Models
{
    public class ConnectorDescriptor
    {
        public ConnectorDescriptor()
        {
            ConnectionInstanceProperties = new List<ConnectionInstancePropertyDescriptor>();
            Actions = new List<ActionDescriptor>();
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public List<ConnectionInstancePropertyDescriptor> ConnectionInstanceProperties  { get; set; }
        public List<ActionDescriptor> Actions { get; set; }
    }
}