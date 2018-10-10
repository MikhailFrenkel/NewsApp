using System.Collections.Generic;

namespace NewsAppWithMvvmCross
{
    public static class Constants
    {
        public const string SyncFusionLicenseKey = "MzA2OTlAMzEzNjJlMzMyZTMwZkdNV3M1VHBER3cyMGRCNE4vbWkxeWxSQzdKSXF2ODRKZkIvd3EvSTRpOD0=";
        public const string BingSearchNewsKey = "968875276d614dea8c0f82afbbb6c853";
        public const string DatabaseName = "Articles.db";
        public const string SearhBarPlaceholderText = "Search topics and articles";

        public static List<string> Topics = new List<string>()
        {
            "Africa",
            "Asia",
            "Australia",
            "Europe",
            "North America",
            "South America"
        };

        public static class Images
        {
            public const string Edit = "edit.png";
            public const string Delete = "delete.png";
            public const string Add = "plus.png";
            public const string Share = "share.png";
            public const string UpAndDownArrows = "arrows.png";
            public const string TrashBin = "trash.png";
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
    }
}
