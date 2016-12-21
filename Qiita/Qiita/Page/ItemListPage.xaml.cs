using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Auth;
using Qiita.API;
using Qiita.API.OAuth;

namespace Qiita.Page
{
	public partial class ItemListPage : ContentPage
	{
        private ObservableCollection<QiitaItem> _items = new ObservableCollection<QiitaItem>();

        public ItemListPage ()
		{
			InitializeComponent ();

            ItemListView.ItemsSource = _items;
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

            var auth = new QiitaAuthenticator (
                "cdd6590e0e9bc747e989e91720f98e00bbfa7b7d",
                "a5ede5f7bd83c32360848c9466f9d1937eff20a7",
                "read_qiita",
                new Uri("https://qiita.com/api/v2/oauth/authorize"),
                new Uri("http://qiita.com/nagasakulllo"),
                new Uri("https://qiita.com/api/v2/access_tokens"));

            auth.AllowCancel = true;
            auth.Completed += OnAuthenticationCompleted;

            auth.StartAuth();
        }

        void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {

            }
        }
    }
}
