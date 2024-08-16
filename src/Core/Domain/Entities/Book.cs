#nullable disable

using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Book : BaseEntity<int>
{
    public int BookId { get; private set; }
    public string Title { get; private set; }
    public int PublicationYear { get; private set; }
    public Genre Genre { get; private set; }
    public int AuthorId { get; private set; }
    public string Publisher { get; private set; }
    public int CopiesAvailable { get; private set; }

    // Navigation properties
    public ICollection<BookAuthor> BookAuthors { get; private set; }
    public ICollection<Loan> Loans { get; private set; }

    public Book(int bookId, string title, int publicationYear, Genre genre, int authorId, string publisher, int copiesAvailable) : base()
    {
        // Validation checks
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));
        if (publicationYear <= 0)
            throw new ArgumentException("Publication year must be a positive integer.", nameof(publicationYear));
        if (string.IsNullOrWhiteSpace(publisher))
            throw new ArgumentException("Publisher cannot be null or empty.", nameof(publisher));
        if (copiesAvailable < 0)
            throw new ArgumentException("Copies available cannot be negative.", nameof(copiesAvailable));

        BookId = bookId;
        Title = title;
        PublicationYear = publicationYear;
        Genre = genre;
        AuthorId = authorId;
        Publisher = publisher;
        CopiesAvailable = copiesAvailable;

        BookAuthors = new List<BookAuthor>();
        Loans = new List<Loan>();
    }
}
