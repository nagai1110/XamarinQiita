using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Qiita.API
{
    [JsonObject("user")]
    class QiitaUser
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("facebook_id")]
        public string FacebookID { get; set; }

        [JsonProperty("followees_count")]
        public int FolloweesCount { get; set; }

        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        [JsonProperty("github_login_name")]
        public string GithubLoginName { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("items_count")]
        public int ItemsCount { get; set; }

        [JsonProperty("linkedin_id")]
        public string LinkedinID { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("oraganization")]
        public string Oraganization { get; set; }

        [JsonProperty("permanent_id")]
        public int PermanentID { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("twitter_screen_name")]
        public string TwitterScreenName { get; set; }

        [JsonProperty("website_url")]
        public string WebSiteUrl { get; set; }
    }
}
