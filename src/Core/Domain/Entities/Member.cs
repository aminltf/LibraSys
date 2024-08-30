#nullable disable

using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Member : BaseEntity<Guid>
{
    public Guid MemberId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; } // Use Value Object
    public string Phone { get; set; }
    public string Email { get; set; }

    // Navigation properties
    public ICollection<Loan> Loans { get; set; }

    public Member(Guid memberId, string firstName, string lastName, DateTime dateOfBirth, Address address, string phone, string email) : base()
    {
        // Validation checks
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone Number cannot be null or empty", nameof(phone));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        MemberId = memberId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Phone = phone;
        Email = email;

        Loans = new List<Loan>();
    }
}
