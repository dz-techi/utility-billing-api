using System.ComponentModel.DataAnnotations;

namespace UtilityBilling.Domain.Models;

public class Address
{
    [Key]
    public Guid Id { get; set; }
    public string Street { get; private set; }
    public string? Street2 { get; private set; } // Optional
    public string City { get; private set; }
    public string? State { get; private set; } // Or Province
    public string PostalCode { get; private set; }
    public string Country { get; private set; }

    private Address() { } // EF Core

    public Address(string street, string city, string postalCode, string country, string? street2 = null)
    {
        Street = street;
        Street2 = street2;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }
    
    public Address(string street, string city, string state, string postalCode, string country, string? street2 = null)
    {
        Street = street;
        Street2 = street2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }
}