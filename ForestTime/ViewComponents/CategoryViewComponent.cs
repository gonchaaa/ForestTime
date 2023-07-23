using ForestTime.Data;
using Microsoft.AspNetCore.Mvc;

namespace ForestTime.ViewComponents
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public CategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _context.Categories.Skip(1).Take(3).ToList();
            return View("Category", categories);
        }   
    }
}
