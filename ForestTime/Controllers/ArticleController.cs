using ForestTime.Data;
using ForestTime.DTOs;
using ForestTime.Models;
using ForestTime.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Security.Claims;

namespace ForestTime.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public ArticleController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment, int articleId)
        {
            try
            {
                comment.ArticleCommentId = articleId;
                comment.UserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return RedirectToAction("Detail", "Article", new { Id = articleId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }


        public IActionResult Detail(int id)
        {

            var article = _context.Articles.Include(x => x.category).Include(x => x.User).Include(y => y.ArticleTags).ThenInclude(x => x.Tag).SingleOrDefault(x => x.Id == id);
            var nextArticles = _context.Articles.Include(x=>x.category).FirstOrDefault(x=>x.Id > id);
            var prevArticles = _context.Articles.Include(x => x.category).FirstOrDefault(x=>x.Id < id);
           

            //if (prevArticles == null)
            //{
            //    prevArticles = _context.Articles
            //        .Include(x => x.)
            //        .OrderByDescending(x => x.Id)
            //        .FirstOrDefault();
            //}
            //if (nextArticles == null)
            //{
            //    nextArticles = _context.Articles
            //        .Include(x => x.Category)
            //        .OrderBy(x => x.Id)
            //        .FirstOrDefault();
            //}

            if (article == null)
            {
                return NotFound();
            }
            var articlecomments = _context.Comments.Include(x => x.User).Where(x => x.ArticleCommentId == id).ToList();
            DetailVM vm = new()
            {
                Article = article,
                Comments = articlecomments,
                NextArticle=nextArticles,
                PrevArticle=prevArticles,
            };

            article.Views++;
            _context.Articles.Update(article);
            _context.SaveChanges();

            return View(vm);


        }

        public IActionResult Category(int categoryId)
        {
            var getByCategory = _context.Articles.Include(x => x.category).Where(x => x.category.Id == categoryId).ToList();
            var vm = new CategoryVM()
            {
                GetByCategory = getByCategory

            };

            return View(vm);
        }
    }
}
