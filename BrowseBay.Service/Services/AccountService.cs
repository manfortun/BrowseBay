using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BrowseBay.Service.Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountService(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> CreateAccountAsync(SignUpDto credentials)
    {
        ArgumentNullException.ThrowIfNull(credentials);

        var identityUser = new IdentityUser
        {
            UserName = credentials.Email,
            Email = credentials.Email,
        };

        var identityRole = new IdentityRole
        {
            Name = ((Role)credentials.Role).ToString()
        };

        IdentityResult identityResult = await _userManager.CreateAsync(identityUser, credentials.Password);

        if (!identityResult.Succeeded)
        {
            return identityResult;
        }

        await _roleManager.CreateAsync(identityRole);

        await _userManager.AddToRoleAsync(identityUser, identityRole.Name);

        return identityResult;
    }

    public async Task<IdentityUser> CreateAccountAsync(ExternalLoginInfo? info)
    {
        var newUser = new IdentityUser
        {
            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
        };

        await _userManager.CreateAsync(newUser);

        return newUser;
    }

    public async Task<bool> ExternalLoginSignInAsync(ExternalLoginInfo info)
    {
        SignInResult signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                        info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (signInResult.Succeeded)
        {
            return true;
        }
        else
        {
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user is null)
                {
                    IdentityUser newUser = await CreateAccountAsync(info);

                    if (newUser is null)
                    {
                        return false;
                    }

                    user = newUser;
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                return await IsSignedIn(info.Principal);
            }
        }

        return false;
    }

    public async Task<bool> IsSignedIn(ClaimsPrincipal user)
    {
        IdentityUser? identityUser = await _userManager.GetUserAsync(user);

        if (identityUser is null)
        {
            return false;
        }

        return _signInManager.IsSignedIn(user);
    }

    public async Task<SignInResult> PasswordSignInAsync(LogInDto credentials)
    {
        return await _signInManager.PasswordSignInAsync(
            credentials.Email,
            credentials.Password,
            isPersistent: false,
            lockoutOnFailure: false);
    }

    public async Task SignInAsync(IdentityUser user)
    {
        await _signInManager.SignInAsync(user, isPersistent: false);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
