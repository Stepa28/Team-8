using FluentValidation;

namespace Application.Mediatr.Commands.ToggleReadiness;

public sealed class ToggleReadinessCommandValidation : AbstractValidator<ToggleReadinessCommand>
{
    public ToggleReadinessCommandValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}