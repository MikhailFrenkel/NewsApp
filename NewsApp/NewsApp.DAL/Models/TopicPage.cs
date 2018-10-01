using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace NewsApp.DAL.Models
{
    public class TopicPage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string SearchQuery { get; set; }
        public bool UserPage { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Article> Articles { get; set; }
    }
}
