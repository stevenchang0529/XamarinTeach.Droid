using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using L05_WebService.net.azurewebsites.testmyws;
using System.Threading.Tasks;
namespace L05_WebService
{
    [Activity(Label = "L05_WebService", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        BasicHttpBinding_IService1 service;
        protected  override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //建立WebService Proxy類別
             service = new BasicHttpBinding_IService1();
           
            this.FindViewById<Button>(Resource.Id.button).Click +=async (sender, e) =>
            {
                var result = service.GetData(999, true);//呼叫WebService方法
                this.FindViewById<TextView>(Resource.Id.textView).Text = result;

                //var result=await this.GetData(999);
                //this.FindViewById<TextView>(Resource.Id.textView).Text = result;
            };
        }

        //轉換成TAP方式
        private Task<string> GetData(int value)
        {
            TaskCompletionSource<string> task = new TaskCompletionSource<string>();
            service.GetDataCompleted += (sender, e) =>
            {
                task.SetResult(e.Result);
            };
            service.GetDataAsync(value, true);
            return task.Task;
        }
    }


}

