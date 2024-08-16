#nullable disable

using FluentValidation;

namespace Application.Dtos.Loan;

public record CreateLoanDto(int BookId, int MemberId, DateTime LoanDate, DateTime DueDate)
{
};

public class CreateLoanDtoValidator : AbstractValidator<CreateLoanDto>
{
    public CreateLoanDtoValidator()
    {
        RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book Id must be greater than 0.");
        RuleFor(x => x.MemberId).GreaterThan(0).WithMessage("Member Id must be greater than 0.");
        RuleFor(x => x.LoanDate).NotNull().WithMessage("Loan date is required.");
        RuleFor(x => x.DueDate).NotNull().WithMessage("Due date is required.");
        RuleFor(x => x.DueDate).GreaterThan(x => x.LoanDate).WithMessage("Due date must be after loan date.");
    }
}
