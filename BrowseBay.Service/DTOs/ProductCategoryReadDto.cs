using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Service.DTOs;

public class ProductCategoryReadDto
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int ProductId { get; set; }
}
