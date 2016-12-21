using Qiita.Setting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            _items.Clear();

            // TODO:ログインしているかどうかの判定はどこかで共通化する
            if (PropertiesAccesser.Get(PropertiesKey.LoginUserToken) == null)
            {
                _items.Add(new MenuItem("ログイン", ""));
            }
            else
            {
                _items.Add(new MenuItem("フォロー中のタグ", ""));
                _items.Add(new MenuItem("ログアウト", ""));
            }
            
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        // TODO:仮でstring
        public string Icon { get; set; }

        public MenuItem(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }
    }
}
