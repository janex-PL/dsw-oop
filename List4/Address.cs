using System.Text.RegularExpressions;

namespace List4;

public class Address
{
    private string _street = null!;
    private string _buildingNumber = null!;
    private string _postalCode = null!;
    private string _city = null!;
    public string Street
    {
        get => _street;
        set => _street = value is not null
            ? value
            : throw new ArgumentNullException(nameof(Street), $"{nameof(Street)} cannot be null");
    }

    public string BuildingNumber
    {
        get => _buildingNumber;
        set => _buildingNumber = Regex.IsMatch(value, @"\d+[a-zA-Z]?")
            ? value
            : throw new ArgumentException(
                $"{nameof(BuildingNumber)} must be a number with an optional letter at the end",
                nameof(BuildingNumber));
    }

    public string PostalCode
    {
        get => _postalCode;
        set => _postalCode = Regex.IsMatch(value, @"\d{2}-\d{3}")
            ? value
            : throw new ArgumentException(
                $"{nameof(PostalCode)} must be provided in following format: XX-XXX, where X is a digit",
                nameof(PostalCode));
    }

    public string City
    {
        get => _city;
        set => _city = value is not null
            ? value
            : throw new ArgumentNullException(nameof(City), $"{nameof(City)} cannot be null");
    }

    public Address(string street, string buildingNumber, string postalCode, string city)
    {
        Street = street;
        BuildingNumber = buildingNumber;
        PostalCode = postalCode;
        City = city;
    }

    public override string ToString()
    {
        return
            $"{nameof(Street)}: {Street}\n{nameof(BuildingNumber)}: {BuildingNumber}\n{nameof(PostalCode)}: {PostalCode}\n{nameof(City)}: {City}\n";
    }
}