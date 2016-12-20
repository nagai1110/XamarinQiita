using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Qiita.API
{
    [JsonObject("access_token")]
    class QiitaAcessToken
    {
        [JsonProperty("client_id")]
        public string CliendID { get; set; }

        //[JsonProperty("scopes")]
        //public string Scopes { get; set; }

        [JsonProperty("token")]
        public string AccessToken { get; set; }
    }
}
