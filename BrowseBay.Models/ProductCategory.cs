using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Models;

public class ProductCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int ProductId { get; set; }

    public virtual Category Category { get; set; }

    public virtual Product Product { get; set; }
}
