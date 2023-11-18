using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
    public class PortfolioValidatior : AbstractValidator<Portfolio>
    {
        public PortfolioValidatior()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Project name cannot be empty");
            //RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Please select image");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Name can't be shorter than 5 characters.");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Name can't be longer than 100 characters.");
        }
    }
}
