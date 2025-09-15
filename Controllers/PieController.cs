using BethanysPieShopAdmin.Models;
using BethanysPieShopAdmin.Models.Repositories;
using BethanysPieShopAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BethanysPieShopAdmin.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: Pies
        public async Task<IActionResult> Index()
        {
            var pies = await _pieRepository.GetAllPiesAsync();
            return View(pies);
        }

        // GET: Pie/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pie = await _pieRepository.GetPieByIdAsync(id);
            if (pie == null)
            {
                return NotFound();
            }
            return View(pie);
        }

        // GET: Pie/Add
        public async Task<IActionResult> Add()
        {
            try
            {
                IEnumerable<Category>? allCategories = await _categoryRepository.GetAllCategoriesAsync();
                IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);

                PieAddViewModel pieAddViewModel = new()
                {
                    Categories = selectListItems
                };
                return View(pieAddViewModel);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"There was an error: {ex.Message}";
            }

            return View(new PieAddViewModel());
        }

        // POST: Pie/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PieAddViewModel pieAddViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Pie pie = new()
                    {
                        CategoryId = pieAddViewModel.Pie.CategoryId,
                        ShortDescription = pieAddViewModel.Pie.ShortDescription,
                        LongDescription = pieAddViewModel.Pie.LongDescription,
                        Price = pieAddViewModel.Pie.Price,
                        AllergyInformation = pieAddViewModel.Pie.AllergyInformation,
                        ImageThumbnailUrl = pieAddViewModel.Pie.ImageThumbnailUrl,
                        ImageUrl = pieAddViewModel.Pie.ImageUrl,
                        InStock = pieAddViewModel.Pie.InStock,
                        IsPieOfTheWeek = pieAddViewModel.Pie.IsPieOfTheWeek,
                        Name = pieAddViewModel.Pie.Name,
                    };

                    await _pieRepository.AddPieAsync(pie);

                    Console.WriteLine("PieController: Add action executed.");
                    Console.WriteLine("Arjwwan test 54");


                    Console.WriteLine("Sherif  Check");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Adding the pie failed, please try again! Error: {ex.Message}");
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);
            pieAddViewModel.Categories = selectListItems;

            return View(pieAddViewModel);
        }
    }
}
