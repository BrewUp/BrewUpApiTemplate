# BrewUpApiTemplate

A clean template for MinimalApi DDD Oriented

You can find more details about this template [here](https://www.intre.it/2022/03/15/net-6-incontra-ddd/)
You can find examples about solution developed with this template [here](https://github.com/BrewUp/BeerDrivenDesign)

You can find instructions to configure custom templates in VisualStudio [here](https://docs.microsoft.com/en-us/visualstudio/ide/customizing-project-and-item-templates?view=vs-2022)

## What's inside this repository?

This repository holds the basic configuration to start a new microservice using .Net 6.

The microservice is already prepared to serve APIs through a web server and expose its OpenAPI Specification generated from the modules you define. It also provides an out-of-the-box implementation status routes for an easier Kubernetes probes integration.

## How do I use it?

The repository provides a Visual Studio solution to import your code directly in your preferred IDE and start developing; you can also use the `dotnet` command line to run tests, builds, etc.

To create your build simply run

```sh
cd src/BrewUpApiTemplate
dotnet restore
dotnet build BrewUpApiTemplate.csproj
```

To add new module you can create a new class that implements IModule interface. The aim to use Module is to create a solution organized by BoundedContext, so DomainOriented, instead organized by Controller.
You can add more projects to your solution, at least one for every Module (BoundedContext) you are going to develop.

### How to implement new modules?

See BrewUp module to create new module, it creates a simple module with one `/brewup` API that you can invoke to obtain a nice _Hello World_ message. Try providing a `Name` in your request to see how it affects the API response!
The aim of BrewUp Module is to help you to introduce Validation, Isolation (you can move endpoint's handle of every module in a custom project).
You have to create your module, set IsEnabled to true, and dotnet load it for you automatically. You need to hide/disable a module? Joust put IsEnabled = false.
