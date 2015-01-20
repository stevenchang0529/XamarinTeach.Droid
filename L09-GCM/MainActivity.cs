using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System.Linq;
namespace L09_GCM
{
    [Activity(Label = "L09_GCM", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += async delegate {

                if (this.DoesContainGsfPackage())
                {
                    var app = this.Application as MyApp;
                    var id = await app.GetRegId();
                    this.FindViewById<TextView>(Resource.Id.textView).Text = id;
                }
            };
        }

        //檢查有無com.google.android.gsf
        public  bool DoesContainGsfPackage()
        {
            PackageManager pm = this.PackageManager;
            var list = pm.GetInstalledPackages(PackageInfoFlags.Activities);
            return list.Any(c => c.PackageName == "com.google.android.gsf");
        }
    }
}

