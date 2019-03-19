using CarvedRock.Api.Data;
using CarvedRock.Api.GraphQL;
using CarvedRock.Api.GraphQL.Messaging;
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
            var connection = _config.GetConnectionString("CarvedRock");
            services.AddDbContext<CarvedRockDbContext>
                (options => options.UseSqlServer(connection));

            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductReviewRepository>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CarvedRockSchema>();
            services.AddSingleton<ReviewMessageService>();

            services.AddGraphQL(o => { o.ExposeExceptions = _env.IsDevelopment(); })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(context => context.User)
                .AddDataLoader() //adds caching layer essentially for refreshes
                .AddWebSockets(); //adds graphQL websockets

            services.AddCors(); //don't do in production probably
        }

        public void Configure(IApplicationBuilder app, CarvedRockDbContext dbContext)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseWebSockets(); //add for API
            app.UseGraphQLWebSockets<CarvedRockSchema>("/graphql");
            app.UseGraphQL<CarvedRockSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            dbContext.Seed();
        }
    }
}
