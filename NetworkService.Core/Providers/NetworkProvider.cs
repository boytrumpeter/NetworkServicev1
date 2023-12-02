namespace NetworkService.Core.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NetworkProvider : INetworkProvider
    {
        public List<Network> Networks { get; set; } = new List<Network>();

        public async Task<Network> CreateNetwork()
        {
            var network = new Network();
            Networks.Add(network);
            return await Task.FromResult<Network>(network);
        }
    }
}
