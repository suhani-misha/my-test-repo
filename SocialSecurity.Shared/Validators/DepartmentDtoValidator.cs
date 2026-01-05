using FluentValidation;
using SocialSecurity.Shared.Dtos.Department;
using SocialSecurity.Domain.Models.Common;
using System;

namespace SocialSecurity.Shared.Validators
{
    public class DepartmentDetailsDtoValidator : AbstractValidator<DepartmentDetailsDto>
    {
        public DepartmentDetailsDtoValidator()
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(150);

            RuleFor(x => x.DepartmentHead)
                .NotEmpty().WithMessage("Department head is required.")
                .MaximumLength(100);

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(150);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(50);

            RuleFor(x => x.RiskRating)
                .NotEmpty().WithMessage("Risk rating is required.")
                .Must(value => Enum.TryParse<RiskRatingStatus>(value, true, out _))
                .WithMessage($"Invalid risk rating. Allowed values: {string.Join(", ", Enum.GetNames(typeof(RiskRatingStatus)))}");
        }
    }
}
