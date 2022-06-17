﻿namespace BrewUpApiTemplate.Modules
{
    public interface IModule
    {
        bool IsEnabled { get; }
        int Order { get; }

        IServiceCollection RegisterModule(WebApplicationBuilder builder);
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}
