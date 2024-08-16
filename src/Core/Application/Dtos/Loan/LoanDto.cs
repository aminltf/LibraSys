#nullable disable

using FluentValidation;

namespace Application.Dtos.Loan;

public record LoanDto(int LoanId, int BookId, int MemberId, DateTime LoanDate, DateTime? ReturnDate, DateTime DueDate)
{
};

public class LoanDtoValidator : AbstractValidator<LoanDto>
{
    public LoanDtoValidator()
    {
        RuleFor(x => x.LoanId).GreaterThan(0).WithMessage("Loan Id must be greater than 0.");
        RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book Id must be greater than 0.");
        RuleFor(x => x.MemberId).GreaterThan(0).WithMessage("Member Id must be greater than 0.");
        RuleFor(x => x.LoanDate).NotNull().WithMessage("Loan date is required.");
        RuleFor(x => x.DueDate).NotNull().WithMessage("Due date is required.");
        RuleFor(x => x.DueDate).GreaterThan(x => x.LoanDate).WithMessage("Due date must be after loan date.");
        RuleFor(x => x.ReturnDate).Must(BeAfterLoanDate).WithMessage("Return date must be after loan date.");
    }

    private bool BeAfterLoanDate(LoanDto loan, DateTime? returnDate)
    {
        if (!returnDate.HasValue)
            return true;
        return returnDate > loan.LoanDate;
    }
}
