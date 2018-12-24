using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ry.ReadChinese;
using SpeechLib;

namespace test
{
    public partial class Form1 : Form
    {


        private SpeakVoice sv;

        public Form1()
        {
            sv =  new SpeakVoice();
            
            sv.BeginSpeak +=new EventHandler(BeginSpeak);
            sv.EndSpeak = this.EndSpeak;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sv.AppendSpeak(textBox1.Text);
            sv.speakVoice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sv.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void BeginSpeak(object sender, EventArgs e)
        {
            listBox1.Items.Add("Begin+"+sender.ToString());
        }

        private void EndSpeak(object sender, EventArgs e)
        {
            listBox1.Items.Add("End+" + sender.ToString());
        }

    }

}
