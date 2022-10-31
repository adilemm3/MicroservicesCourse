using FluentValidation;

namespace PlatformService.Application.Commands.CreatePlatform;

public class CreatePlatformCommandValidator : AbstractValidator<CreatePlatformCommand>
{
    public CreatePlatformCommandValidator()
    {
        RuleFor(x => x.Platform)
            .NotEmpty()
            .SetValidator(new PlatformCreateDtoValidator());
    }
}

class PlatformCreateDtoValidator : AbstractValidator<PlatformCreateDto>
{
    public PlatformCreateDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);
        RuleFor(x => x.Publisher)
            .NotEmpty();
        RuleFor(x => x.Cost)
            .NotEmpty();
    }
}