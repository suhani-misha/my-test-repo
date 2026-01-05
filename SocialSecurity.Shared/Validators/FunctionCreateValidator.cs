using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurity.Shared.Validators
{
    using FluentValidation;
    using SocialSecurity.Domain.Models.Common;
    using SocialSecurity.Shared.Dtos.Function;

    public class FunctionCreateValidator : AbstractValidator<FunctionCreateDto>
    {
        public FunctionCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Function name is required.")
                .MaximumLength(200);

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("Valid DepartmentId is required.");

            RuleFor(x => x.RiskRating)
                .NotEmpty().Must(x => Enum.TryParse<RiskRatingStatus>(x, true, out _))
                .WithMessage("Invalid RiskRating value.");

            RuleFor(x => x.Likelihood)
                .NotEmpty().Must(x => Enum.TryParse<LikelihoodStatus>(x, true, out _))
                .WithMessage("Invalid Likelihood value.");

            RuleFor(x => x.Impact)
                .NotEmpty().Must(x => Enum.TryParse<ImpactStatus>(x, true, out _))
                .WithMessage("Invalid Impact value.");

            RuleFor(x => x.Controls)
                .NotEmpty().Must(x => Enum.TryParse<ControlEffectivenessStatus>(x, true, out _))
                .WithMessage("Invalid Controls value.");

            RuleFor(x => x.Responsible)
                .NotEmpty().MaximumLength(200);
        }
    }

}
