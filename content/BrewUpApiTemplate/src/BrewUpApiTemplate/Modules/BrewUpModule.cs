using BrewUpApiTemplate.Models;
using FluentValidation;

namespace BrewUpApiTemplate.Modules;

public sealed class BrewUpModule : IModule
{
  public bool IsEnabled => true;
  public int Order => 0;
  public IEnumerable<IModule> DependsOn => Array.Empty<IModule>();

  public IServiceCollection Register(WebApplicationBuilder builder)
  {
    return builder.Services;
  }

  WebApplication IModule.Configure(WebApplication app)
  {
    app.MapPost("/brewup", SayHelloAsync)
      .Produces(StatusCodes.Status202Accepted)
      .ProducesValidationProblem()
      .WithName("GetHelloParameters")
      .WithTags("BrewUp");

    return app;
  }

  private static async Task<IResult> SayHelloAsync(HelloRequest helloRequest,
      IValidator<HelloRequest> validator)
  {
    var validationResult = await validator.ValidateAsync(helloRequest);
    if (validationResult.IsValid)
      return Results.Ok($"Hello {helloRequest.Name} from BrewUp");

    var errors = validationResult.Errors.GroupBy(e => e.PropertyName)
        .ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());

    return Results.ValidationProblem(errors);
  }
}