using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClockControl
{
    public partial class ClockControl : UserControl
    {
        DateTime dt;
        public ClockControl()
        {
            InitializeComponent();
            ResizeRedraw = true;
            Enabled = false;
        }

        public DateTime Time
        {
            get
            {
                return dt;
            }
            set
            {
                Graphics grfx = CreateGraphics();
                InitializeCoordinates(grfx);

                DrawMyName(grfx, new Pen(ForeColor));

                Pen pen = new Pen(BackColor);

                if (dt.Hour!=value.Hour)
                {
                    DrawHourHand(grfx, pen);
                }
                if (dt.Minute!=value.Minute)
                {
                    DrawHourHand(grfx, pen);
                    DrawMinuteHand(grfx, pen);
                }
                if (dt.Second!=value.Second)
                {
                    DrawMinuteHand(grfx, pen);
                    DrawSecondHand(grfx, pen);
                }
                if(dt.Millisecond!=value.Millisecond)
                {
                    DrawSecondHand(grfx, pen);
                }

                dt = value;
                pen = new Pen(ForeColor);

                DrawHourHand(grfx, pen);
                DrawMinuteHand(grfx, pen);
                DrawSecondHand(grfx, pen);

                grfx.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics grfx = e.Graphics;
            Pen pen = new Pen(ForeColor);
            Brush brush = new SolidBrush(ForeColor);

            InitializeCoordinates(grfx);
            DrawDots(grfx, brush);
            DrawHourHand(grfx, pen);
            DrawMinuteHand(grfx, pen);
            DrawSecondHand(grfx, pen);
        }

        void InitializeCoordinates(Graphics grfx)
        {
            if (Width == 0 || Height == 0)
                return;
            grfx.TranslateTransform(Width / 2, Height / 2);
            float fInches = Math.Min(Width / grfx.DpiX, Height / grfx.DpiY);

            float x = fInches * grfx.DpiX / 2000;
            float y = fInches * grfx.DpiY / 2000;
            grfx.ScaleTransform(fInches * grfx.DpiX / 2000, fInches * grfx.DpiY / 2000);
        }

        void DrawDots(Graphics grfx, Brush brush)
        {
            for (int i = 0; i < 60; i++)
            {
                int iSize; //= i % 5 == 0 ? 100 : 30;
                if (i%5==0)
                {
                    if (i%15==0)
                    {
                        iSize = 110;
                    }
                    else
                    {
                        iSize = 80;
                    }
                }                
                else
                {
                    iSize = 30;
                }
                grfx.FillEllipse(brush, 0 - iSize / 2, -900 - iSize / 2, iSize, iSize);
                grfx.RotateTransform(6);
            }
        }

        protected virtual void DrawHourHand(Graphics grfx, Pen pen)
        {
            GraphicsState gs = grfx.Save();
            grfx.RotateTransform(360f * Time.Hour / 12 + 30f * Time.Minute / 60);

            Point[] points = new Point[]
                {
                    new Point(0,150), new Point(100, 0),
                    new Point(0,-600),new Point(-100, 0)
                };

            grfx.DrawPolygon(pen, points);

           // grfx.FillPolygon(Brushes.AliceBlue, points);
            grfx.Restore(gs);
        }

        protected virtual  void DrawMinuteHand(Graphics grfx, Pen pen)
        {
            GraphicsState gs = grfx.Save();
            grfx.RotateTransform(360f * Time.Minute / 60 + 6f * Time.Second / 60);

            Point[] points = new Point[]
                {
                    new Point(0,200), new Point(50,0),
                    new Point(0, -800), new Point(-50, 0)
                };
            grfx.DrawPolygon(pen, points);
           // grfx.FillPolygon(Brushes.AliceBlue, points);

            grfx.Restore(gs);
        }

        protected virtual void DrawSecondHand(Graphics grfx, Pen pen)
        {
            GraphicsState gs = grfx.Save();
            grfx.RotateTransform(360f * Time.Second / 60 + 6f * Time.Millisecond / 1000);

            grfx.DrawLine(pen, 0, 0, 0, -800);
            grfx.Restore(gs);
        }

        void DrawMyName(Graphics grfx, Pen pen)
        {
             string s = string.Format("{0:D2}/{1:D2}/{2}", dt.Day, dt.Month, dt.Year);
          //  string s = "Fidel M.";
            Font font = new Font("Century Gothic", 110);
            grfx.DrawString(s, font, Brushes.Aqua, new Point(-360, 400));
            
        }                
    }
}
