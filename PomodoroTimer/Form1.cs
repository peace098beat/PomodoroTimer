using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
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

        private int WorkingTimeLimitSec = 1;// 25 * 60;   //25分;
        private int RelaxTimerLimitSec = 5 * 60;     //5分;

        private Stopwatch stopwatch;
        private int LimitTime;
        private Form SecondForm;

        private Label Label_ActiveWinProc;
        private System.Windows.Forms.Timer Timer_ActiveWinProc;
        private TextBox TextBox_ActWinProc;
        private ActiveWindowLogger ActiveWinLogger;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ストップウォッチを初期化
            this.stopwatch = new Stopwatch();
            this.stopwatch.Stop();

            // 初期レイアウト
            this.Width = 220;
            this.Height = 200;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.WindowState = FormWindowState.Normal;


            // アクティブウィンドウ検知クラスの表示タイマーを生成
            this.Timer_ActiveWinProc = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };
            this.Timer_ActiveWinProc.Tick += Timer_ActiveWinProc_Tick;
            this.Timer_ActiveWinProc.Start();

            // ロギング結果を表示
            this.TextBox_ActWinProc = new TextBox()
            {
                Left = 0,
                Top = 0,
                Width = this.Width,
                Height = this.Height,
                Multiline = true,
                BorderStyle = BorderStyle.None
            };
            this.Controls.Add(this.TextBox_ActWinProc);
            this.ActiveWinLogger = new ActiveWindowLogger();
        }

        private void Timer_ActiveWinProc_Tick(object sender, EventArgs e)
        {
            //this.Label_ActiveWinProc.Text = ActiveWindow.GetActiveProcName();

            // プロセス動作時間を表示
            this.TextBox_ActWinProc.Clear();

            var copyTable = new Dictionary<string, int>(this.ActiveWinLogger.GetDatas());

            foreach (var item in copyTable)
            {
                //double min = item.Value / 10 / 60.0;
                double min = item.Value ;
                int dmin = (int)Math.Ceiling(min);
                
                int Tick = 15; // 30min

                int Count = dmin / Tick;

                // 9h x 60m = 540m
                // ■■■■■■■■■■ 10 => 10h
                // |||||||||||||||||||| 20 => 30 x 20

                this.TextBox_ActWinProc.AppendText($"{item.Key} :");
                for (int i = 0; i < Count; i++)
                {
                    this.TextBox_ActWinProc.AppendText($"|");
                }


                this.TextBox_ActWinProc.AppendText($"{dmin}分" + Environment.NewLine);
            }

        }



        /// <summary>
        /// セカンドディスプレイがある場合に、起動
        /// </summary>
        private void ShowSecondScreen()
        {

            if (Screen.AllScreens.Length == 1)
            {
                // シングルスクリーンなのでなにもしない
                return;
            }

            // フォームが閉じている場合は開く
            if (SecondForm == null || SecondForm.IsDisposed)
            {
                // 閉じている
                this.SecondForm = new Form();
                this.SecondForm.StartPosition = FormStartPosition.Manual;

                Screen _MainFormScrn = Screen.FromPoint(this.Location);

                Debug.WriteLine(_MainFormScrn.DeviceName);

                foreach (Screen screen in Screen.AllScreens)
                {
                    if (screen.DeviceName != _MainFormScrn.DeviceName)
                    {
                        this.SecondForm.Location = screen.Bounds.Location;
                    }
                }

                SecondForm.WindowState = FormWindowState.Maximized;
                SecondForm.BackColor = Color.LightSkyBlue;
                SecondForm.Show();
            }

            if (SecondForm.TopMost != true) SecondForm.TopMost = true;
            if (SecondForm.WindowState != FormWindowState.Maximized) SecondForm.WindowState = FormWindowState.Maximized;
            if (SecondForm.FormBorderStyle != FormBorderStyle.None) SecondForm.FormBorderStyle = FormBorderStyle.None;

        }

        /// <summary>
        /// マルチディスプレイ用
        /// </summary>
        private void CloseSecondScreen()
        {
            this.SecondForm?.Close();
        }

        private void timer_every1s_Tick(object sender, EventArgs e)
        {

            switch (AppState)
            {
                case TimerState.Wait:
                    // マルチスクリーン用
                    CloseSecondScreen();

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
                    this.TopMost = true;

                    if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;

                    this.FormBorderStyle = FormBorderStyle.FixedSingle;

                    // =============================
                    // Textbox_ActiveWindowProcLogger
                    // =============================
                    this.TextBox_ActWinProc.Hide();


                    break;

                case TimerState.Working:
                    // マルチスクリーン用
                    CloseSecondScreen();


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

                    // =============================
                    // Textbox_ActiveWindowProcLogger
                    // =============================
                    this.TextBox_ActWinProc.Show();
                    this.TextBox_ActWinProc.Top = 0;
                    this.TextBox_ActWinProc.Left = this.Width / 2 - 10;
                    this.TextBox_ActWinProc.Width = this.Width / 2 - 10;
                    this.TextBox_ActWinProc.Height = this.Height - 50;
                    this.TextBox_ActWinProc.BackColor = this.BackColor;
                    this.TextBox_ActWinProc.ForeColor = this.ForeColor;

                    break;

                case TimerState.Relax:
                    // マルチスクリーン用
                    ShowSecondScreen();


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
                    this.FormBorderStyle = FormBorderStyle.None; // 閉じれない

                    // =============================
                    // Textbox_ActiveWindowProcLogger
                    // =============================
                    this.TextBox_ActWinProc.Show();
                    this.TextBox_ActWinProc.Top = 25;
                    this.TextBox_ActWinProc.Left = this.Width / 2 - 10;
                    this.TextBox_ActWinProc.Width = this.Width / 2 - 10;
                    this.TextBox_ActWinProc.Height = this.Height - 50;
                    this.TextBox_ActWinProc.BackColor = this.BackColor;
                    this.TextBox_ActWinProc.ForeColor = this.ForeColor;

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

            // カウントダウン
            this.label_lasttime.Margin = new Padding(0);
            this.label_lasttime.Left = (int)Math.Ceiling((this.Width - this.label_lasttime.Width) / 2.2); // 2.2は経験値 2ではずれる
            this.label_lasttime.Top = (int)Math.Ceiling((this.Height - this.label_lasttime.Height) / 2.2);

            this.button_start.Margin = new Padding(0);
            this.button_start.Width = (int)(this.Width * 0.7) ;
            this.button_start.Height = (int)(this.Height * 0.5);
            this.button_start.Left = (int)Math.Ceiling((this.Width - this.button_start.Width) / 2.2);
            this.button_start.Top = (int)Math.Ceiling((this.Height - this.button_start.Height) / 2.2);

        }
    }
}
