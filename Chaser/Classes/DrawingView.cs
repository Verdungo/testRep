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
using System.Threading;

namespace Chaser
{
    public class DrawingView : View
    {
        private List<Chaser> _chasers = new List<Chaser>();
        private Handler _h;
        private Thread _t; // TODO: List<Thread>
        private Point _epicenter;

        public DrawingView(Context context) : base(context)
        {
            var screenWidth = context.Resources.DisplayMetrics.WidthPixels;
            var screenHeight= context.Resources.DisplayMetrics.HeightPixels;

            _chasers.Add(new Chaser(new Point(screenWidth / 2, screenHeight / 2), Color.Red));
            _epicenter = new Point(screenWidth / 2, screenHeight / 2);

            _h = new Handler(new Action<Message>((Message msg) => 
            {
                Invalidate();
            }));

            _t = new Thread(new ThreadStart(MoveChaser));
            _t.Start();
        }

        public override void Draw(Canvas canvas)
        {
            foreach (Chaser chaser in _chasers)
            {
                chaser.Draw(canvas);
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            try
            {
                _epicenter = new Point(Convert.ToInt32(e.GetX(0)), Convert.ToInt32(e.GetY(0)));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        private void MoveChaser()
        {
            while (true)
            {
                for (int i = 0; i < _chasers.Count; i++)
                {
                    _chasers[i].DirectTo(_epicenter);
                    _h.SendEmptyMessage(0);
                }
                Thread.Sleep(10);
            }
        }
    }
}