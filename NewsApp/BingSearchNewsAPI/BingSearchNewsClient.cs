using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SearchNewsAPI.Interfaces;
using SearchNewsAPI.Models;
using static SearchNewsAPI.Constants;
using Uri = System.Uri;

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
        /// Sends request to bing service and return list of articles.
        /// </summary>
        /// <param name="searchQuery">Search string.</param>
        /// <returns>List of articles.</returns>
        public async Task<BingSearchResponse> GetNewsAsync(string searchQuery)
        {
            var uriQuery = Constants.Uri.BingUriBase + Constants.Uri.QuerySymbol + searchQuery
                           + Constants.Uri.Offset + 0 + Constants.Uri.OriginalImageTrue; 

            return await RequestToService(uriQuery);
        }


        /// <summary>
        /// Sends request to bing service and return list of articles.
        /// </summary>
        /// <param name="searchQuery">Search string.</param>
        /// <param name="offset">Number of offset articles.</param>
        /// <param name="count">Count of articles.</param>
        /// <returns>List of articles.</returns>
        public async Task<BingSearchResponse> GetNewsAsync(string searchQuery, int offset, int count = 10)
        {
            var uriQuery = Constants.Uri.BingUriBase + Constants.Uri.QuerySymbol + searchQuery 
                           + Constants.Uri.Offset + offset + Constants.Uri.OriginalImageTrue;

            return await RequestToService(uriQuery);
        }

        private async Task<BingSearchResponse> RequestToService(string uriQuery)
        {
            //TODO: обработка исключений?
            try
            {
                var request = WebRequest.Create(uriQuery);
                request.Headers[HttpHeader.SubscriptionKey] = _accessKey;

                var response = (HttpWebResponse)(await request.GetResponseAsync());                
                string json;
                using (StreamReader sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    json = await sr.ReadToEndAsync();
                    response.Dispose();
                }

                var bingSearchResponse = JsonConvert.DeserializeObject<BingSearchResponse>(json);

                return bingSearchResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
