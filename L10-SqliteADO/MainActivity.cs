using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;

namespace L10_SqliteADO
{
    [Activity(Label = "L10_SqliteADO", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string _ConnectionString;
        List<Info> _Data;
        MyAdapter _Adapter;
        ListView _ListView;
        string _DBPath;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnCreate = FindViewById<Button>(Resource.Id.btnCreate);
            var btnLoad = FindViewById<Button>(Resource.Id.btnLoad);


            string dbName = "ch5_3_2.db";
            _DBPath = Path.Combine(FilesDir.Path, dbName);
            if (!File.Exists(_DBPath))
                SqliteConnection.CreateFile(_DBPath);//建立資料庫檔案

            //連線字串
            _ConnectionString =
                string.Format("Data Source={0};Version=3", _DBPath);

            _ListView = FindViewById<ListView>(Resource.Id.listView);

            btnCreate.Click +=
                (sender, e) =>
                {
                    ExecuteNonQuery
                        ("Create Table if not exists MyData(Name ntext, Age INTEGER )");
                    Toast.MakeText
                        (this, "執行建立資料表", ToastLength.Short).Show();
                };
            btnAdd.Click +=
                 (sender, e) =>
                 {
                     StartActivityForResult(typeof(AddActivity), 0);
                 };
            btnLoad.Click +=
                 (sender, e) =>
                 {
                     ExecuteData("Select * FROM MyData");
                     _Adapter = new MyAdapter(this, _Data);
                     _ListView.Adapter = _Adapter;
                     Toast.MakeText
                         (this, "重新載入資料", ToastLength.Short).Show();
                 };

        }

        protected override void OnActivityResult
            (int requestCode, Result resultCode, Intent data)
        {
            string name = data.GetStringExtra("Name");
            string age = data.GetStringExtra("Age");
            Info info = new Info() { Name = name, Age = age };
            ExecuteNonQuery(string.Format("Insert into MyData Values('{0}',{1})", name, age));
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

                _Data.Add(info);
                _Adapter.NotifyDataSetChanged();
            }

        }

        private void ExecuteData(string cmdText)
        {
            using (var conn = new SqliteConnection(_ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    using (var reader = cmd.ExecuteReader())
                    {
                        _Data = new List<Info>();
                        while (reader.Read())
                        {
                            _Data.Add(
                                new Info()
                                {
                                    Name = reader["Name"].ToString(),
                                    Age = reader["Age"].ToString()
                                });

                        }
                    }
                }
            }
        }

        private void ExecuteNonQuery(string cmdText)
        {
            int x = 0;

            using (var conn = new SqliteConnection(_ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
    public class Info
    {
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

