#nullable disable

using Application.Dtos.Address;
using FluentValidation;

namespace Application.Dtos.Member;

public record CreateMemberDto(string FirstName, string LastName, DateTime DateOfBirth, AddressDto Address, string Phone, string Email)
{
};

public class CreateMemberDtoValidator : AbstractValidator<CreateMemberDto>
{
    public CreateMemberDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("First name must not exceed 50 characters.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
        RuleFor(x => x.DateOfBirth).NotNull().WithMessage("Date of birth is required.");
        RuleFor(x => x.DateOfBirth).Must(BeValidDateOfBirth).WithMessage("Date of birth must be a valid date.");
        RuleFor(x => x.Address).NotNull().WithMessage("Address is required.");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required.");
        RuleFor(x => x.Phone).Matches(@"^\d{10}$").WithMessage("Phone must be a 10-digit number.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid.");
    }

    private bool BeValidDateOfBirth(CreateMemberDto member, DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.Now;
    }
}
