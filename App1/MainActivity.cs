using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using App1.Classes;
using System;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            //SetContentView(new DrawingView(this));
            SetContentView(Resource.Layout.Main);

            LinearLayout MainLayout = FindViewById<LinearLayout>(Resource.Id.MainLayout);
            MainLayout.AddView(new DrawingView(this));

        }       
    }
}

