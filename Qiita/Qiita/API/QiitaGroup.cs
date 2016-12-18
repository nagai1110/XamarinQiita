using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Qiita.API
{
    [JsonObject("group")]
    class QiitaGroup
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("url_name")]
        public string UrlName { get; set; }
    }
}
