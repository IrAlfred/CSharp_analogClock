using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClockControl
{
    public partial class AnalogClock : Form
    {
        ClockControl clkctl;
        public AnalogClock()
        {
            InitializeComponent();

            Text = "Analog Clock";
            BackColor = SystemColors.Window;
            ForeColor = SystemColors.WindowText;
            clkctl = new ClockControl();
            clkctl.Parent = this;
            clkctl.Time = DateTime.Now;
            clkctl.Dock = DockStyle.Fill;
            clkctl.BackColor = Color.Black;
            clkctl.ForeColor = Color.White;

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_OnTick);
            timer.Start();
        }

        private void timer_OnTick(object sender, EventArgs e)
        {
            // clkctl.Time = DateTime.Now;

            DateTime dt = DateTime.Now;
            dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            clkctl.Time = dt;
        }
    }
}
