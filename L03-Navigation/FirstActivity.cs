using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace L03_Navigation
{
    [Activity(Label = "FirstActivity")]
    public class FirstActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.First);
            var textView = FindViewById<TextView>(Resource.Id.textView);

            //取得由上一個Activity傳來的參數
            textView.Text = this.Intent.GetStringExtra("value")??"FirstActivity";


            this.FindViewById<Button>(Resource.Id.button1).Click += (sender, e) =>
            {
                this.StartActivity(typeof(SecondActivity));
            };
            
        }
    }
}