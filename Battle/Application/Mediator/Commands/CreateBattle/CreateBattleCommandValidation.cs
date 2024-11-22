using FluentValidation;

namespace Application.Mediator.Commands.CreateBattle;

public sealed class CreateBattleCommandValidation : AbstractValidator<CreateBattleCommand>
{
    public CreateBattleCommandValidation()
    {
        RuleFor(x => x.Dto.Id)
            .NotEmpty();
        RuleFor(x => x.Dto.Tiles.TilesType)
            .NotEmpty();
    }
}