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

namespace ThreadTest.Classes
{
    public class Ball : Paint
    {
        public Point Center { get; set; }

        public int Radius { get; set; }

        public Ball(Point center, int radius, Color color) : base()
        {
            Center = center;
            Radius = radius;
            Color = color;
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawCircle(Center.X, Center.Y, Radius, this);
        }
    }
}