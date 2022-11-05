namespace BrewUpApiTemplate.Modules;

public sealed class StatusModule : IModule
{
    public bool IsEnabled { get; } = true;
    public int Order { get; }

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/-/healthz", status)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("BrewUp");
        
        endpoints.MapGet("/-/ready", status)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("BrewUp");
        
        endpoints.MapGet("/-/check-up", status)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("BrewUp");

        return endpoints;
    }

    private static IResult status()
    {
        return Results.NoContent();
    }
}
