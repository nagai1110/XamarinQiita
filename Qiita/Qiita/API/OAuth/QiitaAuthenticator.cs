using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Auth;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace Qiita.API.OAuth
{
    public class QiitaAuthenticator : OAuth2Authenticator
    {
        private Uri _redirectUrl;

        public QiitaAuthenticator(string clientId, string scope, Uri authorizeUrl, Uri redirectUrl, GetUsernameAsyncFunc getUsernameAsync = null)
            : base(clientId, scope, authorizeUrl, redirectUrl, getUsernameAsync)
        {
            _redirectUrl = redirectUrl;
        }
        public QiitaAuthenticator(string clientId, string clientSecret, string scope, Uri authorizeUrl, Uri redirectUrl, Uri accessTokenUrl, GetUsernameAsyncFunc getUsernameAsync = null)
            : base(clientId, clientSecret, scope, authorizeUrl, redirectUrl, accessTokenUrl, getUsernameAsync)
        {
            _redirectUrl = redirectUrl;
        }

        public void StartAuth()
        {
            // DependencyServiceを使う
            var oauther = DependencyService.Get<IQiitaOAuth>();
            oauther.StartOAuth(this);
        }

        protected override void OnPageEncountered(Uri url, IDictionary<string, string> query, IDictionary<string, string> fragment)
        {
            // ユーザーが認可してリダイレクトURLに遷移するタイミングを検知する
            if (!(url.AbsolutePath.Equals(_redirectUrl.AbsolutePath, StringComparison.OrdinalIgnoreCase)
                && url.Host.Equals(_redirectUrl.Host, StringComparison.OrdinalIgnoreCase)))
            {
                // ホストとパスのいずれかが違う場合はリダイレクトページではないと判断する
                base.OnPageEncountered(url, query, fragment);
                return;
            }

            if (query.ContainsKey("code"))
            {
                var code = query["code"];
                var api = new QiitaAPI(ClientId, ClientSecret);

                api.GetAccessToken(code,
                    accessToken =>
                    {
                        api.AccessToken = accessToken.AccessToken;
                        api.AuthenticatedUser(
                            user =>
                            {
                                OnSucceeded(new Account(
                                    user.ID,
                                    new Dictionary<string, string> {
                                        { "token", accessToken.AccessToken },
                                        { "user", JsonConvert.SerializeObject(user) }
                                    }
                                ));
                            },
                            () =>
                            {
                            });
                    },
                    () =>
                    {
                        OnError("Cannot get access token");
                    });
                return;
            }

            OnError("Cannot get code");
        }
    }
}
