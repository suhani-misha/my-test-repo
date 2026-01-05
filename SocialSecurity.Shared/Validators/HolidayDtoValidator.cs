using FluentValidation;
using SocialSecurity.Shared.Dtos.Holiday;

namespace SocialSecurity.Shared.Validators
{
    public class HolidayDtoValidator : AbstractValidator<HolidayDto>
    {
        public HolidayDtoValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .GreaterThan(DateTime.MinValue).WithMessage("Invalid date.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Holiday name is required.")
                .MaximumLength(100).WithMessage("Holiday name cannot exceed 100 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country name cannot exceed 50 characters.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Holiday type is required.");
        }
    }
}
