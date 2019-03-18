using CarvedRock.Api.Data;
using CarvedRock.Api.GraphQL;
using CarvedRock.Api.Repositories;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarvedRock.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connection = @"Server=localhost\sqlexpress;Database=CarvedRock;Trusted_Connection=True;ConnectRetryCount=0";
            //var connection = _config["ConnectionStrings:CarvedRock"];
            services.AddDbContext<CarvedRockDbContext>
               (options => options.UseSqlServer(connection));

            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductReviewRepository>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CarvedRockSchema>();

            services.AddGraphQL(o => { o.ExposeExceptions = _env.IsDevelopment(); ; })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(context => context.User)
                .AddDataLoader(); //adds caching layer essentially for refreshes
        }

        public void Configure(IApplicationBuilder app, CarvedRockDbContext dbContext)
        {
            //app.UseMvc(); //only if you need to do both REST and GraphQL

            app.UseGraphQL<CarvedRockSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            dbContext.Seed();
            //app.UseGraphQL<CarvedRockSchema>();
            //app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}
