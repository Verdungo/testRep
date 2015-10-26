using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Chaser
{
    public class Chaser : Paint
    {
        private const float MAX_VELOCITY = 40; // TODO: Должны зависеть от размера экрана
        private const float MIN_VELOCITY = 3; // TODO: Должны зависеть от размера экрана
        private const float MAX_VELOCITY_DIST = 500; // TODO: Должны зависеть от размера экрана
        private const float MIN_VELOCITY_DIST = 10; // TODO: Должны зависеть от размера экрана
        private const float DEADBAND = 200; //
        private const int CHASER_LENGHT = 20; //

        //private Point _position;
        private double _velocity;
        private double _direction;

        private List<Point> _cells = new List<Point>();

        public Chaser(Point origin, Color color) : base()
        {
            _velocity = 10;
            _direction = 0;

            _cells.Add(origin);
            Color = color;
        }

        internal void Draw(Canvas canvas)
        {
            canvas.DrawCircle(_cells.Last().X, _cells.Last().Y, 3, this);
            for (int p = _cells.Count - 1; p > 0; p--)
            {
                canvas.DrawLine(_cells[p].X, _cells[p].Y, _cells[p - 1].X, _cells[p - 1].Y, this);
            }
        }

        public void DirectTo(Point to)
        {
            var position = _cells.Last();
            // TODO: Не нравится мне знаменатель
            double alfa = (to.X != position.X) ? Math.Atan((double)(to.Y - position.Y) / (double)(to.X - position.X)) : 0 ;
            if (to.X < position.X) alfa += Math.PI;

            var distance = Math.Sqrt(Math.Pow(to.X - position.X, 2) + Math.Pow(to.Y - position.Y, 2));
            _velocity = MIN_VELOCITY + (MAX_VELOCITY - MIN_VELOCITY) * ((distance - MIN_VELOCITY_DIST) / (MAX_VELOCITY_DIST - MIN_VELOCITY_DIST));
            if (_velocity > MAX_VELOCITY) _velocity = MAX_VELOCITY;
            if (_velocity < MIN_VELOCITY) _velocity = MIN_VELOCITY;

            if (distance >= DEADBAND)
            {
                _direction = alfa; // TODO: добавить постепенное выравнивание траектории!
            }


            _cells.Add(new Point(position.X + Convert.ToInt32(_velocity * Math.Cos(_direction)), position.Y + Convert.ToInt32(_velocity * Math.Sin(_direction))));
            if (_cells.Count > CHASER_LENGHT) _cells.RemoveAt(0);

        }
    }
}