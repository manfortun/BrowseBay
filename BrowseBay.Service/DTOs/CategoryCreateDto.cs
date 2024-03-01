using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Service.DTOs;

public class CategoryCreateDto
{
    [Required]
    public string Name { get; set; }
}
