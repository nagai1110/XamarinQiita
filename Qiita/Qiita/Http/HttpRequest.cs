using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Qiita.Http
{
    class HttpRequestJob<TResult>
    {
        private Task<TResult> _response;
        private Action _cancelAction;

        public HttpRequestJob(Task<TResult> response, Action cancelAction)
        {
            _response = response;
            _cancelAction = cancelAction;
        }

        public async Task<TResult> GetResponse()
        {
            return await _response;
        }

        public void Cancel()
        {
            _cancelAction();
        }
    }

    class HttpRequester
    {
        private HttpClient _client;

        public HttpRequester()
        {
            _client = new HttpClient();
        }

        public void SetBearerToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public HttpRequestJob<HttpResponseMessage> GET(string url)
        {
            var cts = new CancellationTokenSource();
            Task<HttpResponseMessage> task = _client.GetAsync(url, cts.Token);

            var job = new HttpRequestJob<HttpResponseMessage>(task, () =>
            {
                if (cts.IsCancellationRequested) return;

                cts.Cancel();
            });

            return job;
        }

        public HttpRequestJob<HttpResponseMessage> POST(string url, HttpContent httpContent)
        {
            var cts = new CancellationTokenSource();
            Task<HttpResponseMessage> task = _client.PostAsync(url, httpContent, cts.Token);

            var job = new HttpRequestJob<HttpResponseMessage>(task, () =>
            {
                if (cts.IsCancellationRequested) return;

                cts.Cancel();
            });

            return job;
        }
    }
}
