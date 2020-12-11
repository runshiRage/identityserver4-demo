using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            /**
             * AddAuthentication adds the authentication services to DI.
             * 
             * We are using a cookie to locally sign-in the user (via "Cookies" as the DefaultScheme), 
             * and we set the DefaultChallengeScheme to oidc because when we need the user to login, 
             * we will be using the OpenID Connect protocol.
             *
             * We then use AddCookie to add the handler that can process cookies.
             *
             * Finally, AddOpenIdConnect is used to configure the handler that performs the OpenID Connect protocol. 
             * The Authority indicates where the trusted token service is located. 
             * We then identify this client via the ClientId and the ClientSecret. 
             * SaveTokens is used to persist the tokens from IdentityServer in the cookie (as they will be needed later).
             */
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";


                    options.SaveTokens = true;

                    options.Scope.Add("api1");
                    options.Scope.Add("offline_access");
                });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
