using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace L12_QRCode
{
    [Activity(Label = "L12_QRCode", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            this.FindViewById<Button>(Resource.Id.btnScanditSDK).Click += (sender, e) =>
            {
                this.StartActivity(typeof(ScanActivity));
            };

            this.FindViewById<Button>(Resource.Id.btnZXing).Click +=async (sender, e) =>
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner(this);
                var result = await scanner.Scan();

                if (result != null)
                    Toast.MakeText(this, result.Text, ToastLength.Short).Show();
            };

          
        }
    }
}

