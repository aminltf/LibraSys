#nullable disable

using Application.Dtos.Book;
using FluentValidation;

namespace Application.Dtos.Author;

public record AuthorDto(int AuthorId, string FirstName, string LastName, DateTime DateOfBirth, string Biography, List<BookDto> Books)
{
};

public class AuthorDtoValidator : AbstractValidator<AuthorDto>
{
    public AuthorDtoValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Author Id is required.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.DateOfBirth).Must(BeAValidDate).WithMessage("Date of birth is not valid.");
        RuleFor(x => x.Biography).NotEmpty().WithMessage("Biography is required.");
        RuleFor(x => x.Books).NotNull().WithMessage("Books are required.");
        RuleForEach(x => x.Books).SetValidator(new BookDtoValidator());
    }

    private bool BeAValidDate(DateTime date)
    {
        return date <= DateTime.Now;
    }
}
