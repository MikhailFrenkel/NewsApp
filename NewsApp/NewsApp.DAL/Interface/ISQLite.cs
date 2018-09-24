using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.DAL.Interface
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
