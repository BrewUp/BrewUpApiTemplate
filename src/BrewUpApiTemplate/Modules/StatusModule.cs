using BrewUpApiTemplate.Helpers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BrewUpApiTemplate.Modules;

public sealed class StatusModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddHealthChecks();

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapHealthChecks("/health", new HealthCheckOptions
		{
			ResponseWriter = HealthCheckExtensions.WriteResponse
		});

		return endpoints;
	}
}
