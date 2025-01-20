namespace BrewUpApiTemplate.Modules;

public interface IModule
{
  bool IsEnabled { get; }
  int Order { get; }
  IEnumerable<IModule> DependsOn { get; }

  IServiceCollection Register(WebApplicationBuilder builder);
  WebApplication Configure(WebApplication app);
}