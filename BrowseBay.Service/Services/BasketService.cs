using BrowseBay.Service.DTOs;

namespace BrowseBay.Service.Services;

public class BasketService
{
    private List<PurchaseReadDto> _purchases;
    public bool OnEditMode { get; set; } = false;

    public virtual void AddNewBasket(IEnumerable<PurchaseReadDto> purchases)
    {
        _purchases = [.. purchases];
    }

    public virtual List<PurchaseReadDto> GetBasket()
    {
        return _purchases;
    }

    public virtual void ChangePurchaseCount(int cartId, int count)
    {
        PurchaseReadDto? purchase = _purchases.Find(c => c.Id == cartId);
        if (purchase is PurchaseReadDto purchaseDto)
        {
            purchaseDto.Quantity = count;

            if (purchaseDto.Quantity <= 0)
            {
                _purchases.Remove(purchaseDto);
            }
        }
    }

    public virtual double GetBasketTotal()
    {
        return _purchases.Sum(p => p.Quantity * p.Product.Price);
    }

    public virtual int GetNoOfItems()
    {
        return _purchases.Sum(c => c.Quantity);
    }

    public virtual double GetProductTotal(int productId)
    {
        PurchaseReadDto? purchase = _purchases.FirstOrDefault(p => p.ProductId == productId);

        return purchase is not null ? purchase.Product.Price * purchase.Quantity : 0.00;
    }
}
