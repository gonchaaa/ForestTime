using ForestTime.Data;
using ForestTime.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForestTime.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int Id)
        {

            var category = _context.Categories
                .FirstOrDefault(c => c.Id == Id);

            if (category == null)
            {

                return NotFound();
            }


            var articles = _context.Articles.Include(x => x.category)
                .Where(a => a.CategoryId == category.Id)
                .ToList();


            var categoryDTO = new CategoryDTO
            {
                CategoryId = category.Id,
                CategoryName = category.CategoryName,
                Articles = articles
            };

            return View(categoryDTO);
        }
    }
}
