using FluentValidation;

namespace Application.Mediatr.Commands.Room.DisconnectRoom;

public sealed class DisconnectRoomCommandValidation : AbstractValidator<DisconnectRoomCommand>
{
    public DisconnectRoomCommandValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}