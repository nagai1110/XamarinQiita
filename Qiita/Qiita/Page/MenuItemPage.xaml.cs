using Qiita.API;
using Qiita.Setting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Newtonsoft.Json;
using Xamarin.Forms;
using Qiita.Util;

namespace Qiita.Page
{
    public partial class MenuItemPage : ContentPage
    {
        private ObservableCollection<MenuItem> _items = new ObservableCollection<MenuItem>();

        public ListView Menu { get { return ItemListView; } }

        public MenuItemPage()
        {
            InitializeComponent();

            ItemListView.ItemsSource = _items;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateMenu();
        }

        public void UpdateMenu()
        {
            _items.Clear();

            bool isLogined = LoginUtil.IsLogined();
            UpdateUser(isLogined);
            UpdateItems(isLogined);
        }

        private void UpdateItems(bool isLogined)
        {
            if (isLogined)
            {
                _items.Add(new MenuItem() { Text = "フォロー中のタグ", Icon = "" });
                _items.Add(new MenuItem() { Text = "ログアウト", Icon = "login.png" });
            }
            else
            {
                _items.Add(new MenuItem() { Text = "ログイン", Icon = "login.png" });
            }
        }

        private void UpdateUser(bool isLogined)
        {
            UserName.IsVisible = isLogined;
            UserIcon.IsVisible = isLogined;

            if (isLogined)
            {
                QiitaUser user = LoginUtil.LoginUser();
                UserName.Text = user.ID;
                UserIcon.Source = user.ProfileImageUrl;
            }            
        }
    }
}
