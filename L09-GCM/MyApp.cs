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
using System.Threading.Tasks;

namespace L09_GCM
{
    [Application]
    public  class MyApp:Application
    {
        public event EventHandler<string> OnGetRegistrationId;

        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public Task<string> GetRegId()
        {
            var task = new TaskCompletionSource<string>();
            this.OnGetRegistrationId += (sender, e) =>
            {
                task.TrySetResult(e);
            };
            this.SetPush();
            return task.Task;
        }


        private void SetPush()
        {
            string senders = "31490604079";//放置你的API 專案碼
            Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
            intent.SetPackage("com.google.android.gsf");
            intent.PutExtra("app", PendingIntent.GetBroadcast(this, 0, new Intent(), 0));
            intent.PutExtra("sender", senders);
            this.StartService(intent);
        }

        public void SetID(string id)
        {
            if (OnGetRegistrationId != null)
                OnGetRegistrationId(this, id);
        }


    }
}