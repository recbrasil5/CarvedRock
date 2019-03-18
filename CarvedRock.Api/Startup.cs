using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.Data;
using CarvedRock.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarvedRock.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connection = @"Server=localhost\sqlexpress;Database=CarvedRock;Trusted_Connection=True;ConnectRetryCount=0";
            //var connection = _config["ConnectionStrings:CarvedRock"];
            services.AddDbContext<CarvedRockDbContext>
               (options => options.UseSqlServer(connection));

            services.AddScoped<ProductRepository>();
        }

        public void Configure(IApplicationBuilder app, CarvedRockDbContext dbContext)
        {
            app.UseMvc();
            dbContext.Seed();
            //app.UseGraphQL<CarvedRockSchema>();
            //app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}
