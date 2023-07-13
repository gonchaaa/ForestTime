using ForestTime.Data;
using ForestTime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForestTime.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
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
            var articles = _context.Articles.Include(x => x.User).Include(a => a.category).Include(z => z.ArticleTags).ThenInclude(y => y.Tag).ToList();
            return View(articles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var tagList = _context.Tags.ToList();
            var categoryList = _context.Categories.ToList();
            ViewBag.Tags = new SelectList(tagList, "Id", "TagName");
            ViewBag.Categories = new SelectList(categoryList, "Id", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Article article, List<int> tagIds, int categoryId)
        {
            try
            {
                article.UserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                article.SeoUrl = "";
                article.CreatedDate = DateTime.Now;
                article.UpdatedDate = DateTime.Now;
                article.CategoryId = categoryId;
                await _context.Articles.AddAsync(article);
                await _context.SaveChangesAsync();
                for (int i = 0; i < tagIds.Count; i++)
                {
                    ArticleTag articleTag = new()
                    {
                        TagId = tagIds[i],
                        ArticleId = article.Id
                    };
                    await _context.ArticleTags.AddAsync(articleTag);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View(article);
            }
        }

        public IActionResult Edit(int id)
        {
            var article = _context.Articles.Include(x => x.ArticleTags).Include(y => y.category).SingleOrDefault(a => a.Id == id);

            if (article == null || id == null)
            {
                return NotFound();
            }
            var tags = _context.Tags.ToList();
            var cats = _context.Categories.ToList();
            ViewData["tagList"] = tags;
            ViewData["CatList"] = cats;
            ViewBag.categories = new SelectList(cats, "Id", "CategoryName");
            return View(article);
        }
        [HttpPost]
        public IActionResult Edit(Article article, List<int> tagIds)
        {
            article.UpdatedDate = DateTime.Now;
            article.UserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            article.SeoUrl = "";
            _context.Articles.Update(article);
            _context.SaveChanges();

            var articleTags = _context.ArticleTags.Where(a => a.ArticleId == article.Id).ToList();
            _context.ArticleTags.RemoveRange(articleTags);
            _context.SaveChanges();

            for (int i = 0; i < tagIds.Count; i++)
            {
                ArticleTag articleTag = new()
                {
                    TagId = tagIds[i],
                    ArticleId = article.Id
                };
                _context.ArticleTags.AddAsync(articleTag);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
