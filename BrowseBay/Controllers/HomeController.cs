using AutoMapper;
using BrowseBay.DataAccess;
using BrowseBay.Models;
using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services;
using BrowseBay.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrowseBay.Controllers
{
    public class HomeController : Controller
    {
        private static readonly PaginationService _paginationService = new PaginationService(12);
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public HomeController(
            AppDbContext context,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetItems(int? pageNo)
        {
            IEnumerable<Product> products = _unitOfWork.ProductManager.Get();

            _paginationService.SetItems(_mapper.Map<IEnumerable<ProductReadDto>>(products));

            if (pageNo is int intPageNo)
            {
                _paginationService.ActivePage = intPageNo;
            }

            return PartialView("ProductsDisplayPartialView", _paginationService);
        }

        public IActionResult Search(string searchString)
        {
            var searchWrapper = new PaginationWithSearchService(_paginationService, searchString);

            return PartialView("ProductsDisplayPartialView", searchWrapper);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
