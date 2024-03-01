using BrowseBay.Service.DTOs;

namespace BrowseBay.Service.Services;

public class CategoryStateManagerBuilder : CategoryStateManager
{
    public CategoryStateManagerBuilder SetItems(IEnumerable<CategoryReadDto> categories)
    {
        CategoryReadDtos = categories;
        return this;
    }

    public CategoryStateManagerBuilder SetSelectedItems(IEnumerable<int> categoryIds)
    {
        foreach (var id in categoryIds)
        {
            _selectedCategories.Add(id);
        }

        return this;
    }

    public CategoryStateManager Build()
    {
        ArgumentNullException.ThrowIfNull(CategoryReadDtos);
        return this;
    }
}
