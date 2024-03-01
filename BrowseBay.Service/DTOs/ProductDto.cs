using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Service.DTOs;

public class ProductDto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public double Price { get; set; }

    [MaxLength(150)]
    public string? Description { get; set; } = default!;

    public string? ImageSource { get; set; }
}
