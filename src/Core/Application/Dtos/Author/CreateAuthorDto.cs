#nullable disable

using FluentValidation;

namespace Application.Dtos.Author;

public record CreateAuthorDto(string FirstName, string LastName, DateTime DateOfBirth, string Biography)
{
};

public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.DateOfBirth).Must(BeAValidDate).WithMessage("Date of birth is not valid.");
        RuleFor(x => x.Biography).NotEmpty().WithMessage("Biography is required.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date <= DateTime.Now;
    }
}
