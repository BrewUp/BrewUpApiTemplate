namespace BrewUpApiTemplate.Modules;

public sealed class StatusModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/-/healthz", HandleStatus)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Status");
        
        endpoints.MapGet("/-/ready", HandleStatus)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Status");
        
        endpoints.MapGet("/-/check-up", HandleStatus)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Status");

        return endpoints;
    }

    private static IResult HandleStatus()
    {
        return Results.NoContent();
    }
}
