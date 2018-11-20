using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroTimer
{
    public partial class Form1 : Form
    {

        enum TimerState
        {
            Wait,
            Working,
            Relax
        }
        private TimerState AppState = TimerState.Wait;

        private int WorkingTimeLimitSec =  25*60;   //25分;
        private int RelaxTimerLimitSec =  5*60;     //5分;

        private Stopwatch stopwatch;
        private int LimitTime;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ストップウォッチを初期化
            this.stopwatch = new Stopwatch();
            this.stopwatch.Stop();


            this.Width = 220;
            this.Height = 200;

            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.WindowState = FormWindowState.Normal;

        }


        private void timer_every1s_Tick(object sender, EventArgs e)
        {

            switch (AppState)
            {
                case TimerState.Wait:
                    this.button_start.Visible = true;
                    this.button_start.Enabled = true;
                    this.label_lasttime.Visible = false;
                    this.LimitTime = WorkingTimeLimitSec;
                    this.label_message.Text = "App Waiting. Please Start..";
                    // 色
                    Color fore_color = Color.Gray;
                    this.label_message.BackColor = Color.Transparent;
                    this.label_lasttime.BackColor = Color.Transparent;
                    this.button_start.BackColor = Color.Transparent;
                    this.label_message.ForeColor = fore_color;
                    this.label_lasttime.ForeColor = fore_color;
                    this.button_start.ForeColor = fore_color;

                    this.BackColor = Color.Snow;
                    this.ForeColor = fore_color;

                    // 画面位置
                    this.TopMost = false;

                    if(this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;

                    this.FormBorderStyle = FormBorderStyle.FixedSingle;


                    break;

                case TimerState.Working:
                    this.LimitTime = WorkingTimeLimitSec;
                    this.button_start.Visible = false;
                    this.label_lasttime.Visible = true;

                    this.label_message.Text = "Working..";

                    // 色
                    this.BackColor = Color.Black;
                    fore_color = Color.Crimson;
                    this.label_message.ForeColor = fore_color;
                    this.label_lasttime.ForeColor = fore_color;
                    this.button_start.ForeColor = fore_color;

                    // 画面位置
                    this.TopMost = false;
                    //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

                    break;

                case TimerState.Relax:
                    this.LimitTime = RelaxTimerLimitSec;
                    this.button_start.Visible = false;
                    this.label_lasttime.Visible = true;
                    this.label_message.Text = "Relax";

                    // 色
                    this.BackColor = Color.LightSkyBlue;
                    fore_color = Color.Pink;
                    this.label_message.ForeColor = fore_color;
                    this.label_lasttime.ForeColor = fore_color;
                    this.button_start.ForeColor = fore_color;

                    // 画面位置
                    this.TopMost = true;
                        this.WindowState = FormWindowState.Maximized;
                    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    break;

                default:
                    break;
            }

            // **
            // 残り時間計測
            // **
            TimeSpan elapsed = this.stopwatch.Elapsed;
            TimeSpan timeLimit = new TimeSpan(0, 0, LimitTime);
            TimeSpan lastTime = timeLimit - elapsed;

            // **
            // 状態遷移の判定
            // **
            if (timeLimit.Ticks - elapsed.Ticks < 0)
            {
                // 残り時間が0になりました
                switch (this.AppState)
                {
                    case TimerState.Wait:
                        break;
                    case TimerState.Working:
                        // 最前面
                        // 最大画面にする
                        this.AppState = TimerState.Relax;
                        this.stopwatch.Restart();
                        break;
                    case TimerState.Relax:
                        // 画面を通常サイズに戻す
                        // 最前面をやめる
                        this.AppState = TimerState.Wait;
                        this.stopwatch.Reset();
                        break;
                    default:
                        break;
                }
            }

            // 経過時間を表示
            //this.label_lasttime.Text = elapsed.ToString(@"mm\:ss");
            // 残り時間 表示
            this.label_lasttime.Text = lastTime.ToString(@"mm\:ss");

        }

        private void button_start_Click(object sender, EventArgs e)
        {
            switch (this.AppState)
            {
                case TimerState.Wait:
                    // タイマースタート
                    this.AppState = TimerState.Working; // 状態を遷移
                    this.stopwatch.Restart();
                    this.button_start.Enabled = false; // ボタンを使用不可に
                    break;

                default:
                    break;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            this.label_lasttime.Margin = new Padding(0);
            this.label_lasttime.Left = (int)Math.Ceiling((this.Width - this.label_lasttime.Width) / 2.2) ; // 2.2は経験値 2ではずれる
            this.label_lasttime.Top = (int)Math.Ceiling((this.Height - this.label_lasttime.Height) / 2.2) ;

            this.button_start.Margin = new Padding(0);
            this.button_start.Width = (int)(this.Width * 0.7);
            this.button_start.Height = (int)(this.Height * 0.5);
            this.button_start.Left = (int)Math.Ceiling((this.Width - this.button_start.Width) / 2.2);
            this.button_start.Top = (int)Math.Ceiling((this.Height - this.button_start.Height) / 2.2);

        }
    }
}
