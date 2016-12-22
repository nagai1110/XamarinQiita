using Qiita.API;
using Qiita.Setting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (!LoginUtil.IsLogined())
            {
                _items.Add(new MenuItem() { Text = "ログイン", Icon = "" });
            }
            else
            {
                QiitaUser user = LoginUtil.LoginUser();
                _items.Add(new MenuItem() { Text = user.ID, Icon = user.ProfileImageUrl });
                _items.Add(new MenuItem() { Text = "フォロー中のタグ", Icon = "" });
                _items.Add(new MenuItem() { Text = "ログアウト", Icon = "" });
            }
        }
    }
}
