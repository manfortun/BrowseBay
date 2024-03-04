using AutoMapper;
using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BrowseBay.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(
            IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> LogIn()
        {
            if (await _accountService.IsSignedIn(User))
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto credentials)
        {
            SignInResult result = await _accountService.SignInAsync(credentials);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }

            return result.Succeeded ? RedirectToAction("index", "home") : View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto credentials)
        {
            bool succeeded = await _accountService.CreateAccountAsync(credentials);

            if (!succeeded)
            {
                return BadRequest();
            }

            LogInDto login = _mapper.Map<LogInDto>(credentials);

            SignInResult result = await _accountService.SignInAsync(login);

            return result.Succeeded ? RedirectToAction("index", "home") : throw new InvalidOperationException();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();

            return RedirectToAction("login");
        }
    }
}
