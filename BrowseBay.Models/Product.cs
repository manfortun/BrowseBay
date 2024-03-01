using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public double Price { get; set; }

    [Required]
    [MaxLength(150)]
    public string Description { get; set; } = default!;

    public string? ImageSource { get; set; }
    public virtual List<ProductCategory>? Category { get; set; }
}
