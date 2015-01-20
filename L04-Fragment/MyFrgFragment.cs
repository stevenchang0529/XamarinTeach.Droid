using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace L04_Fragment
{
    public class MyFrgFragment : Fragment
    {
        public string Value { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view= inflater.Inflate(Resource.Layout.MyFrg, null);
            if (Value != null)
            {
                view.FindViewById<TextView>(Resource.Id.textView).Text = Value;
            }
            return view;
        }
    }
}