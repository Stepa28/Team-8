using FluentValidation;

namespace Application.Mediatr.Commands.ConnectRoom;

public sealed class ConnectRoomCommandValidation : AbstractValidator<ConnectRoomCommand>
{
    public ConnectRoomCommandValidation()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}