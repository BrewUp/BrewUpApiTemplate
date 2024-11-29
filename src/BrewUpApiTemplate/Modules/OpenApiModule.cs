namespace BrewUpApiTemplate.Modules;

public class OpenApiModule : IModule
{
  public bool IsEnabled => true;
  public int Order => 0;

  public IServiceCollection Register(WebApplicationBuilder builder)
  {
    builder.Services.AddOpenApi();

    return builder.Services;
  }

  public WebApplication Configure(WebApplication app)
  {
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
      options.SwaggerEndpoint("/openapi/v1.json", "BrewUp API");
      options.RoutePrefix = "documentation";
    });

    return app;
  }
}