namespace NetworkService.Core.Providers
{
    using System.Net;

    public interface INetworkProvider
    {
        List<Network> Networks { get; set; }
        Task<Network> CreateNetwork();
    }
}
