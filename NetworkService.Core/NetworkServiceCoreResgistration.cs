namespace NetworkService.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using NetworkService.Core.Providers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class NetworkServiceCoreResgistration
    {
        public static IServiceCollection AddNetworkServiceApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<INetworkProvider, NetworkProvider>();
            return services;
        }
    }
}
