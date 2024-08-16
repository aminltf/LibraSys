#nullable disable

using FluentValidation;

namespace Application.Dtos.Author;

public record UpdateAuthorDto(int AuthorId, string FirstName, string LastName, DateTime DateOfBirth, string Biography)
{
};

public class UpdateAuthorDtoValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorDtoValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Author Id is required.");
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
