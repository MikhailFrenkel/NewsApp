using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SearchNewsAPI.Interfaces;
using SearchNewsAPI.Models;
using static SearchNewsAPI.Constants;

namespace SearchNewsAPI
{
    /// <summary>
    /// Client that uses bing search news services.
    /// </summary>
    public class BingSearchNewsClient : ISearchNewsClient<BingSearchResponse>
    {
        private readonly string _accessKey;

        /// <summary>
        /// Constructor that accept subscription key.
        /// </summary>
        /// <param name="key">Subscription key.</param>
        public BingSearchNewsClient(string key)
        {
            _accessKey = key;
        }

        /// <summary>
        /// Sends request on bing service and return list of articles.
        /// </summary>
        /// <param name="searchQuery">Search string.</param>
        /// <returns>List of articles.</returns>
        public async Task<BingSearchResponse> GetNewsAsync(string searchQuery)
        {
            var uriQuery = Uri.BingUriBase + Uri.QuerySymbol + searchQuery;

            var request = WebRequest.Create(uriQuery);
            request.Headers[HttpHeader.SubscriptionKey] = _accessKey;

            var response = (HttpWebResponse)(await request.GetResponseAsync());
            string json;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                json = await sr.ReadToEndAsync();
                response.Dispose();
            }

            var bingSearchResponse = JsonConvert.DeserializeObject<BingSearchResponse>(json);
            
            return bingSearchResponse;
        }
    }
}
