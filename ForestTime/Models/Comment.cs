namespace ForestTime.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int ArticleCommentId { get; set; }
        public Article ArticleComment { get; set; }
        public User User { get; set; }

    }
}
