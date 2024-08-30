#nullable disable

using Domain.Common;

namespace Domain.Entities;

public class BookAuthor(Guid bookId, Book book, Guid authorId, Author author) : BaseEntity<Guid>()
{
    public Guid BookId { get; set; } = bookId;
    public Book Book { get; set; } = book ?? throw new ArgumentNullException(nameof(book));
    public Guid AuthorId { get; set; } = authorId;
    public Author Author { get; set; } = author ?? throw new ArgumentNullException(nameof(author));
}
