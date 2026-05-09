namespace MatchPet.Shared.Models;

public class Address : Entity
{
  public Guid UserId { get; private set; }

  public string Country { get; private set; } = "BR";
  public string City { get; private set; } = string.Empty;
  public string District { get; private set; } = string.Empty;
  public string ZipCode { get; private set; } = string.Empty;
  public string Street { get; private set; } = string.Empty;
  public string Number { get; private set; } = string.Empty;

  public bool IsPrimary { get; private set; }

  private Address() { } // EF

  public Address(
    Guid userId,
    string country,
    string city,
    string district,
    string zipCode,
    string street,
    string number,
    bool isPrimary = false
  )
  {
    UserId = userId;
    SetAddress(country, city, district, zipCode, street, number);
    IsPrimary = isPrimary;
  }

  public void Update(
    string country,
    string city,
    string district,
    string zipCode,
    string street,
    string number
  )
  {
    SetAddress(country, city, district, zipCode, street, number);
  }

  public void SetAsPrimary()
  {
    IsPrimary = true;
  }

  public void RemovePrimary()
  {
    IsPrimary = false;
  }

  private void SetAddress(
    string country,
    string city,
    string district,
    string zipCode,
    string street,
    string number
  )
  {
    if (string.IsNullOrWhiteSpace(city))
      throw new ArgumentException("City is required");

    if (string.IsNullOrWhiteSpace(street))
      throw new ArgumentException("Street is required");

    Country = country;
    City = city;
    District = district;
    ZipCode = zipCode;
    Street = street;
    Number = number;
  }
}