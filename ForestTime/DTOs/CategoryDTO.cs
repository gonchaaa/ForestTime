using ForestTime.Models;
using ForestTime.ViewModels;

namespace ForestTime.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Article> Articles { get; set; }
public List<CategoryVM> Categories { get; set; }
    }
}
