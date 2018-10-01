using SQLite;

namespace NewsApp.DAL.Interface
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
