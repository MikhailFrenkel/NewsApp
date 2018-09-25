using System;
using SQLite;

namespace NewsApp.DAL.Models
{
    [Table("Articles")]
    public class Article
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DatePublished { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
    }
}
