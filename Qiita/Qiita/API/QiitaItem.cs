using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Qiita.API
{
    [JsonObject("item")]
    class QiitaItem
    {
        [JsonProperty("rendered_body")]
        public string RenderdBody { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("coediting")]
        public bool Coediting { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("group")]
        public QiitaGroup Group { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        // 上手く取れない
        // [JsonProperty("tags")]
        // public string tags { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("user")]
        public QiitaUser User { get; set; }
    }
}
