using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Models;
using Newtonsoft.Json;

namespace BingSearchNewsAPI
{
    public class BingSearchNewsClient
    {
        private readonly string _accessKey;

        private const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/news/search";

        public BingSearchNewsClient(string key)
        {
            _accessKey = key;
        }

        public async Task<BingSearchResponse> GetNews(string searchQuery)
        {
            var uriQuery = uriBase + "?q=" + searchQuery;

            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = _accessKey;
            HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            BingSearchResponse bingSearchResponse = JsonConvert.DeserializeObject<BingSearchResponse>(json);

            return bingSearchResponse;
        }
    }
}
