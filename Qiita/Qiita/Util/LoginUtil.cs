using System;
using System.Collections.Generic;
using System.Text;

using Qiita.Setting;
using System.Runtime.CompilerServices;
using Qiita.API;
using Newtonsoft.Json;
using Qiita.API.OAuth;

namespace Qiita.Util
{
    class LoginUtil
    {
        public static bool IsLogined()
        {
            return PropertiesAccesser.Get(PropertiesKey.LoginUser) != null
                && PropertiesAccesser.Get(PropertiesKey.LoginUserToken) != null;
        }

        public static void Login(Action<QiitaUser, string> completed, Action cancelled, Action<Exception, string> error)
        {
            var auth = new QiitaAuthenticator(
                "cdd6590e0e9bc747e989e91720f98e00bbfa7b7d",
                "a5ede5f7bd83c32360848c9466f9d1937eff20a7",
                "read_qiita",
                new Uri("https://qiita.com/api/v2/oauth/authorize"),
                new Uri("http://qiita.com/nagasakulllo"),
                new Uri("https://qiita.com/api/v2/access_tokens"));

            auth.AllowCancel = true;
            auth.Completed += (sender, e) => {
                if (e.IsAuthenticated)
                {
                    string user = e.Account.Properties["user"];
                    string token = e.Account.Properties["token"];

                    PropertiesAccesser.Set(PropertiesKey.LoginUser, user);
                    PropertiesAccesser.Set(PropertiesKey.LoginUserToken, token);

                    completed?.Invoke(JsonConvert.DeserializeObject<QiitaUser>(user), token);
                }
                else
                {
                    cancelled?.Invoke();
                }
            };

            auth.Error += (sender, e) =>
            {
                error?.Invoke(e.Exception, e.Message);
            };

            auth.StartAuth();
        }

        public static void Logout()
        {
            PropertiesAccesser.Remove(PropertiesKey.LoginUser);
            PropertiesAccesser.Remove(PropertiesKey.LoginUserToken);
        }

        public static QiitaUser LoginUser()
        {
            if (IsLogined())
            {
                return JsonConvert.DeserializeObject<QiitaUser>(PropertiesAccesser.Get(PropertiesKey.LoginUser) as string);
            }

            return null;
        }

        public static string LoginUserToekn()
        {
            if (IsLogined())
            {
                return PropertiesAccesser.Get(PropertiesKey.LoginUserToken) as string;
            }

            return null;
        }
    }
}
