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
using Qiita.Setting;
using Qiita.Share;

namespace Qiita.Page
{
	public partial class ItemListPage : ContentPage
	{
        private ObservableCollection<QiitaItem> _items = new ObservableCollection<QiitaItem>();

        public ItemListPage ()
		{
			InitializeComponent ();

            ItemListView.ItemsSource = _items;
            ItemListView.ItemSelected += (sender, e) => OnItemSelected(e.SelectedItem as QiitaItem);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateItems();
        }

        private void UpdateItems()
        {
            _items.Clear();

            var api = new QiitaAPI();
            var token = PropertiesAccesser.Get(PropertiesKey.LoginUserToken);
            if (token != null) api.AccessToken = token as string;

            api.GetAllItems(
                items =>
                {
                    foreach (QiitaItem item in items)
                    {
                        _items.Add(item);
                    }
                },
                () =>
                {

                });
        }

        private async void OnItemSelected(QiitaItem item)
        {
            //DisplayActionSheetの表示
            var result = await DisplayActionSheet("選択", "キャンセル", null, "開く", "共有");
            if (result == "開く")
            {
                Navigation.PushAsync(new WebViewPage(item.Url));
            }
            else if (result == "共有")
            {
                Share.Share.ShareUrlAsync(item.Url, "", "");
            } 
        }
    }
}
