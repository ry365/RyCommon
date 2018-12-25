using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

    


namespace test
{
    public partial class HotKey : Form
    {
        public int i;
        public HotKey()
        {
            InitializeComponent();
            i = 0;
        }

        Ry.GlobalHotKey.GlobalHotKey h = new Ry.GlobalHotKey.GlobalHotKey();

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "注册")
            {
                string vaule = textBox1.Text;
                Regist(vaule);
                button1.Text = "卸载";
                label2.Text = "注册成功";
            }
            else
            {
                h.UnRegist(this.Handle, CallBack);
                button1.Text = "注册";
                label2.Text = "卸载成功";
            }
        }

        private void Regist(string str)
        {
            if (str == "")
                return;
            int modifiers = 0;
            Keys vk = Keys.None;
            foreach (string value in str.Split('+'))
            {
                if (value.Trim() == "Ctrl")
                    modifiers = modifiers + (int)Ry.GlobalHotKey.GlobalHotKey.HotkeyModifiers.Control;
                else if (value.Trim() == "Alt")
                    modifiers = modifiers + (int)Ry.GlobalHotKey.GlobalHotKey.HotkeyModifiers.Alt;
                else if (value.Trim() == "Shift")
                    modifiers = modifiers + (int)Ry.GlobalHotKey.GlobalHotKey.HotkeyModifiers.Shift;
                else
                {
                    if (Regex.IsMatch(value, @"[0-9]"))
                    {
                        vk = (Keys)Enum.Parse(typeof(Keys), "D" + value.Trim());
                    }
                    else
                    {
                        vk = (Keys)Enum.Parse(typeof(Keys), value.Trim());
                    }
                }
            }
            //这里注册了Ctrl+Alt+E 快捷键
            h.Regist(this.Handle, modifiers, vk, CallBack);
        }

        //按下快捷键时被调用的方法
        public void CallBack()
        {
            listBox1.Items.Add("快捷键被调用！" + i.ToString());
        }

        protected override void WndProc(ref Message m)
        {
            //窗口消息处理函数
            h.ProcessHotKey(m);
            base.WndProc(ref m);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            StringBuilder keyValue = new StringBuilder();
            keyValue.Length = 0;
            keyValue.Append("");
            if (e.Modifiers != 0)
            {
                if (e.Control)
                    keyValue.Append("Ctrl + ");
                if (e.Alt)
                    keyValue.Append("Alt + ");
                if (e.Shift)
                    keyValue.Append("Shift + ");
            }
            if ((e.KeyValue >= 33 && e.KeyValue <= 40) ||
                (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                (e.KeyValue >= 112 && e.KeyValue <= 123))   //F1-F12
            {
                keyValue.Append(e.KeyCode);
            }
            else if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
            {
                keyValue.Append(e.KeyCode.ToString().Substring(1));
            }
            ((TextBox)sender).Text = keyValue.ToString();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string str = ((TextBox)sender).Text.TrimEnd();
            int len = str.Length;
            if (len >= 1 && str.Substring(str.Length - 1) == "+")
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            h.UnRegist(this.Handle, CallBack);
        }


        public void aaa()
        {
            MessageBox.Show("CTRL+C");
        }
        public void bbb()
        {
            MessageBox.Show("CTRL+D");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            h.Regist(this.Handle, "CTRL+C", aaa);
            h.Regist(this.Handle, "CTRL+D", bbb);
             //h.Regist(this.Handle,, vk, CallBack);
        }

    }
}
