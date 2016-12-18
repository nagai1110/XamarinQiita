using System;
using System.Net.Http;
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

        public HttpRequestJob<HttpResponseMessage> GET(string url)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<HttpResponseMessage> task = _client.GetAsync(url, cts.Token);

            HttpRequestJob<HttpResponseMessage> job = new HttpRequestJob<HttpResponseMessage>(task, () =>
            {
                if (cts.IsCancellationRequested) return;

                cts.Cancel();
            });

            return job;
        }

        public HttpRequestJob<HttpResponseMessage> POST(string url)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            // TODO HTTP Content
            Task<HttpResponseMessage> task = _client.PostAsync(url, null, cts.Token);

            HttpRequestJob<HttpResponseMessage> job = new HttpRequestJob<HttpResponseMessage>(task, () =>
            {
                if (cts.IsCancellationRequested) return;

                cts.Cancel();
            });

            return job;
        }
    }
}
