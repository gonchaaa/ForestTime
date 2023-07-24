using ForestTime.Data;
using ForestTime.DTOs;
using ForestTime.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForestTime.ViewComponents
{
    public class SideViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SideViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topArticles = _context.Articles.OrderByDescending(x => x.Views).Take(3).ToList();
            var recentArticles = _context.Articles.OrderByDescending(x => x.Id).Take(3).ToList();
            var feedArticles = _context.Articles.OrderByDescending(z => z.Views).Take(9).ToList();
        

            // Retrieve categories and their article counts
            var popularCategories = _context.Articles.Include(x => x.category)
                .GroupBy(a => a.category.CategoryName)
                .Select(g => new CategoryCountDTO
                {
                    CategoryName = g.Key,
                    CategoryCount = g.Count()
                })
                .ToList();

            SideVM sideArticle = new SideVM
            {

                PopularCategories = popularCategories,
                TopArticles = topArticles,
                RecentAddedArticles = recentArticles,
                FeedArticles = feedArticles
            };

            return View("Side", sideArticle);
        }

    }
}
