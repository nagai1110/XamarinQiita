using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
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

            QiitaAPI api = new QiitaAPI();
            api.GetAllItems(
                items =>
                {
                    foreach (QiitaItem item in items)
                    {
                        Items.Add(item);
                    }
                },
                () =>
                {
                    int bbb = 0;
                });
        }

    }
}
