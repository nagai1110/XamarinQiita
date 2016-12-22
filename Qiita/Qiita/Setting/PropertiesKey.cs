using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Qiita.Setting
{
    public static class PropertiesKey
    {
        public const string LoginUser       = @"login_user";
        public const string LoginUserToken  = @"login_user_token";
    }

    public static class PropertiesAccesser
    {
        private static object lockObject = new object();

        public static object Get(string key)
        {
            lock (lockObject)
            {
                if (Application.Current.Properties.ContainsKey(key))
                {
                    return Application.Current.Properties[key];
                }
            }

            return null;
        }

        public static void Set(string key, object value)
        {
            lock (lockObject)
            {
                Application.Current.Properties[key] = value;
            }
        }

        public static void Remove(string key)
        {
            lock (lockObject)
            {
                if (Application.Current.Properties.ContainsKey(key))
                {
                    Application.Current.Properties.Remove(key);
                }
            }
        }
    }
}
