using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;

namespace L06_WebAPI_Json
{
    [Activity(Label = "L06_WebAPI_Json", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {


        protected override void OnCreate(Bundle bundle)
        {
            //http://graph.facebook.com/stevenzhang

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            string api = "http://testmyapi.azurewebsites.net/api/values/";
            this.FindViewById<Button>(Resource.Id.MyButton).Click +=async (sender, e) =>
            {
                WebClient request = new WebClient();
                var result=await request.DownloadStringTaskAsync(new Uri(api));
                this.FindViewById<TextView>(Resource.Id.textView).Text = result;
                var obj=Newtonsoft.Json.JsonConvert.DeserializeObject<MyClass>(result);

                this.FindViewById<EditText>(Resource.Id.txtAge).Text = obj.Age.ToString();
                this.FindViewById<EditText>(Resource.Id.txtName).Text = obj.Name;
                this.FindViewById<EditText>(Resource.Id.txtWork).Text = obj.Work;
            };
        }
    }

    //http://json2csharp.com/
    public class MyClass
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Work { get; set; }
    }
}

