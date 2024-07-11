using FluentValidation;

namespace Application.Mediatr.Commands.RegisterUser;

public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
        
        RuleFor(x => x.Nick)
            .NotEmpty();
    }
}