#nullable disable

using Domain.Enums;
using FluentValidation;

namespace Application.Dtos.Book;

public record CreateBookDto(string Title, int PublicationYear, Genre Genre, string Publisher, int CopiesAvailable, List<int> AuthorIds)
{
};

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.PublicationYear).GreaterThan(0).WithMessage("Publication year must be greater than 0.");
        RuleFor(x => x.Genre).IsInEnum().WithMessage("Invalid genre.");
        RuleFor(x => x.Publisher).NotEmpty().WithMessage("Publisher is required.");
        RuleFor(x => x.CopiesAvailable).GreaterThanOrEqualTo(0).WithMessage("Copies available must be 0 or more.");
        RuleFor(x => x.AuthorIds).NotNull().WithMessage("Author Ids are required.");
        RuleFor(x => x.AuthorIds).Must(HaveAtLeastOneItem).WithMessage("At least one author Id is required.");
    }

    private bool HaveAtLeastOneItem(List<int> list)
    {
        return list != null && list.Count > 0;
    }
}
