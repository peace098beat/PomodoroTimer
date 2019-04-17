using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace PomodoroTimer
{
    /// Win32API の extern 宣言クラス
    public class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowText", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
    }


    public static class ActiveWindow
    {

        /// <summary>
        /// アクティブウィンドウの名前を返す
        /// </summary>
        /// <returns></returns>
        public static string GetActiveWindowText()
        {
            StringBuilder sb = new StringBuilder(65535); //65535に特に意味はない
            WinAPI.GetWindowText(WinAPI.GetForegroundWindow(), sb, 65535);

            return sb.ToString();
        }


        public static string GetActiveProcName()
        {
            // アクティブなウィンドウハンドルの取得
            IntPtr hWnd = WinAPI.GetForegroundWindow();
            int id;
            // ウィンドウハンドルからプロセスIDを取得
            WinAPI.GetWindowThreadProcessId(hWnd, out id);
            Process process = Process.GetProcessById(id);


            return process.ProcessName;

        }

    }

    public class ActiveWindowLogger
    {
        private Dictionary<String, int> HashTable;
        private Timer timer;

        public ActiveWindowLogger()
        {
            this.HashTable = new Dictionary<string, int>();

            this.timer = new System.Timers.Timer();
            this.timer.Interval = 100;
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.Start();
        }

        public Dictionary<string, int> GetDatas()
        {
            // たぶん参照渡し.
            return this.HashTable;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string procname = ActiveWindow.GetActiveProcName();
            string wintitle = ActiveWindow.GetActiveWindowText();

            if (this.HashTable.ContainsKey(procname))
            {
                this.HashTable[procname] += 1;
            }
            else
            {
                this.HashTable[procname] = 0;
            }

        }


    }
}
