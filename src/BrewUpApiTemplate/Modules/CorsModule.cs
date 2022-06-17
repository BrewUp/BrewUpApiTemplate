namespace BrewUpApiTemplate.Modules;

public sealed class CorsModule : IModule
{
    public bool IsEnabled { get; }
    public int Order { get; }

    public CorsModule()
    {
        IsEnabled = true;
        Order = 0;
    }

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", corsBuilder =>
                corsBuilder.AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader());
        });

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}