using BrowseBay.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BrowseBay.Controllers
{
    public class BasketController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public BasketController(AppDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
