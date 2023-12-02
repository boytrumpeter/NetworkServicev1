namespace NetworkService.Api.Requests
{
    using NetworkService.Api.Models;

    public class NetworkRequest
    {
        public Network? Network { get; set; }
        public int SelectedNode { get; set; }
    }
}
