using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace L04_Fragment
{
    [Activity(Label = "L04_Fragment", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
     
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            //以下為動態
            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.frameLayout1, new MyFrgFragment() { Value = "動態Fragment1" })
                .Commit();

            this.FindViewById<Button>(Resource.Id.button).Click += (sender, e) =>
            {
                FragmentManager.BeginTransaction()
                    .AddToBackStack(null)//將上一個 Fragment加進Back Stack
                    .Replace(Resource.Id.frameLayout1, new MyFrgFragment() { Value = "動態Fragment2" })
               .Commit();
            };

        }
    }
}

