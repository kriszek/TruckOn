using FluentValidation;
using TruckOn.Trucks.Controllers.Contracts;

namespace TruckOn.Trucks.Controllers.Validation;

// Is applied to controllers automatically by SharpGrip FluentValidation AutoValidation package
public class TruckDTOValidator : AbstractValidator<TruckDTO>
{
    public TruckDTOValidator()
    {
        RuleFor(truck => truck.TruckCode).Matches("^[a-zA-Z0-9]+$").WithMessage("'{PropertyName}' must not be empty and consist only from alphanumeric characters");
        RuleFor(truck => truck.TruckName).NotEmpty();
    }
}
