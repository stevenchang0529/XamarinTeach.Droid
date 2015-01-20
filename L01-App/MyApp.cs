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
using Android.Util;

namespace L01_App
{
    [Application]
    public  class MyApp:Application
    {
        public string Value { get; set; }

        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            Log.WriteLine(LogPriority.Info, "MyApp", "======執行MyApp建構子======");
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Log.WriteLine(LogPriority.Info, "MyApp", "======執行MyApp OnCreate======");
            this.Value = "Set By MyApp";
        }


    }
}