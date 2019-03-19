using System;
using CarvedRock.Web.Clients;
using GraphQL.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarvedRock.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private string CarvedRockApiUri => _config["CarvedRockApiUri"];

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(t => new GraphQLClient(CarvedRockApiUri));
            services.AddSingleton<ProductGraphClient>();
            services.AddHttpClient<ProductHttpClient>(o => o.BaseAddress = new Uri(CarvedRockApiUri));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
