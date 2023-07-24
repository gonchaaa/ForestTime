using ForestTime.DTOs;
using ForestTime.Models;

namespace ForestTime.ViewModels
{
    public class CategoryVM
    {
        public List<Article> GetByCategory { get; set; }
        public List<Comment> RecentPosts { get; set; }
        public List<CategoryCountDTO> PopularCategories { get; set; }
        public List<Article> TopArticle { get; set; }
        public List<CategoryDTO> SideCategories { get; set; }

    }
}
