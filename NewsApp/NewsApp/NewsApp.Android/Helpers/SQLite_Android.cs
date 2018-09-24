using NewsApp.DAL.Interface;
using NewsApp.Droid.Helpers;
using System.IO;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace NewsApp.Droid.Helpers
{
    public class SQLite_Android : ISQLite
    {
        public string GetDatabasePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}