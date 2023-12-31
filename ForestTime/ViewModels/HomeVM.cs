﻿using ForestTime.DTOs;
using ForestTime.Models;

namespace ForestTime.ViewModels
{
    public class HomeVM
    {
        public List<Article> HomeArticles { get; set; }
        public List<Tag> HomeTags { get; set; }
        public List<Category> Categories { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }
        public List<Article> HeadArticles { get; set; }

    }
}
