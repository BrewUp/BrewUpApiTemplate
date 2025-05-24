namespace BrewUpApiTemplate.Modules;

public static class ModuleExtensions
{
  private static readonly IList<IModule> RegisteredModules = new List<IModule>();

  public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
  {
    var modules = DiscoverModules().ToList();

    foreach (var module in modules
               .Where(m => m.IsEnabled)
               .OrderBy(m => m.Order))
    {
      foreach (var dependency in module.DependsOn)
      {
        var dependentModule = modules.FirstOrDefault(m => m.GetType() == dependency.GetType());
        if (dependentModule is not { IsEnabled: true })
        {
          throw new InvalidOperationException(
            $"Il modulo {module.GetType().Name} dipende dal modulo {dependency.GetType().Name}, ma quest'ultimo non è abilitato.");
        }
      }

      module.Register(builder);
      RegisteredModules.Add(module);
    }

    return builder;
  }

  public static WebApplication ConfigureModules(this WebApplication app)
  {
    foreach (var module in RegisteredModules)
    {
      module.Configure(app);
    }

    return app;
  }

  private static IEnumerable<IModule> DiscoverModules()
  {
    return typeof(IModule).Assembly
      .GetTypes()
      .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
      .Select(Activator.CreateInstance)
      .Cast<IModule>();
  }
}