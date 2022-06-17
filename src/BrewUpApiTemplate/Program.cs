using BrewUpApiTemplate.Modules;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
builder.RegisterModules();

builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.MapEndpoints();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(s =>
    {
        s.RouteTemplate = "documentation/{documentName}/documentation.json";
        s.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/documentation/v1/documentation.json", "BrewUp Minimal Api Template");
        s.RoutePrefix = "documentation";
    });
}

app.Run();