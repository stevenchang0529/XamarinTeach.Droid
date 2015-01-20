using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Collections.Generic;
using SQLite;
using System.Linq;
namespace L10_SqliteNet
{
    [Activity(Label = " L10_SqliteNet", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        List<Info> _Data;
        MyAdapter _Adapter;
        ListView _ListView;
        SQLiteConnection _Conn;
        string _DBPath;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnQuery = FindViewById<Button>(Resource.Id.btnQuery);
            var input = FindViewById<EditText>(Resource.Id.input);
            _ListView = FindViewById<ListView>(Resource.Id.listView);

            string dbName = "L10-SqliteNet.db";
            _DBPath = Path.Combine(FilesDir.Path, dbName);
            _Conn = new SQLiteConnection(_DBPath);
            _Conn.CreateTable<Info>();
            _Conn.DeleteAll<Info>();
            List<Info> items = new List<Info>();
            items.Add(new Info() { Age = "12", Name = "Eco" });
            items.Add(new Info() { Age = "13", Name = "Lydia" });
            items.Add(new Info() { Age = "14", Name = "Steven" });
            items.Add(new Info() { Age = "15", Name = "Ben" });
            items.Add(new Info() { Age = "17", Name = "Terry" });
            items.Add(new Info() { Age = "18", Name = "KFC" });
            items.Add(new Info() { Age = "92", Name = "Moss" });
            items.Add(new Info() { Age = "53", Name = "Toyota" });
            items.Add(new Info() { Age = "122", Name = "BMW" });
            items.Add(new Info() { Age = "12", Name = "Zent" });
            _Conn.InsertAll(items);

            btnAdd.Click +=
                 (sender, e) =>
                 {
                     StartActivityForResult(typeof(AddActivity), 0);
                 };
            btnQuery.Click +=
                 (sender, e) =>
                 {
                     // ExecuteData("Select * FROM MyData");
                     var query=_Conn.Table<Info>();
                     if (input.Text != string.Empty)
                         query=query.Where(c => c.Name == input.Text);
                     _Data = query.ToList();
      
                     _Adapter = new MyAdapter(this, _Data);
                     _ListView.Adapter = _Adapter;
                     Toast.MakeText
                         (this, "查詢完成", ToastLength.Short).Show();
                 };

        }

        protected override void OnActivityResult
            (int requestCode, Result resultCode, Intent data)
        {
            string name = data.GetStringExtra("Name");
            string age = data.GetStringExtra("Age");
            Info info = new Info() { Name = name, Age = age };
            //ExecuteNonQuery(string.Format("Insert into MyData Values('{0}',{1})", name, age));
            bool isExist = _Conn.Table<Info>().Any(c => c.Name == name);
            if (isExist)
                Toast.MakeText
                         (this, "名字已存在", ToastLength.Short).Show();
            else
            {
                _Conn.Insert(info);
                _Data.Add(info);
            }
            if (_Adapter == null)
            {
                _Data = new List<Info>() { info };
                _Adapter = new MyAdapter(
                    this,
                    _Data);
                _ListView.Adapter = _Adapter;
            }
            else
            {
                _Adapter.NotifyDataSetChanged();
            }

        }

        
    }
    public class Info
    {
        [SQLite.PrimaryKey]
        public string Name { get; set; }
        public string Age { get; set; }
    }
    public class MyAdapter : BaseAdapter<Info>
    {
        List<Info> _Data;
        Activity _Context;

        public MyAdapter(Activity context, List<Info> data)
        {
            _Data = data;
            _Context = context;
        }

        public override int Count
        {
            get { return _Data.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Info this[int position]
        {
            get { return _Data[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = _Context.LayoutInflater.Inflate(Resource.Layout.Item, null);
            view.FindViewById<TextView>(Resource.Id.txtName).Text = _Data[position].Name;
            view.FindViewById<TextView>(Resource.Id.txtAge).Text = _Data[position].Age + "歲";
            return view;
        }

    }
}

