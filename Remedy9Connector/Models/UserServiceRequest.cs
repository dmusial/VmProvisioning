namespace Remedy9Connector.Models
{
    public class UserServiceRequestInputs
    {
        public string Requestor { get; set; }        
        public string Description { get; set; }
        public string RefrenceTaskId { get; set; }
    }

    public class UserServiceRequest
    {
        public  UserServiceRequestInputs Inputs { get; set; }
    }
}