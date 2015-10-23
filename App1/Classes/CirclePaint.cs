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
    public class CirclePaint : Paint
    {
        public Point CircleCenter { get; set; }

        public int CircleRadius { get; set; }

        public bool Touched { get; set; }

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
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawCircle(CircleCenter.X, CircleCenter.Y, CircleRadius, this);
        }

        public bool IsTouched(Point touch)
        {
            bool res;

            // TODO: проверить касание в круг
            if (Math.Pow(Convert.ToDouble(CircleCenter.X - touch.X), 2) + Math.Pow(Convert.ToDouble(CircleCenter.Y - touch.Y), 2) < Math.Pow(CircleRadius, 2))
            /*if (Center.X - Radius < touch.X && touch.X > Center.X + Radius &&
                    Center.Y - Radius < touch.Y && touch.Y > Center.Y + Radius)*/
                    // ^^ тут было касание в квадрат, если что
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

        public void CheckTouched(Point touchPlace)
        {
            if (Math.Pow(Convert.ToDouble(CircleCenter.X - touchPlace.X), 2) + Math.Pow(Convert.ToDouble(CircleCenter.Y - touchPlace.Y), 2) < Math.Pow(CircleRadius, 2))
            {
                Touched = true;
                Delta = new Point(touchPlace.X - CircleCenter.X, touchPlace.Y - CircleCenter.Y);
            }
        }

        public void MoveTo(Point touchPlace)
        {
            CircleCenter = new Point(touchPlace.X - Delta.X, touchPlace.Y - Delta.Y);
        }
    }
}