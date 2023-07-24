using ForestTime.DTOs;
using ForestTime.Models;

namespace ForestTime.ViewModels
{
    public class SideVM
    {
        public List<CategoryCountDTO> PopularCategories { get; set; }
        public List<Article> Articles { get; set; }
        public List<Article> TopArticles { get; set; }
        public List<Article> RecentAddedArticles { get; set; }
        public List<Article> FeedArticles { get; set; }

    }
}
