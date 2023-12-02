namespace NetworkService.Api.Models
{
    public class Network
    {
        public List<Branch> Branches { get; set; } = new List<Branch>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
