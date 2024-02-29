using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual List<Product>? Products { get; set; }
}
