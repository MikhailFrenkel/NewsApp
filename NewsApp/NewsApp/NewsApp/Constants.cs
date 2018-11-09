using System.Collections.Generic;
using NewsApp.Models;
using NewsApp.Resources;

namespace NewsApp
{
    public static class Constants
    {
        public const string SyncFusionLicenseKey = "MzA2OTlAMzEzNjJlMzMyZTMwZkdNV3M1VHBER3cyMGRCNE4vbWkxeWxSQzdKSXF2ODRKZkIvd3EvSTRpOD0=";
        public const string BingSearchNewsKey = "968875276d614dea8c0f82afbbb6c853";
        public const string DatabaseName = "Articles.db";
        public const string AppCenterLicenseKey = "android=6f03a3dd-2147-4af6-82e5-6ed081445fdf;";

        public static class CountNews
        {
            public const int CountPages = 10;
            public const int CountArticlesOnPage = 10;
        }

        public static List<Topic> Topics = new List<Topic>()
        {
            new Topic{ GroupName = Resource.TopicsGroupHeaderContinent, Name = Resource.TopicsContinentAfrica },
            new Topic{ GroupName = Resource.TopicsGroupHeaderContinent, Name = Resource.TopicsContinentAsia },
            new Topic{ GroupName = Resource.TopicsGroupHeaderContinent, Name = Resource.TopicsContinentAustralia },
            new Topic{ GroupName = Resource.TopicsGroupHeaderContinent, Name = Resource.TopicsContinentEurope },
            new Topic{ GroupName = Resource.TopicsGroupHeaderContinent, Name = Resource.TopicsContinentNorthAmerica },
            new Topic{ GroupName = Resource.TopicsGroupHeaderContinent, Name = Resource.TopicsContinentSouthAmerica },
            new Topic{ GroupName = Resource.TopicsGroupHeaderMoreTopics, Name = Resource.MoreTopicsEntertainment },
            new Topic{ GroupName = Resource.TopicsGroupHeaderMoreTopics, Name = Resource.MoreTopicsSport },
            new Topic{ GroupName = Resource.TopicsGroupHeaderMoreTopics, Name = Resource.MoreTopicsTech },
            new Topic{ GroupName = Resource.TopicsGroupHeaderMoreTopics, Name = Resource.MoreTopicsBusiness },
            new Topic{ GroupName = Resource.TopicsGroupHeaderMoreTopics, Name = Resource.MoreTopicsFootball }
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
                { Resource.DefaultTopicWorldNewsSearchString, Resource.DefaultTopicWorldNewsText},
                { Resource.DefaultTopicRussiaNewsSearchString, Resource.DefaultTopicRussiaNewsText},
                { Resource.DefaultTopicScienceSearchString, Resource.DefaultTopicScienceText }
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
