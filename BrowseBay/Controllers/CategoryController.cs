using AutoMapper;
using BrowseBay.DataAccess;
using BrowseBay.Models;
using BrowseBay.Service;
using BrowseBay.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryCreateDto model)
        {
            Category catModel = _mapper.Map<Category>(model);

            // check if the category name is already existing
            bool isUnique = IsUnique(catModel);

            if (!isUnique)
            {
                ModelState.AddModelError(nameof(model.Name), "A category with this name already exists.");
                return View();
            }

            _unitOfWork.CategoryManager.Insert(catModel);
            _unitOfWork.Save();

            TempData["success"] = "New category added.";
            return RedirectToAction("index", "category");
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
            Category catModel = _mapper.Map<Category>(model);

            // check if the category name is already existing
            bool isUnique = IsUnique(catModel, c => c.Id != catModel.Id);

            if (!isUnique)
            {
                ModelState.AddModelError(nameof(model.Name), "A category with this name already exists.");
                return View();
            }

            _unitOfWork.CategoryManager.Update(_mapper.Map<Category>(model));
            _unitOfWork.Save();

            TempData["success"] = "Product successfully changed.";
            return RedirectToAction("index", "category");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category? category = _unitOfWork.CategoryManager.Find(id);

            if (category is null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryManager.Delete(id);
            _unitOfWork.Save();

            TempData["success"] = "Category deleted.";
            return RedirectToAction("index", "category");
        }

        /// <summary>
        /// Checks if the normalized name of <paramref name="newCategory"/> already exists in the database
        /// </summary>
        /// <param name="newCategory"></param>
        /// <param name="expression"></param>
        /// <returns><c>true</c> if unique. Otherwise, <c>false</c></returns>
        private bool IsUnique(Category newCategory, Expression<Func<Category, bool>>? expression = null)
        {
            return !_unitOfWork.CategoryManager
                .Get(expression)
                .Any(c => c.NormalizedString.Equals(newCategory.NormalizedString));
        }
    }
}
