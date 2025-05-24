using Scalar.AspNetCore;

namespace BrewUpApiTemplate.Modules;

public sealed class ScalarModule : IModule
{
  public bool IsEnabled => true;
  public int Order => 0;
  
  public IEnumerable<IModule> DependsOn => Array.Empty<IModule>();

  public IServiceCollection Register(WebApplicationBuilder builder)
  {
    return builder.Services;
  }

  public WebApplication Configure(WebApplication app)
  {
    if (!app.Environment.IsDevelopment()) return app;

    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
      options.WithTitle("BrewUp API v1.0")
        .WithFavicon("https://github.com/BrewUp/BrewUpApiTemplate/src/Logo-png")
        .WithTheme(ScalarTheme.Mars);
    });

    return app;
  }
}