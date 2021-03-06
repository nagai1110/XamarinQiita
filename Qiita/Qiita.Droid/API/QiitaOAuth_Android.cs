﻿using System;
using Xamarin.Forms;
using Android.Content;
using Android.OS;
using Xamarin.Auth;
using Android.App;
using Qiita.API.OAuth.Android;

[assembly: Dependency(typeof(QiitaOAuth_Android))]

namespace Qiita.API.OAuth.Android
{
    public class QiitaOAuth_Android : IQiitaOAuth
    {
        public void StartOAuth(OAuth2Authenticator authenticator)
        {
            var activity = Forms.Context as Activity;
            activity.StartActivity(authenticator.GetUI(activity));
        }
    }
}