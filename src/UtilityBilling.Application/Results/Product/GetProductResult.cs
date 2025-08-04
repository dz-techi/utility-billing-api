namespace UtilityBilling.Application.Results.Product;

public class GetProductResult
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}