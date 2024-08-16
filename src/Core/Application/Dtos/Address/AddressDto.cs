#nullable disable

using FluentValidation;

namespace Application.Dtos.Address;

public record AddressDto(string Street, string City, string State, string PostalCode)
{
};

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.");
        RuleFor(x => x.Street).MaximumLength(100).WithMessage("Street must not exceed 100 characters.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.City).MaximumLength(50).WithMessage("City must not exceed 50 characters.");
        RuleFor(x => x.State).NotEmpty().WithMessage("State is required.");
        RuleFor(x => x.State).MaximumLength(2).WithMessage("State must be a 2-character abbreviation.");
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code is required.");
        RuleFor(x => x.PostalCode).Matches(@"^\d{5}(-\d{4})?$").WithMessage("Postal code must be a 5-digit or 9-digit number.");
    }
}
