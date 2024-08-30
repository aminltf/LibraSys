#nullable disable

using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Book : BaseEntity<Guid>
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public Genre Genre { get; set; }
    public Guid AuthorId { get; set; }
    public string Publisher { get; set; }
    public int CopiesAvailable { get; set; }

    // Navigation properties
    public ICollection<BookAuthor> BookAuthors { get; set; }
    public ICollection<Loan> Loans { get; set; }

    public Book(Guid bookId, string title, int publicationYear, Genre genre, Guid authorId, string publisher, int copiesAvailable) : base()
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
