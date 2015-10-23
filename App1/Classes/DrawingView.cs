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
using Android.Graphics;

namespace App1.Classes
{
    public class DrawingView : View
    {
        private List<CirclePaint> _touches;
        private TextView _tv;

        public DrawingView(Context context) : base(context)
        {
            //DEBUG: создали несколько объектов
            _touches = new List<CirclePaint>()
            {
                new CirclePaint(new Point(100, 100), 100, Color.Red),
                new CirclePaint(new Point(100, 300), 100, Color.Yellow),
                new CirclePaint(new Point(100, 500), 100, Color.Green)
            };
        }

        public override void Draw(Canvas canvas)
        {
            foreach (CirclePaint item in _touches)
            {
                item.Draw(canvas);
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            //TextView tv = FindViewById<TextView>(Resource.Id.textView1);
            Point TouchPlace = new Point((int)e.GetX(), (int)e.GetY());

            switch (e.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    foreach (var item in _touches)
                    {
                        item.CheckTouched(TouchPlace);
                    }
                    break;
                case MotionEventActions.Move:
                    foreach (var item in _touches)
                    {
                        if (item.Touched)
                        {
                            item.MoveTo(TouchPlace);
                            Invalidate();
                        }
                    }
                    break;
                case MotionEventActions.Up:
                    foreach (var item in _touches)
                    {
                        if (item.Touched)
                        {
                            item.MoveTo(TouchPlace);
                            item.Touched = false;
                        }
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}