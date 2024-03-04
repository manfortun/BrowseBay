using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services.Interfaces;

namespace BrowseBay.Service.Services;

public class PaginationService : IPaginationService<ProductReadDto>
{
    internal int _activePage = 1;
    internal int _noOfPages = 0;
    internal IEnumerable<ProductReadDto> _products;

    internal readonly int _pageSize;

    public int NoOfPages => _noOfPages;
    public int PageSize => _pageSize;
    public int ActivePage
    {
        get => _activePage;
        set
        {
            if (value < 1)
            {
                _activePage = _noOfPages;
            }
            else if (value > _noOfPages)
            {
                _activePage = 1;
            }
            else
            {
                _activePage = value;
            }
        }
    }

    public PaginationService(int pageSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

        _pageSize = pageSize;
        _products = default!;
    }

    public virtual IEnumerable<ProductReadDto> Get()
    {
        return _products
            .Skip((_activePage - 1) * _pageSize)
            .Take(_pageSize);
    }

    public virtual void SetItems(IEnumerable<ProductReadDto> items)
    {
        _products = items;
        _noOfPages = (int)Math.Ceiling((double)items.Count() / _pageSize);
        ActivePage = _activePage;
    }
}
