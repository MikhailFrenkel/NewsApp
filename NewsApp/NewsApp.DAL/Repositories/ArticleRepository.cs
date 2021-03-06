﻿using System.Collections.Generic;
using System.Linq;
using NewsApp.DAL.Connection;
using NewsApp.DAL.Interface;
using NewsApp.DAL.Models;
using SQLite;

namespace NewsApp.DAL.Repositories
{
    public class ArticleRepository : IRepository<Article>
    {
        private readonly SQLiteConnection _db;

        public ArticleRepository(string filename)
        {
            _db = DB.GetConnection(filename);
            _db.CreateTable<Article>();
        }

        public IEnumerable<Article> GetItems()
        {
            return _db.Table<Article>().ToList();
        }

        public void RemoveItems()
        {
            _db.DeleteAll<Article>();
        }

        public void SaveItem(Article item)
        {
            _db.Insert(item);
        }
    }
}
