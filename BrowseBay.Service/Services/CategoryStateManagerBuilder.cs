using BrowseBay.Service.DTOs;

namespace BrowseBay.Service.Services;

public class CategoryStateManagerBuilder : CategoryStateManager
{
    public CategoryStateManagerBuilder SetItems(IEnumerable<CategoryReadDto> categories)
    {
        CategoryReadDtos = categories;
        return this;
    }

    public CategoryStateManagerBuilder SetSelectedItems(IEnumerable<CategoryReadDto> categories)
    {
        categories.ToList().ForEach(c => _selectedCategories.Add(c.Id));
        return this;
    }

    public CategoryStateManager Build()
    {
        return this;
    }
}
