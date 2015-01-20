using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace L03_Navigation
{
    [Activity(Label = "L03_Navigation", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //啟動下一個Activity
            this.FindViewById<Button>(Resource.Id.button1).Click += (sender, e) =>
            {
                this.StartActivity(typeof(FirstActivity));
            };

            this.FindViewById<Button>(Resource.Id.button2).Click += (sender, e) =>
            {
                Intent i = new Intent(this, typeof(FirstActivity));

                //將參數放進Intent
                //i.PutExtra("value", "我是參數");

                this.StartActivity(i);
            };

            this.FindViewById<Button>(Resource.Id.button3).Click += (sender, e) =>
            {
                Intent i = new Intent(this, typeof(FirstActivity));

                i.SetFlags(ActivityFlags.NoHistory);//設置flag讓下一個Activity不存進Task

                this.StartActivity(i);
            };

            this.FindViewById<Button>(Resource.Id.button4).Click += (sender, e) =>
            {
                
                this.StartActivityForResult(typeof(ResultActivity),999);
            };
        }

        //取得下一個Activity回傳的結果
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 999 && resultCode == Result.Ok)
            {
                this.FindViewById<TextView>(Resource.Id.textView).Text = data.GetStringExtra("value");
            }
        }
    }
}

