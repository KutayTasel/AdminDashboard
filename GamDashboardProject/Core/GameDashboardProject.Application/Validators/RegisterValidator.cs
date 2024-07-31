using FluentValidation;
using GameDashboardProject.Application.Constants;
using GameDashboardProject.Application.Features.Commands;

namespace GameDashboardProject.Application.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(Messages.UsernameRequired)
                .Length(3, 20).WithMessage(Messages.UsernameTooShort)
                .Matches("^[a-zA-Z0-9]*$").WithMessage(Messages.InvalidUserName);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Messages.EmailRequired)
                .EmailAddress().WithMessage(Messages.InvalidEmailFormat);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Messages.PasswordRequired)
                .MinimumLength(6).WithMessage(Messages.PasswordTooWeak)
                .Matches("[A-Z]").WithMessage(Messages.InvalidPasswordFormat)
                .Matches("[a-z]").WithMessage(Messages.InvalidPasswordFormat)
                .Matches("[0-9]").WithMessage(Messages.InvalidPasswordFormat)
                .Matches("[^a-zA-Z0-9]").WithMessage(Messages.InvalidPasswordFormat);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(Messages.ConfirmPasswordRequired)
                .Equal(x => x.Password).WithMessage(Messages.PasswordsDoNotMatch);
        }
    }
}







