namespace NetworkService.Api.Extensions
{
    using NetworkService.Api.Requests;
    using NetworkService.Api.Services;

    public static class EndpointExtension
    {
        public static void RegisterNetworkServiceEndpoints(this WebApplication app)
        {
            app.MapPost("api/NumberOfCustomers", async (NetworkRequest networkRequest, IService service) => await service.ProcessRequest(networkRequest))
            .WithName("NetworkService")
            .WithOpenApi();
        }
    }
}
