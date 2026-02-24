namespace LeadsManager.Domain.ValueObjects;

public class ContactInfo : IEquatable<ContactInfo>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    private ContactInfo() { }

    public ContactInfo(string firstName, string lastName, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string FullName => $"{FirstName} {LastName}";

    public bool Equals(ContactInfo? other)
    {
        if (other is null) return false;
        return FirstName == other.FirstName && 
               LastName == other.LastName && 
               Email == other.Email && 
               PhoneNumber == other.PhoneNumber;
    }

    public override bool Equals(object? obj) => Equals(obj as ContactInfo);
    public override int GetHashCode() => HashCode.Combine(FirstName, LastName, Email, PhoneNumber);
}
