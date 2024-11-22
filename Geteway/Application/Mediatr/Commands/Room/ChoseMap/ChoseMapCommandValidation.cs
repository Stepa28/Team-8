using FluentValidation;

namespace Application.Mediatr.Commands.Room.ChoseMap;

public sealed class ChoseMapCommandValidation : AbstractValidator<ChoseMapCommand>
{
    public ChoseMapCommandValidation()
    {
        RuleFor(x => x.RoomId)
            .GreaterThan(0);
        
        RuleFor(x => x.MapId)
            .GreaterThan(0);
    }
}