using NewsApp.DAL.Interface;
using SQLite;
using Xamarin.Forms;

namespace NewsApp.DAL.Connection
{
    internal static class DB
    {
        private static SQLiteConnection _db;
        private static object sync = new object();

        internal static SQLiteConnection GetConnection(string filename)
        {
            if (_db == null)
            {
                lock (sync)
                {
                    if (_db == null)
                    {
                        string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
                        _db = new SQLiteConnection(databasePath);
                    }
                }
            }

            return _db;
        }
    }
}
