using BrowseBay.Service.DTOs;

namespace BrowseBay.Service.Services;

public class CategoryStateManager
{
    protected HashSet<int> _selectedCategories = new HashSet<int>();

    public IEnumerable<CategoryReadDto> CategoryReadDtos { get; protected set; } = default!;

    public int[] SelectedCategoryIds => [.. _selectedCategories];

    protected CategoryStateManager() { }

    public void Toggle(int categoryId)
    {
        if (!_selectedCategories.Add(categoryId))
        {
            _selectedCategories.Remove(categoryId);
        }
    }

    public bool HasCategory(int categoryId)
    {
        return _selectedCategories.Contains(categoryId);
    }

    public IEnumerable<CategoryReadDto> GetCategories()
    {
        return CategoryReadDtos;
    }

    public bool Any()
    {
        return _selectedCategories.Any();
    }

    public void Clear()
    {
        CategoryReadDtos = new List<CategoryReadDto>();
        _selectedCategories.Clear();
    }


    public IEnumerable<ProductCategoryDto> ToProductCategoryDtos(int productId)
    {
        return _selectedCategories.Select(c => new ProductCategoryDto
        {
            ProductId = productId,
            CategoryId = c
        });
    }
}
