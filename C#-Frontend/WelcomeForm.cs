using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentMentorApp
{
    public partial class WelcomeForm : Form
    {
        private Timer timerFade;
        bool fadingOut = false;

        public WelcomeForm()
        {
            InitializeComponent();

            // Form basics
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Opacity = 1.0;

            // Create and configure the timer in code
            timerFade = new Timer();
            timerFade.Interval = 30;              // 30 ms tick for smooth fade
            timerFade.Tick += TimerFade_Tick;

            // Ensure btnStart exists in your Designer and wire its click if not wired
            btnStart.Click -= btnStart_Click;     // safe remove (in case already wired)
            btnStart.Click += btnStart_Click;

            // Simple hover/floating visual (1st-year friendly)
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 1;
            btnStart.BackColor = Color.Aqua;
            btnStart.MouseEnter += (s, e) => btnStart.BackColor = Color.LightGray;
            btnStart.MouseLeave += (s, e) => btnStart.BackColor = Color.Aqua;

        }

        private void TimerFade_Tick(object sender, EventArgs e)
        {
            if (fadingOut)
            {
                if (this.Opacity > 0.05)
                {
                    this.Opacity -= 0.05;
                }
                else
                {
                    timerFade.Stop();
                    //Open MentorForm and hide WelcomeForm
                    MentorForm mentor = new MentorForm();
                    mentor.Show();
                    this.Hide();
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // disable button immediately and begin fade-out
            btnStart.Enabled = false;
            fadingOut = true;
            timerFade.Start();
        }
    }
}
