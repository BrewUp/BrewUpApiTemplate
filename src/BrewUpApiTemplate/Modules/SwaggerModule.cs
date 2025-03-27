using Microsoft.OpenApi.Models;

namespace BrewUpApiTemplate.Modules;

public sealed class SwaggerModule : IModule
{
	public bool IsEnabled => false;
	public int Order => 0;
	public IEnumerable<IModule> DependsOn => [];

	public IServiceCollection Register(WebApplicationBuilder builder)
	{
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
		{
			Description = "BrewUp",
			Title = "BrewUp API",
			Version = "v1",
			Contact = new OpenApiContact
			{
				Name = "BrewUp"
			}
		}));

		return builder.Services;
	}

	public WebApplication Configure(WebApplication endpoints) => endpoints;
}