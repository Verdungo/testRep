using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        LinearLayout MainLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            MainLayout = FindViewById<LinearLayout>(Resource.Id.MainLayout);
            MainLayout.AddView(new MyCircle(this));


        }

        class MyCircle : View
        {
            Paint circle;
            float cx = 500, cy = 1000, cr = 200;
            bool touchFlag = false;
            float touchX = 0, touchY = 0;

            public MyCircle(Context context): base(context)
            {
                circle = new Paint();
                circle.Color = Color.Red;
            }

            public override void Draw(Canvas canvas)
            {
                //base.Draw(canvas);
                canvas.DrawCircle(cx, cy, cr, circle);
            }

            public override bool OnTouchEvent(MotionEvent e)
            {
                float x = e.GetX();
                float y = e.GetY();

                //Toast.MakeText(Context, e.Action.ToString(), ToastLength.Short).Show();
                switch (e.Action)
                {
                    case MotionEventActions.Down:
                        if (x < cx + cr && x > cx - cr && y < cy + cr && y > cy - cr)
                        {
                            touchFlag = true;
                            touchX = x - cx;
                            touchY = y - cy;
                        }
                        break;
                    case MotionEventActions.Move:
                        if (touchFlag)
                        {
                            cx = x - touchX;
                            cy = y - touchY;

                            // ???????????????????????????????????????????????????????????? base ?
                            Invalidate();
                        }
                        break;
                    case MotionEventActions.Up:
                        touchFlag = false;
                        break;
                    default:
                        break;
                }

                return true;// base.OnTouchEvent(e);
            }
        }

    }
}

