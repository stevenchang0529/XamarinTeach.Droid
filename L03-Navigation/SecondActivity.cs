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
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Second);
            var textView = FindViewById<TextView>(Resource.Id.textView);
            textView.Text = "SecondActivity";

            this.FindViewById<Button>(Resource.Id.button1).Click += (sender, e) =>
            {
                Intent i = new Intent(this, typeof(MainActivity));
                //啟動Main，會重新啟動Activity，並清除在其之上的Activity。
               // i.SetFlags(ActivityFlags.ClearTop);

                //啟動Main，不會重新啟動Activity，並清除在其之上的Activity。
                i.SetFlags(ActivityFlags.ClearTop| ActivityFlags.SingleTop);
                this.StartActivity(i);
            };
        }
    }
}