using ForestTime.Data;
using ForestTime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForestTime.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories=_context.Categories.ToList();
            return View(categories);
        }    
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            try
            {
                category.CreatedDate = DateTime.Now;
                category.UpdatedDate = DateTime.Now;
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");       
            }
            catch (Exception ex)
            {

                return View("Index");
            };
        }
    }
}
