using FluentValidation;

namespace PlatformService.Application.Queries.GetPlatformById;

public class GetPlatformByIdQueryValidator : AbstractValidator<GetPlatformByIdQuery>
{
    public GetPlatformByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}