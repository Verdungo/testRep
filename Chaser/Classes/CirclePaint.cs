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

namespace Chaser
{
    public class CirclePaint : Paint
    {
        public Point CircleCenter { get; set; }

        public int CircleRadius { get; set; }

        public int Touched { get; set; }

        public Point Delta { get; set; }

        //public Color CircleColor { get; set; }

        //TODO: организовать наложения один поверх другого- Priority?

        public CirclePaint() : base()
        {
            
        }

        public CirclePaint(Point center, int radius, Color color) : base()
        {
            CircleCenter = center;
            CircleRadius = radius;
            Color = color;
            Touched = -1;
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawCircle(CircleCenter.X, CircleCenter.Y, CircleRadius, this);
        }

        public bool IsTouched(Point touch)
        {
            bool res;

            if (Math.Pow(Convert.ToDouble(CircleCenter.X - touch.X), 2) + Math.Pow(Convert.ToDouble(CircleCenter.Y - touch.Y), 2) < Math.Pow(CircleRadius, 2))
            {
                Delta = new Point(touch.X - CircleCenter.X, touch.Y - CircleCenter.Y);
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

        public bool CheckTouched(Point touchPlace)
        {
            if (Math.Pow(Convert.ToDouble(CircleCenter.X - touchPlace.X), 2) + Math.Pow(Convert.ToDouble(CircleCenter.Y - touchPlace.Y), 2) < Math.Pow(CircleRadius, 2))
            {
                Delta = new Point(touchPlace.X - CircleCenter.X, touchPlace.Y - CircleCenter.Y);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveTo(Point touchPlace)
        {
            CircleCenter = new Point(touchPlace.X - Delta.X, touchPlace.Y - Delta.Y);
        }
    }
}