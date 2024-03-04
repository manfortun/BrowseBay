using BrowseBay.Service.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BrowseBay.Service.Services.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> CreateAccountAsync(SignUpDto credentials);

    Task<IdentityUser> CreateAccountAsync(ExternalLoginInfo? info);

    Task<bool> ExternalLoginSignInAsync(ExternalLoginInfo info);

    Task<SignInResult> PasswordSignInAsync(LogInDto credentials);

    Task SignInAsync(IdentityUser user);

    Task SignOutAsync();

    Task<bool> IsSignedIn(ClaimsPrincipal user);
}
