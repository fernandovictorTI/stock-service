using System.Text.Json;
using JacksonVeroneze.StockService.Api.Middlewares.ErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JacksonVeroneze.StockService.Api.Configuration
{
    public static class ApiConfig
    {
        private const string CorsPolicyName = "CorsPolicy";

        public static IServiceCollection AddApiConfiguration(this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment hostEnvironment)
        {
            services.AddRouting(options => options.LowercaseUrls = true)
                .AddCorsConfiguration(configuration, CorsPolicyName)
                .HealthChecksConfiguration()
                .AddAutoMapperConfiguration()
                .AddAutoMapperConfigurationValid()
                .AddDatabaseConfiguration(configuration)
                .AddAutoMediatRConfiguration()
                .AddDependencyInjectionConfiguration()
                .AddSwaggerConfiguration()
                .AddApplicationInsightsConfiguration(configuration)
                .AddAuthenticationConfiguration(configuration)
                .AddControllers();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
        {
            app.UseCultureSetup()
                .UseCors(CorsPolicyName)
                .UseHealthChecksSetup()
                .UseSerilogRequestLogging()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseMiddleware<ErrorHandlingMiddleware>()
                .UseSwaggerSetup()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }
}
