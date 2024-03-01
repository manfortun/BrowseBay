using AutoMapper;
using BrowseBay.DataAccess;
using BrowseBay.Models;
using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrowseBay.Controllers
{
    public class BasketController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private static BasketService _basketService;
        public BasketController(
            AppDbContext context,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCarts()
        {
            _basketService = new BasketService();

            string? userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            IEnumerable<Purchase> purchases = _unitOfWork.PurchaseManager.Get(c => c.OwnerId == userId);

            _basketService.AddNewBasket(_mapper.Map<IEnumerable<PurchaseReadDto>>(purchases));

            return PartialView("BasketSummaryPartialView", _basketService);
        }

        [HttpGet]
        public IActionResult ToggleEditMode(bool mode, bool save)
        {
            _basketService.OnEditMode = mode;

            if (save)
            {
                // perform save logic here
            }

            return PartialView("BasketSummaryPartialView", _basketService);
        }
    }
}
