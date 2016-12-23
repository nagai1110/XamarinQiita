using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Auth;

namespace Qiita.API.OAuth
{
    public interface IQiitaOAuth
    {
        void StartOAuth(OAuth2Authenticator authenticator);
    }
}
