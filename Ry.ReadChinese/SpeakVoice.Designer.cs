namespace Ry.ReadChinese
{
    partial class SpeakVoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lstLanguage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstVoiceType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sliderValue = new System.Windows.Forms.TrackBar();
            this.playTimes = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sliderValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "语音类型";
            // 
            // lstLanguage
            // 
            this.lstLanguage.FormattingEnabled = true;
            this.lstLanguage.Location = new System.Drawing.Point(108, 17);
            this.lstLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.lstLanguage.Name = "lstLanguage";
            this.lstLanguage.Size = new System.Drawing.Size(402, 24);
            this.lstLanguage.TabIndex = 1;
            this.lstLanguage.SelectionChangeCommitted += new System.EventHandler(this.lstLanguage_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "格式类型";
            // 
            // lstVoiceType
            // 
            this.lstVoiceType.FormattingEnabled = true;
            this.lstVoiceType.Location = new System.Drawing.Point(108, 55);
            this.lstVoiceType.Margin = new System.Windows.Forms.Padding(4);
            this.lstVoiceType.Name = "lstVoiceType";
            this.lstVoiceType.Size = new System.Drawing.Size(402, 24);
            this.lstVoiceType.TabIndex = 1;
            this.lstVoiceType.SelectionChangeCommitted += new System.EventHandler(this.lstVoiceType_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "语    速";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "播放次数";
            // 
            // sliderValue
            // 
            this.sliderValue.LargeChange = 1;
            this.sliderValue.Location = new System.Drawing.Point(108, 93);
            this.sliderValue.Name = "sliderValue";
            this.sliderValue.Size = new System.Drawing.Size(399, 45);
            this.sliderValue.TabIndex = 2;
            this.sliderValue.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sliderValue.ValueChanged += new System.EventHandler(this.sliderValue_ValueChanged);
            // 
            // playTimes
            // 
            this.playTimes.LargeChange = 1;
            this.playTimes.Location = new System.Drawing.Point(108, 138);
            this.playTimes.Maximum = 5;
            this.playTimes.Minimum = 1;
            this.playTimes.Name = "playTimes";
            this.playTimes.Size = new System.Drawing.Size(399, 45);
            this.playTimes.TabIndex = 2;
            this.playTimes.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.playTimes.Value = 1;
            this.playTimes.ValueChanged += new System.EventHandler(this.playTimes_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(29, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(403, 26);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "测试内容一二三四5六七";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(439, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "播放测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(292, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 26);
            this.button2.TabIndex = 5;
            this.button2.Text = "确  定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(414, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 26);
            this.button3.TabIndex = 5;
            this.button3.Text = "取  消";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // RYSpeakVoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 248);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.playTimes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sliderValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstVoiceType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstLanguage);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RYSpeakVoice";
            this.Text = "语音播放设置";
            this.Load += new System.EventHandler(this.Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sliderValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.ComboBox lstLanguage;
        public System.Windows.Forms.ComboBox lstVoiceType;
        public System.Windows.Forms.TrackBar sliderValue;
        public System.Windows.Forms.TrackBar playTimes;
    }
}