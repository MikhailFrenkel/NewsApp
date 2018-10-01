using System.Collections.Generic;
using System.Linq;
using NewsApp.DAL.Connection;
using NewsApp.DAL.Interface;
using NewsApp.DAL.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace NewsApp.DAL.Repositories
{
    public class TopicPageRepository : IRepository<TopicPage>
    {
        private readonly SQLiteConnection _db;

        public TopicPageRepository(string filename)
        {
            _db = DB.GetConnection(filename);
            _db.CreateTable<TopicPage>();
            _db.CreateTable<Article>();
        }

        public IEnumerable<TopicPage> GetItems()
        {
            return _db.GetAllWithChildren<TopicPage>().ToList();
        }

        public void RemoveItems()
        {
            _db.DeleteAll<TopicPage>();
        }

        public void SaveItem(TopicPage item)
        {
            _db.InsertWithChildren(item);
        }
    }
}
