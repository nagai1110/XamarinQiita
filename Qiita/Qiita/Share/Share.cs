using System;
using System.Collections.Generic;
using System.Text;

using Plugin.Share;

namespace Qiita.Share
{
    public class Share
    {
        public async static void ShareUrlAsync(string url, string message, string title)
        {
            await CrossShare.Current.ShareLink(url, message, title);
        }

        public async static void ShareMessageAsync(string message, string title)
        {
            await CrossShare.Current.Share(message, title);
        }
    }
}
