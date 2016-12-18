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

        public QiitaAPI() : this(null, null)
        { 
        }

        public QiitaAPI(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        //public async void OAuth()
        //{
        //    // とりあえずread固定
        //    string url = string.Format("https://qiita.com/api/v2/oauth/authorize?client_id={0}&scope=read_qiita", _clientId);

        //    HttpRequester requester = new HttpRequester();
        //    HttpRequestJob<HttpResponseMessage> job = requester.GET(url);

        //    HttpResponseMessage reponse = await job.GetResponse();
        //}

        public void GetAllItems(Action<List<QiitaItem>> completed, Action error)
        {
            string url = string.Format("https://qiita.com/api/v2/items");

            HttpRequester requester = new HttpRequester();
            HttpRequestJob<HttpResponseMessage> job = requester.GET(url);

            Task<HttpResponseMessage> task = job.GetResponse();
            task.ContinueWith(t => {
                switch(t.Status)
                {
                    case TaskStatus.RanToCompletion:
                        string json = t.Result.Content.ReadAsStringAsync().Result;
                        List<QiitaItem> items = JsonConvert.DeserializeObject<List<QiitaItem>>(json);
                        completed(items);
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
