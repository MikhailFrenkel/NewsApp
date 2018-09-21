using System.Threading.Tasks;

namespace SearchNewsAPI.Interfaces
{
    /// <summary>
    /// Interface for finding news.
    /// </summary>
    /// <typeparam name="T">Finding result.</typeparam>
    public interface ISearchNewsClient<T>
    {
        /// <summary>
        /// Asynchronous method for finding news.
        /// </summary>
        /// <param name="searchQuery">Search string.</param>
        /// <returns>List of articles.</returns>
        Task<T> GetNewsAsync(string searchQuery);
    }
}
