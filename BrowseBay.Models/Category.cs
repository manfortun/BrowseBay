using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowseBay.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [DisplayName("Category Name")]
    public string Name { get; set; }

    [NotMapped]
    public string NormalizedString => Name.ToUpperInvariant().Replace(" ", "");
}
