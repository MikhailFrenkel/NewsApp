namespace SearchNewsAPI
{
    /// <summary>
    /// Class that contains all constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Contains http headers name.
        /// </summary>
        public static class HttpHeader
        {
            public const string SubscriptionKey = "Ocp-Apim-Subscription-Key";
        }

        /// <summary>
        /// Contains services uri.
        /// </summary>
        public static class Uri
        {
            public const string BingUriBase = "https://api.cognitive.microsoft.com/bing/v7.0/news/search";

            public const string QuerySymbol = "?q=";

            public const string Offset = "&offset=";

            public const string Count = "&count=";

            public const string OriginalImageTrue = "&originalImg=true";
        }
    }
}
