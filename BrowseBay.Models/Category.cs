using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [DisplayName("Category Name")]
    public string Name { get; set; }
}
