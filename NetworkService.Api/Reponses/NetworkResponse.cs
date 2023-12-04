namespace NetworkService.Api.Reponses
{
    public class NetworkResponse
    {
        public NetworkResponse(int selectedNode, int numberOfCustomers)
        {
            SelectedNode = selectedNode;
            TotalNumberOfCustomers = numberOfCustomers;
        }
        public int SelectedNode { get; private set; }
        public int TotalNumberOfCustomers { get; private set; }
    }
}
