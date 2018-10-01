using System;
using NewsApp.DAL.Interface;
using NewsApp.iOS.Helpers;
using System.IO;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_iOS))]
namespace NewsApp.iOS.Helpers
{
    public class SQLite_iOS : ISQLite
    {
        public string GetDatabasePath(string filename)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libPath = Path.Combine(docPath, "..", "Library");
            return Path.Combine(libPath, filename);
        }
    }
}