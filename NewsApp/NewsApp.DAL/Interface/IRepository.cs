using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.DAL.Interface
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetItems();
        void RemoveItems();
        void SaveItem(T item);
    }
}
