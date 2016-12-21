using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Qiita.API.OAuth;
using Xamarin.Auth;
using Qiita.Setting;

namespace Qiita.Page
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();

            masterPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);
        }

        private void NavigateTo(MenuItem menu)
        {
            if (menu.Name.Equals("ログイン"))
            {
                var auth = new QiitaAuthenticator(
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
            else if (menu.Name.Equals("ログアウト"))
            {
                PropertiesAccesser.Remove(PropertiesKey.LoginUserToken); 
            }

            IsPresented = false;
        }

        private void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                Application.Current.Properties[PropertiesKey.LoginUserName] = e.Account.Username;
                Application.Current.Properties[PropertiesKey.LoginUserToken] = e.Account.Properties["token"];
            }
        }
    }
}
