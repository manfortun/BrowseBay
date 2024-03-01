using AutoMapper;
using BrowseBay.DataAccess;
using BrowseBay.Models;
using BrowseBay.Service;
using BrowseBay.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrowseBay.Controllers
{
    [Authorize(Policy = nameof(Policy.AdminRights))]
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category? category = _unitOfWork.CategoryManager.Find(id);

            if (category is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public IActionResult Edit(CategoryDto model)
        {
            _unitOfWork.CategoryManager.Update(_mapper.Map<Category>(model));
            _unitOfWork.Save();

            TempData["success"] = "Product successfully changed.";
            return RedirectToAction("index", "category");
        }
    }
}
