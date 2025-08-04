using UtilityBilling.Contracts.Common;
using UtilityBilling.Domain.Models;

namespace UtilityBilling.Application.Results.Property;

public class PropertyItem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public PropertyType PropertyType { get; set; }

    public AddressResult Address { get; set; } = null!;

    public List<PropertyUserResult> Users { get; set; } = new();

    public static PropertyItem FromPropertyDto(Domain.Models.Property property)
    {
        return new PropertyItem
        {
            Id = property.Id,
            Name = property.Name,
            PropertyType = property.PropertyType,
            Address = AddressResult.FromAddress(property.Address),
            Users = property.PropertyUsers
                .Select(PropertyUserResult.FromPropertyUser)
                .ToList()
        };
    }
}

public class AddressResult
{
    public string Street { get; set; }

    public string? Street2 { get; set; }

    public string City { get; set; }

    public string? State { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public static AddressResult FromAddress(Address address)
    {
        return new AddressResult
        {
            Street = address.Street,
            Street2 = address.Street2,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }
}

public class PropertyUserResult
{
    public Guid UserId { get; set; }

    public Guid PropertyId { get; set; }

    public UserRole UserRole { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    
    public static PropertyUserResult FromPropertyUser(PropertyUser propertyUser)
    {
        return new PropertyUserResult
        {
            UserId = propertyUser.UserId,
            PropertyId = propertyUser.PropertyId,
            UserRole = propertyUser.Role,
            FirstName = propertyUser.User.FirstName,
            LastName = propertyUser.User.LastName,
            Email = propertyUser.User.Email
        };
    }
}