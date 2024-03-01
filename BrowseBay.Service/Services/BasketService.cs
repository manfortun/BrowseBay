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

    public virtual void AddToBasket(ProductOnPurchaseDto product, string userId)
    {
        PurchaseReadDto? purchase = _purchases.FirstOrDefault(p => p.ProductId == product.Id);
        if (purchase is PurchaseReadDto purchaseDto)
        {
            purchaseDto.Quantity++;
        }
        else
        {
            _purchases.Add(new PurchaseReadDto
            {
                ProductId = product.Id,
                Product = product,
                OwnerId = userId,
                Quantity = 1
            });
        }
    }

    public virtual void TakeFromBasket(int productId)
    {
        PurchaseReadDto? purchase = _purchases.FirstOrDefault(p => p.ProductId == productId);
        if (purchase is PurchaseReadDto purchaseDto)
        {
            purchaseDto.Quantity--;

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
