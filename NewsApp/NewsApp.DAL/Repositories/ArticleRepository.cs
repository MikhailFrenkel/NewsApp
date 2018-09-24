using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsApp.DAL.Interface;
using NewsApp.DAL.Models;
using SQLite;
using Xamarin.Forms;

namespace NewsApp.DAL.Repositories
{
    public class ArticleRepository : IRepository<Article>
    {
        //TODO: locker?

        private readonly SQLiteConnection _db;

        public ArticleRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            _db = new SQLiteConnection(databasePath);
            _db.CreateTable<Article>();
        }

        public IEnumerable<Article> GetItems(string category)
        {
            return _db.Table<Article>().Where(x => x.Category == category).ToList();
        }

        public void RemoveItems()
        {
            _db.DeleteAll<Article>();
        }

        public void SaveItem(Article item)
        {
            if (item.Id != 0)
            {
                _db.Update(item);
            }
            else
            {
                _db.Insert(item);
            }
        }
    }
}
