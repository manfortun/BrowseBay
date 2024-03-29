﻿using AutoMapper;
using BrowseBay.DataAccess;
using BrowseBay.Models;
using BrowseBay.Service;
using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services;
using BrowseBay.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BrowseBay.Controllers
{
    public class ProductController : Controller
    {
        private static CategoryStateManager _categoryStateManager;
        private readonly IUploadService<IFormFile> _uploadService;
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public ProductController(
            AppDbContext context,
            UserManager<IdentityUser> userManager,
            IUploadService<IFormFile> uploadService,
            IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _userManager = userManager;
            _uploadService = uploadService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = nameof(Policy.BuyerRights))]
        public IActionResult ViewProduct(int id)
        {
            Product? product = _unitOfWork.ProductManager.Find(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProductReadDto>(product));
        }

        [HttpGet]
        [Authorize(Policy = nameof(Policy.SellerRights))]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = nameof(Policy.SellerRights))]
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
        [Authorize(Policy = nameof(Policy.SellerRights))]
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
        [Authorize(Policy = nameof(Policy.SellerRights))]
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
        [Authorize(Policy = nameof(Policy.SellerRights))]
        public IActionResult Delete(int id)
        {
            _unitOfWork.ProductManager.Delete(id);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted";
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [Authorize(Policy = nameof(Policy.SellerRights))]
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
        [Authorize(Policy = nameof(Policy.SellerRights))]
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
        [Authorize(Policy = nameof(Policy.BuyerRights))]
        public IActionResult AddToCart(int id)
        {
            string? userId = _userManager.GetUserId(User);

            Product? product = _unitOfWork.ProductManager.Find(id);

            if (product is null || string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            Purchase? purchase = _unitOfWork.PurchaseManager.Get(c => c.OwnerId == userId && c.ProductId == id).FirstOrDefault();

            if (purchase is null)
            {
                purchase = new Purchase
                {
                    OwnerId = userId,
                    ProductId = product.Id,
                    Quantity = 1,
                };

                _unitOfWork.PurchaseManager.Insert(purchase);
            }
            else
            {
                purchase.Quantity++;
                _unitOfWork.PurchaseManager.Update(purchase);
            }

            _unitOfWork.Save();

            return Ok(product.Name);
        }

        [HttpGet]
        [Authorize(Policy = nameof(Policy.SellerRights))]
        public IActionResult ToggleCategory(int id)
        {
            _categoryStateManager.Toggle(id);

            return PartialView("CategoryTogglePartialView", _categoryStateManager);
        }
    }
}
