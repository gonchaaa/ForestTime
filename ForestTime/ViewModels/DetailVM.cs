using ForestTime.DTOs;
using ForestTime.Models;

namespace ForestTime.ViewModels
{
    public class DetailVM
    {
        public Article Article { get; set; }
        public Article PrevArticle { get; set; }
        public Article NextArticle { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
