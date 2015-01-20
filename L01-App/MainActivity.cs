using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace L01_App
{
    //於特性給予MainLauncher = true
    [Activity(Label = "L01_App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);//指定此Activity所顯示的View

            var app = this.Application as MyApp;
            Log.WriteLine(LogPriority.Info, "MainActivity", "======取得MyApp中的Value值為:" + app.Value + "======");
        }
    }
}

