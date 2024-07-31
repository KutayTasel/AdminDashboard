using FluentValidation;
using GameDashboardProject.Application.Constants;
using GameDashboardProject.Application.Features.Queries.Login;

namespace GameDashboardProject.Application.Validators
{
    public class LoginQueryRequestValidator : AbstractValidator<LoginQueryRequest>
    {
        public LoginQueryRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(Messages.UsernameRequired)
                .MinimumLength(3).WithMessage(Messages.UsernameTooShort)
                .MaximumLength(20).WithMessage(Messages.UsernameTooLong)
                .Matches("^[a-zA-Z0-9]+$").WithMessage(Messages.InvalidUserName);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Messages.PasswordRequired)
                .MinimumLength(6).WithMessage(Messages.PasswordTooShort)
                .Matches(@"[A-Z]").WithMessage(Messages.PasswordTooWeak) // Büyük harf
                .Matches(@"[a-z]").WithMessage(Messages.PasswordTooWeak) // Küçük harf
                .Matches(@"\d").WithMessage(Messages.PasswordTooWeak)   // Rakam
                .Matches(@"[\W_]").WithMessage(Messages.PasswordTooWeak); // Özel karakter
        }
    }
}
