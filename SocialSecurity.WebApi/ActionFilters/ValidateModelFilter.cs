using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialSecurity.Domain.Models.Common;
using SocialSecurity.Shared.Dtos.Common;

namespace SocialSecurity.API.ActionFilters
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Collect FluentValidation errors
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = context.HttpContext.RequestServices.GetService(validatorType);

                if (validator is IValidator valid)
                {
                    ValidationResult result = valid.Validate(new ValidationContext<object>(argument));
                    if (!result.IsValid)
                    {
                        var errors = result.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToArray()
                            );

                        context.Result = new JsonResult(new Response
                        {
                            Status = ResponseSatusEnums.Error.ToString(),
                            Message = "Validation failed",
                            Data = errors,
                            Code = 400
                        })
                        { StatusCode = 400 };

                        return;
                    }
                }
            }

            // Collect DataAnnotations errors
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        ms => ms.Key,
                        ms => ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                context.Result = new JsonResult(new Response
                {
                    Status = ResponseSatusEnums.Error.ToString(),
                    Message = "Validation failed",
                    Data = errors,
                    Code = 400
                })
                { StatusCode = 400 };
            }
        }
    }
}
