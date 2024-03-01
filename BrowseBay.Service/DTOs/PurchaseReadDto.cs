namespace BrowseBay.Service.DTOs;

public class PurchaseReadDto
{
    public int Id { get; set; }

    public string OwnerId { get; set; }
    
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual ProductOnPurchaseDto Product { get; set; } = default!;
}
