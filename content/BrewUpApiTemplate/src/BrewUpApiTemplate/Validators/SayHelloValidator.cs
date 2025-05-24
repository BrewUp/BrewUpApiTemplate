using BrewUpApiTemplate.Models;
using FluentValidation;

namespace BrewUpApiTemplate.Validators;

public class SayHelloValidator : AbstractValidator<HelloRequest>
{
    public SayHelloValidator()
    {
        RuleFor(h => h.Name).NotEmpty().MaximumLength(50);
    }
}