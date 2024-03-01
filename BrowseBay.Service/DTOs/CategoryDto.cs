using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Service.DTOs;

public class CategoryDto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
