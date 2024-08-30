#nullable disable

using Domain.Common;

namespace Domain.Entities;

public class Author : BaseEntity<Guid>
{
    public Guid AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Biography { get; set; }

    // Navigation properties
    public ICollection<BookAuthor> BookAuthors { get; set; }

    public Author() { }

    public Author(Guid authorId, string firstName, string lastName, DateTime dateOfBirth, string biography = "") : base()
    {
        // Validation checks
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
        if (dateOfBirth == default)
            throw new ArgumentException("Date of birth is required.", nameof(dateOfBirth));

        AuthorId = authorId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Biography = biography ?? string.Empty; // Default to an empty string if null

        BookAuthors = new List<BookAuthor>();
    }
}
