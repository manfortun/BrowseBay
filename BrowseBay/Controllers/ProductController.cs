using AutoMapper;
using BrowseBay.DataAccess;
using BrowseBay.Models;
using BrowseBay.Service.DTOs;
using BrowseBay.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrowseBay.Controllers
{
    public class ProductController : Controller
    {
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
        public IActionResult Add(ProductReadDto model)
        {
            _unitOfWork.ProductManager.Insert(_mapper.Map<Product>(model));
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
    }
}
