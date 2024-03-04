using BrowseBay.Service.DTOs;

namespace BrowseBay.Service.Services;

public class PaginationWithSearchService : PaginationService
{
    public PaginationWithSearchService(PaginationService service, string searchString) : base(service.PageSize)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            SetItems(service._products);
        }
        else
        {
            SetItems(service._products.Where(p => SearchFunction(p, searchString)));
        }
    }

    private bool SearchFunction(ProductReadDto product, string searchKey)
    {
        if (product.Name.Contains(searchKey, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        string[] categoryNames = product.Category?
            .Select(c => c.Category?.Name)
            .OfType<string>()
            .ToArray() ?? [];
        
        if (categoryNames.Any(c => c.Contains(searchKey, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        return false;
    }
}
