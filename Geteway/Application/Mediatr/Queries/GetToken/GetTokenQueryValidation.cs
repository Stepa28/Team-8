using FluentValidation;

namespace Application.Mediatr.Queries.GetToken;

public class GetTokenQueryValidation : AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidation()
    {
        RuleFor(x => x.Email)
          .NotEmpty()
          .EmailAddress();

        RuleFor(x => x.Password)
          .NotEmpty();
    }
}