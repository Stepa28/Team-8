using FluentValidation;

namespace Application.Mediatr.Commands.Battle.SendBattleState;

public sealed class SendBattleStateCommandValidation : AbstractValidator<SendBattleStateCommand>
{
    public SendBattleStateCommandValidation()
    {
        RuleFor(x => x.Dto.UnitList)
            .NotEmpty();
        RuleFor(x => x.Dto.RoundCurrent)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.Dto.Tiles.TilesType)
            .NotEmpty();
        RuleFor(x => x.Dto.Tiles.CountColumn)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.Dto.Tiles.CountRow)
            .NotEmpty()
            .GreaterThan(0);
    }
}