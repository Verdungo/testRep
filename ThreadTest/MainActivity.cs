using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ThreadTest.Classes;

namespace ThreadTest
{
    [Activity(Label = "ThreadTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            LinearLayout ll = new LinearLayout(this);
            TextView tv = new TextView(this);
            ll.AddView(tv);
            ll.AddView(new DrawingView(this, new TextView(this)));
            SetContentView(ll);
        }
    }
}

