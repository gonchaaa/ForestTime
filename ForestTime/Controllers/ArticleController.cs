using ForestTime.Data;
using ForestTime.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForestTime.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;

        public ArticleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            try
            {
                var article = _context.Articles.Include(x=>x.category).Include(x=>x.User).Include(y=>y.ArticleTags).ThenInclude(x=>x.Tag).SingleOrDefault(x => x.Id == id);
                var topArticles = _context.Articles.OrderByDescending(z=>z.Views).Take(9).ToList();
                if (article == null)
                {
                    return NotFound();
                }
                DetailVM vm = new()
                {
                    Article = article,
                    TopArticle=topArticles
                };

                article.Views++;
                _context.Articles.Update(article);
                _context.SaveChanges();

                return View(vm);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
              
        }
}
}
