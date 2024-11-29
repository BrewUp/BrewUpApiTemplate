namespace BrewUpApiTemplate.Modules;

public sealed class CorsModule : IModule
{
  public bool IsEnabled => true;
  public int Order => 0;

  public IServiceCollection Register(WebApplicationBuilder builder)
  {
    builder.Services.AddCors(options =>
    {
      options.AddPolicy("CorsPolicy", corsBuilder =>
              corsBuilder.AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(_ => true)
                  .AllowCredentials());
    });

    return builder.Services;
  }

  WebApplication IModule.Configure(WebApplication app)
  {
    app.UseCors("CorsPolicy");

    return app;
  }
}