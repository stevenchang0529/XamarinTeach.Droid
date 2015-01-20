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
    [Activity(Label = "ResultActivity")]
    public class ResultActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Result);
            this.FindViewById<Button>(Resource.Id.button).Click += (sender, e) =>
            {
                //將結果放入Intent
                Intent i = new Intent();
                i.PutExtra("value", this.FindViewById<TextView>(Resource.Id.editText).Text);

                //將Intent傳入SetResult並關閉Activity
                this.SetResult(Result.Ok, i);
                this.Finish();
            };
        }
    }
}