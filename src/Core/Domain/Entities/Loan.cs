#nullable disable

using Domain.Common;

namespace Domain.Entities;

public class Loan : BaseEntity<int>
{
    public int LoanId { get; private set; }
    public int BookId { get; private set; }
    public int MemberId { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public DateTime DueDate { get; private set; }

    // Navigation properties
    public Book Book { get; private set; }
    public Member Member { get; private set; }

    public Loan(int loanId, int bookId, int memberId, DateTime loanDate, DateTime dueDate, DateTime? returnDate = null) : base()
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
