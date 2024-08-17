using FluentValidation;

namespace Application.Mediatr.Commands.CreateRoom;

public class CreateRoomCommandValidation : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
    }
}