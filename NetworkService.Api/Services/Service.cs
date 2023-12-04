namespace NetworkService.Api.Services
{
    using Microsoft.AspNetCore.Mvc;
    using NetworkService.Api.Reponses;
    using NetworkService.Api.Requests;
    using NetworkService.Core;
    using NetworkService.Core.Providers;
    using System;
    using System.Threading.Tasks;
    
    public class Service : IService
    {
        private readonly INetworkProvider _networkProvider;

        public Service(INetworkProvider networkProvider)
        {
            _networkProvider = networkProvider;
        }

        public async Task<IResult> ProcessRequest(NetworkRequest request)
        {
            
            if (!Validate(request))
            {
                return Results.BadRequest("Bad request");
            }

            Network network = new Network();

            foreach (var branch in request.Network.Branches)
            {
                if (network.FindNode(branch.StartNode) == null)
                {
                    network = await _networkProvider.CreateNetwork();
                }

                network.AddBranch(branch.StartNode, branch.EndNode);
            }

            var nodes = await GetDownStreamNodes(request.SelectedNode);

            var numberOfCustomers = request.Network.Customers.Sum(x => nodes.Contains(x.Node) ? x.NumberOfCustomers : 0);

            return Results.Ok(new NetworkResponse(request.SelectedNode, numberOfCustomers));
        }

        #region Private Methods
        private bool Validate(NetworkRequest request)
        {
            if (request == null)
            {
                return false;
            }

            if(request.Network == null)
            {
                return false;
            }

            return true;
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
        #endregion
    }
}
