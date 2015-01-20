
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Provider;
using Android.Net;
using Android.Database;

namespace L11_ImageCapture
{
    [Activity(Label = "L11_ImageCapture", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        string path;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            path = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "temp.data");
            button.Click += (sender, e) =>
            {

                Intent i = new Intent(Android.Provider.MediaStore.ActionImageCapture);

               // i.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(new Java.IO.File(path)));

                this.StartActivityForResult(i, 123);
            };

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            //直接取得Bitmap  影響解析度較差
            //var bitmap = data.Extras.Get("data") as Bitmap;
          

            //存檔後讀取，為原解析度
           // var buff = System.IO.File.ReadAllBytes(path);
            //var bitmap=BitmapFactory.DecodeByteArray(buff, 0, buff.Length);

            //從預設輸出的目錄將URI轉換成實體路徑
            var uri = data.Data;
            var path = GetPathToImage(uri);
            var buff = System.IO.File.ReadAllBytes(path);
            var bitmap=BitmapFactory.DecodeByteArray(buff, 0, buff.Length);

            this.FindViewById<ImageView>(Resource.Id.imageView).SetImageBitmap(bitmap);
            base.OnActivityResult(requestCode, resultCode, data);
        }


        private string GetPathToImage(Android.Net.Uri uri)
        {
            string path = null;
            string[] projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
            using (ICursor cursor = this.ManagedQuery(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }
            return path;
        }
    }
}

