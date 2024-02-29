using BrowseBay.Service.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BrowseBay.Service.Services.Interfaces;

public interface IAccountService
{
    Task<bool> CreateAccountAsync(CredentialsCreateDto credentials);

    Task<SignInResult> SignInAsync(LogInDto credentials);

    Task SignOutAsync();

    Task<bool> IsSignedIn(ClaimsPrincipal user);
}
