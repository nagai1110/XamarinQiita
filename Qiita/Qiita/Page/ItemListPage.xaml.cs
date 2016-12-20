using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Auth;
using Qiita.API;

namespace Qiita.Page
{
	public partial class ItemListPage : ContentPage
	{
        ObservableCollection<QiitaItem> Items = new ObservableCollection<QiitaItem>();

        public ItemListPage ()
		{
			InitializeComponent ();

            ItemListView.ItemsSource = Items;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //QiitaAPI api = new QiitaAPI();
            //api.GetAllItems(
            //    items =>
            //    {
            //        foreach (QiitaItem item in items)
            //        {
            //            Items.Add(item);
            //        }
            //    },
            //    () =>
            //    {
            //        int bbb = 0;
            //    });

            var auth = new MyAuthenticator (
                "cdd6590e0e9bc747e989e91720f98e00bbfa7b7d",
                "a5ede5f7bd83c32360848c9466f9d1937eff20a7",
                "read_qiita",
                new Uri("https://qiita.com/api/v2/oauth/authorize"),
                new Uri("http://qiita.com/nagasakulllo"),
                new Uri("https://qiita.com/api/v2/access_tokens"));

            auth.AllowCancel = true;
          
            var oauther = DependencyService.Get<IQiitaOAuth>();
            auth.Completed += OnAuthenticationCompleted;
            oauther.StartOAuth(auth);
        }

        void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {

            }
        }
    }

    // TODO:仮
    public class MyAuthenticator : OAuth2Authenticator
    {
        public MyAuthenticator(string clientId, string scope, Uri authorizeUrl, Uri redirectUrl, GetUsernameAsyncFunc getUsernameAsync = null)
            : base(clientId, scope, authorizeUrl, redirectUrl, getUsernameAsync)
        {

        }
        public MyAuthenticator(string clientId, string clientSecret, string scope, Uri authorizeUrl, Uri redirectUrl, Uri accessTokenUrl, GetUsernameAsyncFunc getUsernameAsync = null)
            : base(clientId, clientSecret, scope, authorizeUrl, redirectUrl, accessTokenUrl, getUsernameAsync)
        {

        }

        protected override void OnRedirectPageLoaded(Uri url, IDictionary<string, string> query, IDictionary<string, string> fragment)
        {
            if (query.ContainsKey("code"))
            {
                var code = query["code"];

                var api = new QiitaAPI("cdd6590e0e9bc747e989e91720f98e00bbfa7b7d", "a5ede5f7bd83c32360848c9466f9d1937eff20a7");
                api.GetAccessToken(code,
                    json =>
                {
                    // TODO:アクセストークン保持
                    base.OnSucceeded(new Account());
                },
                () =>
                {
                    int bbb = 0;
                });
            }
            // base.OnRedirectPageLoaded(url, query, fragment);
        }
    }
}
