﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BrowseBay.Service.DTOs;

public class SignUpDto
{
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
