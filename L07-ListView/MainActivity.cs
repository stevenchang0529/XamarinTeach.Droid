using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace L07_ListView
{
    [Activity(Label = "L07_ListView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var listView = this.FindViewById<ListView>(Resource.Id.listView);

            
            string[] items=new string[]{"Row1","Row2","Row3","Row4","Row5","Row6","Row7","Row8"};

            //建立Adapter並給予資料來源，以及指定Layout
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);

            //使用自訂的Layout
            //ArrayAdapter adapter = new ArrayAdapter(this,Resource.Layout.RowItem,Resource.Id.txtRow, items);

            listView.Adapter = adapter;


            //註冊click事件
            listView.ItemClick += (sender, e) =>
            {
                int index = e.Position;//點擊的索引位置
                Toast.MakeText(this, items[index], ToastLength.Short).Show();
            };
        }
    }
}

