using FluentValidation;

namespace Application.Mediator.Commands.ConnectBattle;

public sealed class ConnectBattleCommandValidation : AbstractValidator<ConnectBattleCommand>
{
    public ConnectBattleCommandValidation()
    {
        RuleFor(x => x.Model.Id)
            .NotEmpty();
    }
}