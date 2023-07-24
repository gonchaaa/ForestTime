using ForestTime.Data;
using ForestTime.DTOs;
using ForestTime.Models;
using ForestTime.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ForestTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var articles=_context.Articles.Include(x=>x.User).OrderByDescending(x=>x.Id).ToList();
            var tags=_context.Tags.ToList();
            var categories=_context.Categories.ToList();
        
            var headArticles = _context.Articles.Include(x=>x.category).OrderBy(x=>x.category.CategoryName).Take(3).ToList();
            HomeVM vm = new()
            {
                HomeArticles=articles,
                HomeTags=tags,
                Categories=categories,
                HeadArticles=headArticles
                
            }; 
            
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}