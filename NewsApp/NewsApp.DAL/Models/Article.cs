using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace NewsApp.DAL.Models
{
    public class Article
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DatePublished { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(typeof(TopicPage))]
        public int TopicPageId { get; set; }

        [ManyToOne]
        public TopicPage TopicPage { get; set; }
    }
}
