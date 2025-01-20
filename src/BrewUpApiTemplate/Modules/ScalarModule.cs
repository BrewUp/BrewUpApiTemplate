using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace BrewUpApiTemplate.Modules;

public sealed class ScalarModule : IModule
{
  public bool IsEnabled => true;
  public int Order => 0;
  public IEnumerable<IModule> DependsOn => Array.Empty<IModule>();

  public IServiceCollection Register(WebApplicationBuilder builder)
  {
    builder.Services.AddOpenApi(options =>
    {
      options.AddDocumentTransformer((document, _, _) =>
          {
            document.Servers = [new OpenApiServer { Url = "/" }];
            document.Info = new OpenApiInfo
            {
              Title = "BrewUp API",
              Version = "v1.0",
              Description = "Project template per Minimal API",
              Contact = new OpenApiContact
              {
                Name = "BrewUp",
                Url = new Uri("https://github.com/BrewUp/BrewUpApiTemplate")
              }
            };

            // Configurazione schema Bearer direttamente
            document.Components = new OpenApiComponents
            {
              SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
              {
                ["Bearer"] = new()
                {
                  Type = SecuritySchemeType.Http,
                  Scheme = "bearer", // "bearer" è il nome dell'header
                  In = ParameterLocation.Header,
                  BearerFormat = "JWT"
                }
              }
            };

            document.SecurityRequirements.Add(new OpenApiSecurityRequirement
            {
              {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>() // Scopes (vuoto se non ci sono scope specifici)
              }
            });

            return Task.CompletedTask;
          });
    });

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