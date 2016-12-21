using System;
using System.Collections.Generic;
using System.Text;
using Qiita.Http;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Qiita.API
{
    class QiitaAPI
    {
        private string _clientId;
        private string _clientSecret;

        public string AccessToken { get; set; }

        public QiitaAPI() : this(null, null)
        { 
        }

        public QiitaAPI(string clientId, string clientSecret) : this(clientId, clientSecret, null)
        {
        }

        public QiitaAPI(string clientId, string clientSecret, string accessToken)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            AccessToken = accessToken;
        }

        //public async void OAuth()
        //{
        //    // とりあえずread固定
        //    string url = string.Format("https://qiita.com/api/v2/oauth/authorize?client_id={0}&scope=read_qiita", _clientId);

        //    HttpRequester requester = new HttpRequester();
        //    HttpRequestJob<HttpResponseMessage> job = requester.GET(url);

        //    HttpResponseMessage reponse = await job.GetResponse();
        //}

        public void GetAccessToken(String code, Action<QiitaAcessToken> completed, Action error)
        {
            var url = string.Format("https://qiita.com/api/v2/access_tokens");
            var body = new Dictionary<string, string> {
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "code", code },
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requester = new HttpRequester();
            HttpRequestJob<HttpResponseMessage> job = requester.POST(url, content);

            Task<HttpResponseMessage> task = job.GetResponse();
            task.ContinueWith(t => {
                switch (t.Status)
                {
                    case TaskStatus.RanToCompletion:
                        string str = t.Result.Content.ReadAsStringAsync().Result;
                        var accessToken = JsonConvert.DeserializeObject<QiitaAcessToken>(str);
                        completed(accessToken);
                        break;
                    default:
                        break;
                }
            });
        }

        public void AuthenticatedUser(Action<QiitaUser> completed, Action error)
        {
            var url = string.Format("https://qiita.com/api/v2/authenticated_user");

            var requester = new HttpRequester();
            if (!string.IsNullOrEmpty(AccessToken)) requester.SetBearerToken(AccessToken);
            HttpRequestJob<HttpResponseMessage> job = requester.GET(url);

            Task<HttpResponseMessage> task = job.GetResponse();
            task.ContinueWith(t => {
                switch (t.Status)
                {
                    case TaskStatus.RanToCompletion:
                        string str = t.Result.Content.ReadAsStringAsync().Result;
                        var user = JsonConvert.DeserializeObject<QiitaUser>(str);
                        completed(user);
                        break;
                    default:
                        break;
                }
            });
        }

        public void GetAllItems(Action<List<QiitaItem>> completed, Action error)
        {
            var url = string.Format("https://qiita.com/api/v2/items");

            var requester = new HttpRequester();
            if (!string.IsNullOrEmpty(AccessToken)) requester.SetBearerToken(AccessToken); 
            HttpRequestJob<HttpResponseMessage> job = requester.GET(url);

            Task<HttpResponseMessage> task = job.GetResponse();
            task.ContinueWith(t => {
                switch(t.Status)
                {
                    case TaskStatus.RanToCompletion:
                        string str = t.Result.Content.ReadAsStringAsync().Result;
                        var items = JsonConvert.DeserializeObject<List<QiitaItem>>(str);
                        completed(items);
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
