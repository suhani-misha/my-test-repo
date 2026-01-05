using FluentValidation;
using SocialSecurity.Shared.Dtos.Holiday;

namespace SocialSecurity.Shared.Validators
{
    public class LeaveRequestDtoValidator : AbstractValidator<LeaveRequestDto>
    {
        public LeaveRequestDtoValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .GreaterThan(DateTime.MinValue).WithMessage("Invalid start date.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to Start date.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User id is required.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Leave reason is required.");

            RuleFor(x => x.Duaration)
                .GreaterThan(0).WithMessage("Duration must be greater than zero.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Invalid leave status.");
        }
    }
}
