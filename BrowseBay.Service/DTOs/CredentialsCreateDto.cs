using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace BrowseBay.Service.DTOs;

public class CredentialsCreateDto
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required]
    public int Role { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [DisplayName("Confirm password")]
    public string ConfirmPassword { get; set; } = default!;
}
