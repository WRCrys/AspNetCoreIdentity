using AspNetCoreIdentity.Extensions;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentity.Config
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogger>((context) => Logger.Factory.Get());

            services.AddScoped<AuditoriaFilter>();

            return services;
        }
    }
}
