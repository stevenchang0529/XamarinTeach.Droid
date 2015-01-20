using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;

namespace L08_CustomListView
{
    [Activity(Label = "L08_CustomListView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var listView = this.FindViewById<ListView>(Resource.Id.listView);

            List<string> items = new List<string>();
            for (int i = 1; i <= 50; i++)
                items.Add(i.ToString());

            MyAdapter adapter = new MyAdapter()
            {
                Context=this,
                Items = items
            };

            listView.Adapter = adapter;
        }
    }

    public class MyAdapter : BaseAdapter<string>
    {
        public List<string> Items { get; set; }

        public Activity Context { get; set; }

        public override string this[int position]
        {
            get { return Items[position]; }
        }

        public override int Count
        {
            get { return Items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Holder holder = null;
            if (convertView == null)//NULL代表VIEW未建立
            {
                convertView = Context.LayoutInflater.Inflate(Resource.Layout.RowItem, null);//建立VIEW
                convertView.FindViewById<Button>(Resource.Id.button1).Click += (sender, e) =>//第一次建立的VIEW要註冊按鈕事件
                {
                    Toast.MakeText(this.Context, Items[holder.Index], ToastLength.Short).Show();
                };
                holder = new Holder();//建立存放在View中的物件
                convertView.Tag = holder;
            }
            else
                holder = convertView.Tag as Holder;//從目前的VIEW中取得holder

            holder.Index = position;//給予Holder目前的索引值
            convertView.FindViewById<TextView>(Resource.Id.textView1).Text = Items[position];//給予目前VIEW對應的索引資料
            return convertView;
        }

        public class Holder : Java.Lang.Object
        {
            public int Index { get; set; }
        }
    }
}

