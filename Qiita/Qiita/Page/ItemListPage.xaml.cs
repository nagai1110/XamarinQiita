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

            var auth = new OAuth2Authenticator(
                "cdd6590e0e9bc747e989e91720f98e00bbfa7b7d",
                "read_qiita",
                new Uri("https://qiita.com/api/v2/oauth/authorize"),
                new Uri("http://qiita.com/nagasakulllo"));

            auth.AllowCancel = true;
            auth.Completed += async (object sender, AuthenticatorCompletedEventArgs ee) => {

                // Now that we're logged in, make a OAuth2 request to get the user's info.
                var accessToken = ee.Account.Properties["access_token"].ToString();
            };

            var oauther = DependencyService.Get<IQiitaOAuth>();

            auth.Completed += OnAuthenticationCompleted;
            oauther.StartOAuth(auth);
        }

        async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {

            }
        }
    }
}
