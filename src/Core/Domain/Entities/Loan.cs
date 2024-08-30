#nullable disable

using Domain.Common;

namespace Domain.Entities;

public class Loan : BaseEntity<Guid>
{
    public Guid LoanId { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public DateTime DueDate { get; set; }

    // Navigation properties
    public Book Book { get; set; }
    public Member Member { get; set; }

    public Loan(Guid loanId, Guid bookId, Guid memberId, DateTime loanDate, DateTime dueDate, DateTime? returnDate = null) : base()
    {
        // Validation checks
        if (loanDate == default)
            throw new ArgumentException("Loan date is required.", nameof(loanDate));
        if (dueDate == default)
            throw new ArgumentException("Due date is required.", nameof(dueDate));
        if (dueDate <= loanDate)
            throw new ArgumentException("Due date must be after the loan date.", nameof(dueDate));

        LoanId = loanId;
        BookId = bookId;
        MemberId = memberId;
        LoanDate = loanDate;
        DueDate = dueDate;
        ReturnDate = returnDate;

        // Initialize Navigation properties to null until explicitly set
        Book = null;
        Member = null;
    }

    // Optional: Methods to update navigation properties safely
    public void SetBook(Book book)
    {
        Book = book ?? throw new ArgumentNullException(nameof(book));
        BookId = book.BookId;
    }

    public void SetMember(Member member)
    {
        Member = member ?? throw new ArgumentNullException(nameof(member));
        MemberId = member.MemberId;
    }
}
