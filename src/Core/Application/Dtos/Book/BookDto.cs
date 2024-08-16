#nullable disable

using Application.Dtos.Author;
using Domain.Enums;
using FluentValidation;

namespace Application.Dtos.Book;

public record BookDto(int BookId, string Title, int PublicationYear, Genre Genre, string Publisher, int CopiesAvailable, List<AuthorDto> Authors)
{
};

public class BookDtoValidator : AbstractValidator<BookDto>
{
    public BookDtoValidator()
    {
        RuleFor(x => x.BookId).NotEmpty().WithMessage("Book Id is required.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.PublicationYear).GreaterThan(0).WithMessage("Publication year must be greater than 0.");
        RuleFor(x => x.Genre).IsInEnum().WithMessage("Invalid genre.");
        RuleFor(x => x.Publisher).NotEmpty().WithMessage("Publisher is required.");
        RuleFor(x => x.CopiesAvailable).GreaterThanOrEqualTo(0).WithMessage("Copies available must be 0 or more.");
        RuleFor(x => x.Authors).NotNull().WithMessage("Authors are required.");
        RuleForEach(x => x.Authors).SetValidator(new AuthorDtoValidator());
    }
}
