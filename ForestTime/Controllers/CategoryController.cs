using ForestTime.Data;
using ForestTime.DTOs;
using ForestTime.ViewModels;
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

            var category = _context.Categories.FirstOrDefault(c => c.Id == Id);
            var topArticles = _context.Articles.OrderByDescending(z => z.Views).Take(9).ToList();
            var recentPosts = _context.Comments.Include(x => x.ArticleComment).OrderByDescending(x => x.Id).GroupBy(u => u.ArticleCommentId).Select(g => g.First()).Take(3).ToList();
            var popularCat = _context.Articles.Include(x => x.category).GroupBy(x => x.category.CategoryName).Select(x => new CategoryCountDTO
            {
                CategoryName = x.Key,
                CategoryCount = x.Count()
            }).ToList();
            CategoryVM vm = new()
            {
                TopArticle=topArticles,
                RecentPosts=recentPosts,
                PopularCategories=popularCat
            };

            if (category == null)
            {

                return NotFound();
            }


            var articles = _context.Articles.Include(x => x.category).Include(x=>x.User)
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
