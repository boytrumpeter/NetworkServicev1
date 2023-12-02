﻿namespace NetworkService.Api.Services
{
    
    using NetworkService.Api.Reponses;
    using NetworkService.Api.Requests;
    using NetworkService.Core;
    using NetworkService.Core.Providers;
    using System.Threading.Tasks;
    
    public class Service : IService
    {
        private readonly INetworkProvider _networkProvider;

        public Service(INetworkProvider networkProvider)
        {
            _networkProvider = networkProvider;
        }

        public async Task<NetworkResponse> ProcessRequest(NetworkRequest request)
        {
            if (request.Network == null)
            {
                new NetworkResponse(request.SelectedNode, 0);
            }

            Network network = null;

            foreach (var branch in request.Network.Branches)
            {
                if (_networkProvider.Networks.Count == 0 || network?.FindNode(branch.StartNode) == null)
                {
                    network = await _networkProvider.CreateNetwork();
                }

                network.AddBranch(branch.StartNode, branch.EndNode);
            }

            var nodes = await GetDownStreamNodes(request.SelectedNode);

            var numberOfCustomers = request.Network.Customers.Sum(x => nodes.Contains(x.Node) ? x.NumberOfCustomers : 0);

            return new NetworkResponse(request.SelectedNode, numberOfCustomers);
        }

        private async Task<List<int>> GetDownStreamNodes(int selectedNode)
        {
            List<int> nodes = new List<int>();
            foreach (var network in _networkProvider.Networks)
            {
                var node = network.FindNode(selectedNode);
                if (node != null)
                {
                    return await Task.FromResult(network.TraverseDownStream(selectedNode));
                }
            }

            return nodes;
        }
    }
}
