using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Service.DTOs;

public class LogInDto
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}
