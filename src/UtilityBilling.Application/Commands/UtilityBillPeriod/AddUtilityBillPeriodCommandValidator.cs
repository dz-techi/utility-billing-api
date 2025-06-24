using FluentValidation;

namespace UtilityBilling.Application.Commands.UtilityBillPeriod;

public class AddUtilityBillPeriodCommandValidator : AbstractValidator<AddUtilityBillPeriodCommand>
{
    public AddUtilityBillPeriodCommandValidator()
    {
        RuleFor(u => u.StartDate)
            .LessThan(u => u.EndDate)
            .WithMessage("Start date must be less than end date");
    }
}