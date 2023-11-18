using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
    public class SkillValidatior:AbstractValidator<Skill>
    {
        public SkillValidatior()
        {
            RuleFor(x=>x.Title).NotEmpty().WithMessage("The Title field is required.");
            RuleFor(x=>x.Title).MinimumLength(5).WithMessage("Name space can't be shorter than 5 characters.");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Rate space can't be empty. (Pick a rate between 1-100)");
            RuleFor(x => x.Value).LessThanOrEqualTo(100).WithMessage("Pick a rate between 1-100");
        }
    }
}
