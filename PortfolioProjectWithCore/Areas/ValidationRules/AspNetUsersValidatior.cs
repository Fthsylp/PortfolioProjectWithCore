using FluentValidation;
using PortfolioProject.Areas.Users.Models;

namespace PortfolioProject.Areas.ValidationRules
{
    public class AspNetUsersValidatior : AbstractValidator<UserRegisterViewModel>
    {
        public AspNetUsersValidatior()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x=>x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x=>x.ImageUrl).NotEmpty().WithMessage("ImageUrl is required");
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(5).WithMessage("Username can't be shorter than 5 characters.")
                .MaximumLength(100).WithMessage("Username can't be longer than 100 characters.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(5).WithMessage("Password can't be shorter than 5 characters.")
                .MaximumLength(100).WithMessage("Password can't be longer than 100 characters.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address format");
        }
    }
}

