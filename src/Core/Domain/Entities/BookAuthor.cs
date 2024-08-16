#nullable disable

using Domain.Common;

namespace Domain.Entities;

public class BookAuthor(int bookId, Book book, int authorId, Author author) : BaseEntity<int>()
{
    public int BookId { get; private set; } = bookId;
    public Book Book { get; private set; } = book ?? throw new ArgumentNullException(nameof(book));
    public int AuthorId { get; private set; } = authorId;
    public Author Author { get; private set; } = author ?? throw new ArgumentNullException(nameof(author));
}
