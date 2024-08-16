#nullable disable

using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Member : BaseEntity<int>
{
    public int MemberId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Address Address { get; private set; } // Use Value Object
    public string Phone { get; private set; }
    public string Email { get; private set; }

    // Navigation properties
    public ICollection<Loan> Loans { get; private set; }

    public Member(int memberId, string firstName, string lastName, DateTime dateOfBirth, Address address, string phone, string email) : base()
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
