using AutoMapper;
using BrowseBay.DataAccess;
using BrowseBay.Models;
using BrowseBay.Service;
using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services;
using BrowseBay.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BrowseBay.Controllers
{
    [Authorize(Policy = nameof(Policy.SellerRights))]
    public class ProductController : Controller
    {
        private static CategoryStateManager _categoryStateManager;
        private readonly IUploadService<IFormFile> _uploadService;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(
            AppDbContext context,
            IUploadService<IFormFile> uploadService,
            IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _uploadService = uploadService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductCreateDto model)
        {
            // save product to database
            EntityEntry<Product> entityEntry = _unitOfWork.ProductManager.Insert(_mapper.Map<Product>(model));
            _unitOfWork.Save();

            // save product categories to database
            if (_categoryStateManager.Any())
            {
                int id = entityEntry.Entity.Id;
                IEnumerable<ProductCategoryDto> newCategoryDtos = _categoryStateManager.ToProductCategoryDtos(id);

                foreach (var dto in newCategoryDtos)
                {
                    ProductCategory newProductCategory = _mapper.Map<ProductCategory>(dto);
                    _unitOfWork.ProductCategoryManager.Insert(newProductCategory);
                }
            }

            _categoryStateManager.Clear();
            _unitOfWork.Save();

            TempData["success"] = "Product added successfully";
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product? product = _unitOfWork.ProductManager.Find(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public IActionResult Edit(ProductDto model)
        {
            _unitOfWork.ProductManager.Update(_mapper.Map<Product>(model));

            IEnumerable<ProductCategory> toDelete = _unitOfWork.ProductCategoryManager
                .Get(c => c.ProductId == model.Id);
            IEnumerable<ProductCategoryDto> toAdd = _categoryStateManager.ToProductCategoryDtos(model.Id);

            // delete the old categories of the product
            foreach (var delete in toDelete)
            {
                _unitOfWork.ProductCategoryManager.Delete(delete);
            }

            // replace with the new categories
            foreach (var add in toAdd)
            {
                _unitOfWork.ProductCategoryManager.Insert(_mapper.Map<ProductCategory>(add));
            }

            _unitOfWork.Save();

            TempData["success"] = "Product successfully updated";
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _unitOfWork.ProductManager.Delete(id);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted";
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public async Task<IActionResult> SetImage(IFormFile file)
        {
            string finalizedFileName = _uploadService.GetAvailableFileName(file.FileName);

            if (!await _uploadService.Upload(file))
            {
                return BadRequest("Please select a correct image format.");
            }

            return Content($"~/images/{finalizedFileName}");
        }

        [HttpGet, HttpPost]
        public IActionResult GetCategories(int id)
        {
            IEnumerable<Category> categories = _unitOfWork.CategoryManager.Get();

            var builder = new CategoryStateManagerBuilder();

            builder = builder
                .SetItems(_mapper.Map<IEnumerable<CategoryReadDto>>(categories));

            // obtain the categories of the product with the specified id
            if (id > 0)
            {
                IEnumerable<ProductCategory> productCategories = _unitOfWork.ProductCategoryManager
                    .Get(c => c.ProductId == id);

                builder = builder.SetSelectedItems(productCategories.Select(c => c.CategoryId));
            }

            _categoryStateManager = builder.Build();

            return PartialView("CategoryTogglePartialView", _categoryStateManager);
        }

        [HttpGet]
        public IActionResult ToggleCategory(int id)
        {
            _categoryStateManager.Toggle(id);

            return PartialView("CategoryTogglePartialView", _categoryStateManager);
        }
    }
}
