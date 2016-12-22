using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Qiita.API.OAuth;
using Xamarin.Auth;
using Qiita.Setting;
using Qiita.Util;

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
            if (menu.Text.Equals("ログイン"))
            {
                LoginUtil.Login(
                    (user, token) =>
                    {
                        masterPage.UpdateMenu();
                    },
                    () =>
                    {

                    },
                    (ex, msg) =>
                    {

                    }
               );
            }
            else if (menu.Text.Equals("ログアウト"))
            {
                LoginUtil.Logout();
                masterPage.UpdateMenu();
            }

            IsPresented = false;
        }
    }
}
