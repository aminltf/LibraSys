#nullable disable

using Application.Dtos.Address;
using Application.Dtos.Loan;
using FluentValidation;

namespace Application.Dtos.Member;

public record MemberDto(int MemberId, string FirstName, string LastName, DateTime DateOfBirth, AddressDto Address, string Phone, string Email, List<LoanDto> Loans)
{
};

public class MemberDtoValidator : AbstractValidator<MemberDto>
{
    public MemberDtoValidator()
    {
        RuleFor(x => x.MemberId).GreaterThan(0).WithMessage("Member Id must be greater than 0.");
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
        RuleFor(x => x.Loans).NotNull().WithMessage("Loans are required.");
    }

    private bool BeValidDateOfBirth(MemberDto member, DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.Now;
    }
}
