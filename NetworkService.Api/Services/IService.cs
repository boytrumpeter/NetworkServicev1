namespace NetworkService.Api.Services
{
    using NetworkService.Api.Reponses;
    using NetworkService.Api.Requests;

    public interface IService
    {
        Task<IResult> ProcessRequest(NetworkRequest request);
    }
}
