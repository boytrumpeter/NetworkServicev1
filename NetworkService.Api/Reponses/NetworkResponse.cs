namespace NetworkService.Api.Reponses
{
    public class NetworkResponse
    {
        public NetworkResponse(int selectedNode, int numberOfCustomers)
        {
            SelectedNode = selectedNode;
            NumberOfCustomers = numberOfCustomers;
        }
        public int SelectedNode { get; private set; }
        public int NumberOfCustomers { get; private set; }
    }
}
