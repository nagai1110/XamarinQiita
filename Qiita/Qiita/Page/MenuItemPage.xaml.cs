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

            // TODO:ログインしているかどうかの判定はどこかで共通化する
            if (PropertiesAccesser.Get(PropertiesKey.LoginUserToken) == null)
            {
                _items.Add(new MenuItem() { Text = "ログイン", Icon = "" });
            }
            else
            {
                var userObj = PropertiesAccesser.Get(PropertiesKey.LoginUser);
                if (userObj != null)
                {
                    var user = JsonConvert.DeserializeObject<QiitaUser>(userObj as string);
                    _items.Add(new MenuItem() { Text = user.ID, Icon = user.ProfileImageUrl });
                }

                _items.Add(new MenuItem() { Text = "フォロー中のタグ", Icon = "" });
                _items.Add(new MenuItem() { Text = "ログアウト", Icon = "" });
            }
        }
    }
}
