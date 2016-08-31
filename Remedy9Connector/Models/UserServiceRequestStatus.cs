namespace Remedy9Connector.Models
{
    public class UserServiceRequestStatusInputs
    {
        public string TicketId { get; set; }        
    }

    public class UserServiceRequestStatus
    {
        public  UserServiceRequestStatusInputs Inputs { get; set; }
    }
}