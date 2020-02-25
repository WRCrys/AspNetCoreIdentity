using AspNetCoreIdentity.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentity.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));
            });

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("AspNetCoreIdentityContextConnection")));


            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

            return services;
        }
    }
}
