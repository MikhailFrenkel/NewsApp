using System.Collections.Generic;

namespace NewsApp
{
    public static class Constants
    {
        public const string SyncFusionLicenseKey = "MzA2OTlAMzEzNjJlMzMyZTMwZkdNV3M1VHBER3cyMGRCNE4vbWkxeWxSQzdKSXF2ODRKZkIvd3EvSTRpOD0=";
        public const string BingSearchNewsKey = "968875276d614dea8c0f82afbbb6c853";
        public const string DatabaseName = "Articles.db";
        public const string SearchBarPlaceholderText = "Search topics and articles";

        public static class CountNews
        {
            public const int CountPages = 10;
            public const int CountArticlesOnPage = 10;
        }

        /*public static List<string> Topics = new List<string>()
        {
            "Africa",
            "Asia",
            "Australia",
            "Europe",
            "North America",
            "South America"
        };*/

        public static Dictionary<string, string> Topics = new Dictionary<string, string>()
        {
            { "Continent", "Africa"},
            { "Continent", "Asia"},
            { "Continent", "Australia"},
            { "Continent", "Europe"},
            { "Continent", "North America"},
            { "Continent", "South America"},
            { "More topics", "Entertainment" },
            { "More topics", "Sport" },
            { "More topics", "Tech" },
            { "More topics", "Business" },
            { "More topics", "Football" }
        };

        public static class Images
        {
            public const string Edit = "edit.png";
            public const string Delete = "delete.png";
            public const string Add = "plus.png";
            public const string Share = "share.png";
            public const string UpAndDownArrows = "arrows.png";
            public const string TrashBin = "trash.png";
            public const string TrashBinRed = "trash_red.png";
        }

        public static class DefaultNews
        {
            public static Dictionary<string, string> Topics = new Dictionary<string, string>()
            {
                { "world", "World News"},
                { "russia", "News in Russia"},
                { "science", "Science news" }
            };
        }

        public static class B2C
        {
            public const string ApplicationId = "4171b9dc-8da7-4af2-8a43-b4f8fe10380b";
            public const string Authority = "https://login.microsoftonline.com/tfp/samnews.onmicrosoft.com/B2C_1_susi";
            public const string Edit = "https://login.microsoftonline.com/tfp/samnews.onmicrosoft.com/B2C_1_edit";
            public const string Reset = "https://login.microsoftonline.com/tfp/samnews.onmicrosoft.com/B2C_1_reset";
            public static readonly string RedirectUrl = $"msal{ApplicationId}://auth";
            public const string SusiPolicy = "B2C_1_susi";
            public const string EditPolicy = "B2C_1_edit";
            public const string ResetPolicy = "B2C_1_reset";
            public static readonly string[] Scopes = { "email" };
        }
    }
}
