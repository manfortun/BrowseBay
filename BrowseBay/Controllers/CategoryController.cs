using AutoMapper;
using BrowseBay.DataAccess;
using BrowseBay.Models;
using BrowseBay.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrowseBay.Controllers
{
    [Authorize(Policy = "AdminRights")]
    public class CategoryController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(
            AppDbContext context,
            IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _unitOfWork.CategoryManager.Get();

            return View(_mapper.Map<IEnumerable<CategoryReadDto>>(categories));
        }
    }
}
