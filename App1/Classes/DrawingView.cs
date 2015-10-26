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

        public DrawingView(Context context) : base(context)
        {
            //DEBUG: создали несколько объектов
            _touches = new List<CirclePaint>()
            {
                new CirclePaint(new Point(360, 200), 75, Color.Red),
                new CirclePaint(new Point(360, 500), 75, Color.Yellow),
                new CirclePaint(new Point(360, 800), 75, Color.Green)
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
            int touchesCount = e.PointerCount;
            List<Point> TouchPlace = new List<Point>();

            for (int i = 0; i < touchesCount; i++)
            {
                TouchPlace.Add(new Point((int)e.GetX(i), (int)e.GetY(i)));
            }

            switch (e.ActionMasked)
            {
                case MotionEventActions.PointerDown:
                case MotionEventActions.Down:
                    foreach (Point touch in TouchPlace)
                    {
                        foreach (CirclePaint circle in _touches)
                        {
                            if (circle.IsTouched(touch))
                            {
                                circle.Touched = TouchPlace.IndexOf(touch);
                            }
                        }
                    }
                    break;
                case MotionEventActions.Move:
                    foreach (CirclePaint circle in _touches)
                    {
                        if (circle.Touched >= 0)
                        {
                            circle.MoveTo(TouchPlace[e.FindPointerIndex(circle.Touched)]);
                            Invalidate();
                        }
                    }
                    break;
                case MotionEventActions.PointerUp:
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    foreach (CirclePaint circle in _touches)
                    {
                        if (circle.Touched == e.GetPointerId(e.ActionIndex))
                        {
                            circle.Touched = -1;
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