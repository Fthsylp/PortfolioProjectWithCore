using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
    public class ExperienceValidatior : AbstractValidator<Experience>
    {
        public ExperienceValidatior()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name cannot be empty")
                .MinimumLength(5).WithMessage("Name can't be shorter than 5 characters.")
                .MaximumLength(100).WithMessage("Name can't be longer than 100 characters.");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("JobTitle can't be empty.")
                .MinimumLength(5).WithMessage("JobTitle can't be shorter than 5 characters.")
                .MaximumLength(50).WithMessage("JobTitle can't be longer than 100 characters.");


            RuleFor(x => x.Date).NotEmpty().WithMessage("Date can't be empty.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description can't be empty.")
                .MinimumLength(50).WithMessage("Description can't be shorter than 50 characters.")
                .MaximumLength(1000).WithMessage("Description can't be longer than 1000 characters.");

            //RuleFor(x => x.ImageUrl)
                //.NotEmpty().WithMessage("Image can't be empty.");
        }
    }
}
