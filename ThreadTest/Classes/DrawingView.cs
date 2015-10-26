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

namespace ThreadTest.Classes
{
    public class DrawingView : View
    {
        private Ball _ball = new Ball(new Point(300, 300), 50, Color.Red);
        private Handler _h;
        Thread t;
        private TextView _tvDebug;

        public DrawingView(Context context, TextView tv) : base(context)
        {
            _tvDebug = tv;
            
            _h = new Handler(new Action<Message>((Message msg) => 
            {
                _tvDebug.Text = msg.What.ToString();
                _ball.Center.Y = msg.What;
                Invalidate();
            }));
            t = new Thread(new ThreadStart(MoveBall));
            t.Start();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            t.Suspend();
            
            return true;
        }

        public override void Draw(Canvas canvas)
        {
            // перерисовать объект
            _ball.Draw(canvas);
        }

        private void MoveBall()
        {
            int y = 300, direction = 10;

            while (true)
            {
                if (y >= 500 || y <= 100)
                {
                    direction = -direction;
                }

                y += direction;
                _h.SendEmptyMessage(y);

                Thread.Sleep(30);
            }
        }
    }
}