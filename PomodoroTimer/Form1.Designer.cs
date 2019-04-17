namespace PomodoroTimer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label_lasttime = new System.Windows.Forms.Label();
            this.label_message = new System.Windows.Forms.Label();
            this.timer_every1s = new System.Windows.Forms.Timer(this.components);
            this.button_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_lasttime
            // 
            this.label_lasttime.AutoSize = true;
            this.label_lasttime.BackColor = System.Drawing.Color.Transparent;
            this.label_lasttime.Font = new System.Drawing.Font("MS UI Gothic", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_lasttime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_lasttime.Location = new System.Drawing.Point(17, 38);
            this.label_lasttime.Name = "label_lasttime";
            this.label_lasttime.Size = new System.Drawing.Size(210, 80);
            this.label_lasttime.TabIndex = 0;
            this.label_lasttime.Text = "00:00";
            // 
            // label_message
            // 
            this.label_message.AutoSize = true;
            this.label_message.BackColor = System.Drawing.SystemColors.Control;
            this.label_message.Font = new System.Drawing.Font("游明朝", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_message.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_message.Location = new System.Drawing.Point(24, 20);
            this.label_message.Name = "label_message";
            this.label_message.Size = new System.Drawing.Size(30, 16);
            this.label_message.TabIndex = 1;
            this.label_message.Text = "wait";
            this.label_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_every1s
            // 
            this.timer_every1s.Enabled = true;
            this.timer_every1s.Tick += new System.EventHandler(this.timer_every1s_Tick);
            // 
            // button_start
            // 
            this.button_start.BackColor = System.Drawing.SystemColors.Control;
            this.button_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_start.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button_start.Location = new System.Drawing.Point(27, 39);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(204, 110);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "START";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Visible = false;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(270, 179);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.label_lasttime);
            this.Controls.Add(this.label_message);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pomodoro Timer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_lasttime;
        private System.Windows.Forms.Label label_message;
        private System.Windows.Forms.Timer timer_every1s;
        private System.Windows.Forms.Button button_start;
    }
}

