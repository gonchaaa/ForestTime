using ForestTime.Models;

namespace ForestTime.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Article> Articles { get; set; }
    }
}
