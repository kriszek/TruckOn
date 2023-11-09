using FluentValidation;
using TruckOn.Trucks.Controllers.Contracts;
using TruckOn.Trucks.Models;

namespace TruckOn.Trucks.Controllers.Validation;

// Is applied to controllers automatically by SharpGrip FluentValidation AutoValidation package
public class TruckDTOValidator : AbstractValidator<TruckDTO>
{
    public TruckDTOValidator()
    {
        RuleFor(truck => truck.TruckCode)
            .NotEmpty()
            .MaximumLength(ModelRestrictions.TruckCodeMaxLength)
            .Matches("^[a-zA-Z0-9]*$").WithMessage("'{PropertyName}' must consist only from alphanumeric characters");

        RuleFor(truck => truck.TruckName)
            .NotEmpty()
            .MaximumLength(ModelRestrictions.TruckNameMaxLength);

        RuleFor(truck => truck.TruckStatus)
            .NotEmpty()
            .IsEnumName(typeof(TruckStatus), caseSensitive: false);

        RuleFor(truck => truck.TruckDescription)
            .MaximumLength(ModelRestrictions.TruckDescriptionMaxLength);
    }
}
