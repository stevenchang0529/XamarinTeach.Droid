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

namespace L10_SqliteADO
{
    [Activity(Label = "AddActivity")]
    public class AddActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Add);
            var name=FindViewById<EditText>(Resource.Id.inputName);
            var age = FindViewById<EditText>(Resource.Id.inputAge);
            FindViewById<Button>(Resource.Id.btnOK)
                .Click += (sender, e) =>
                {
                    Intent i = new Intent();
                    i.PutExtra("Name", name.Text);
                    i.PutExtra("Age", age.Text);
                    SetResult(Result.Ok, i);
                    this.Finish();
                };
        }
    }
}