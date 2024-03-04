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
        private static BasketService _localBasket;
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
            SyncDbBasketToLocalBasket();

            return _localBasket.GetNoOfItems() > 0 ?
                PartialView("BasketSummaryPartialView", _localBasket) :
                PartialView("NoContentPartialView");
        }

        [HttpGet]
        public IActionResult ToggleEditMode(bool onEditMode, bool save)
        {
            _localBasket.OnEditMode = onEditMode;

            if (save)
            {
                // sync the local basket to the database basket
                if (!onEditMode && !SyncLocalBasketToDbBasket())
                {
                    return BadRequest();
                }
            }
            else
            {
                // resyncs the database basket to the local basket
                return RedirectToAction("getcarts");
            }

            return _localBasket.GetNoOfItems() > 0 ?
                PartialView("BasketSummaryPartialView", _localBasket) :
                PartialView("NoContentPartialView");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            _localBasket.Clear();

            if (!SyncLocalBasketToDbBasket())
            {
                return BadRequest();
            }
            else
            {
                return PartialView("NoContentPartialView");
            }
        }

        [HttpGet]
        public IActionResult ChangeCount(int id, int count)
        {
            _localBasket.ChangePurchaseCount(id, count);

            return PartialView("BasketSummaryPartialView", _localBasket);
        }

        /// <summary>
        /// Gets the local basket and syncs into the database basket
        /// </summary>
        /// <returns></returns>
        private bool SyncLocalBasketToDbBasket()
        {
            string? userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            return MergeDbBasketWithLocalBasket(userId);
        }

        /// <summary>
        /// Merge the local basket to database basket
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool MergeDbBasketWithLocalBasket(string userId)
        {
            IEnumerable<Purchase> oldBasket = _unitOfWork.PurchaseManager.Get(c => c.OwnerId == userId);

            foreach (var purchase in oldBasket)
            {
                _unitOfWork.PurchaseManager.Delete(purchase);
            }

            return AddLocalBasketToDb();
        }

        /// <summary>
        /// Sets the local basket to database
        /// </summary>
        /// <returns></returns>
        private bool AddLocalBasketToDb()
        {
            IEnumerable<Purchase> newPurchase = _mapper.Map<IEnumerable<Purchase>>(_localBasket.GetBasket());

            foreach (var purchase in newPurchase)
            {
                purchase.Product = default!;
                _unitOfWork.PurchaseManager.Insert(purchase);
            }

            _unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// Gets the database basket and syncs into the local basket
        /// </summary>
        /// <returns></returns>
        private bool SyncDbBasketToLocalBasket()
        {
            _localBasket = new BasketService();

            string? userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            IEnumerable<Purchase> purchases = _unitOfWork.PurchaseManager.Get(c => c.OwnerId == userId);

            _localBasket.AddNewBasket(_mapper.Map<IEnumerable<PurchaseReadDto>>(purchases));
            return true;
        }
    }
}
