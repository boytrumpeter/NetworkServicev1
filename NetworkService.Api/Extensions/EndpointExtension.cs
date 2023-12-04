namespace NetworkService.Api.Extensions
{
    using NetworkService.Api.Requests;
    using NetworkService.Api.Services;

    public static class EndpointExtension
    {
        public static void RegisterNetworkServiceEndpoints(this WebApplication app)
        {
            app.MapPost("api/NumberOfCustomers", async (NetworkRequest networkRequest, IService service, ILogger<WebApplication> logger) => {
                try
                {
                    return await service.ProcessRequest(networkRequest);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Bad request : {ex.Message}");
                    return Results.BadRequest("Invalid request");
                }
            })
            .WithName("NetworkService")
            .WithOpenApi();
        }
    }
}
