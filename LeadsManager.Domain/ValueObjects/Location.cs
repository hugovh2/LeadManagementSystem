namespace LeadsManager.Domain.ValueObjects;

public class Location : IEquatable<Location>
{
    public string Suburb { get; private set; }
    public string? PostalCode { get; private set; }

    private Location() { }

    public Location(string suburb, string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(suburb))
            throw new ArgumentException("Suburb cannot be empty", nameof(suburb));

        Suburb = suburb;
        PostalCode = postalCode;
    }

    public bool Equals(Location? other)
    {
        if (other is null) return false;
        return Suburb == other.Suburb && PostalCode == other.PostalCode;
    }

    public override bool Equals(object? obj) => Equals(obj as Location);
    public override int GetHashCode() => HashCode.Combine(Suburb, PostalCode);
}
