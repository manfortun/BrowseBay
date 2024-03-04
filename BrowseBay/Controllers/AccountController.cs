using AutoMapper;
using Azure.Identity;
using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BrowseBay.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IAccountService accountService,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> LogIn(string returnUrl)
        {
            if (await _accountService.IsSignedIn(User))
            {
                return RedirectToAction("index", "home");
            }

            LogInDto model = new LogInDto
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto credentials)
        {
            SignInResult result = await _accountService.PasswordSignInAsync(credentials);

            if (!result.Succeeded)
            {
                var user = User;
                var info = await _signInManager.GetExternalLoginInfoAsync();

                bool test = await _accountService.ExternalLoginSignInAsync(info);

                return View("index", "home");

                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }

            return result.Succeeded ? RedirectToAction("index", "home") : View(credentials);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto credentials)
        {
            IdentityResult result = await _accountService.CreateAccountAsync(credentials);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();
            }

            LogInDto login = _mapper.Map<LogInDto>(credentials);

            SignInResult signInResult = await _accountService.PasswordSignInAsync(login);

            return signInResult.Succeeded ? RedirectToAction("index", "home") : BadRequest();
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            string redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LogInDto login = new LogInDto
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", login);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info is null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", login);
            }

            bool result = await _accountService.ExternalLoginSignInAsync(info);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();

            return RedirectToAction("login");
        }
    }
}
