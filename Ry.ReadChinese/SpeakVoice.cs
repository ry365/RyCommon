using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ry.Defined;
using SpeechLib;
using XMLFileOperate;

namespace Ry.ReadChinese
{
    public partial class SpeakVoice : Form
    {
        private delegate void SetEventDelegate(int status,string value);   //先声明一个传递int类型参数，并返回为void的委托  
        private string configFile = MyConst.ConfigFilePath + "\\SpeakVoice.cfg";
        private SpeechVoiceSpeakFlags _spFlags;
        private SpVoice _voice;
        private int _times;
        private bool _working;
        private List<string> _speakTextList;
        private Thread newThread;

        private EventHandler beginSpeak;
        private EventHandler endSpeak;


        public EventHandler BeginSpeak
        {
            get { return beginSpeak; }
            set { beginSpeak = value; }
        }

        public EventHandler EndSpeak
        {
            get { return endSpeak; }
            set { endSpeak = value; }
        }

        public void AppendSpeak(string value)
        {
            _speakTextList.Add(value);
        }

        public void ImmediateSpeak(string value)
        {
            _speakTextList.Insert(0,value);
        }

        public SpeakVoice()
        {
            InitializeComponent();
            IntPtr i = this.Handle;
            _spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            _voice = new SpVoice();
            InitialControl();
            _speakTextList = new List<string>();
            lstLanguage.SelectedIndex = Convert.ToInt32(
                CXmlFile.GetStringFromXmlFile(configFile, "语音设置/语音类型", "0"));
            lstVoiceType.SelectedIndex = Convert.ToInt32(
                CXmlFile.GetStringFromXmlFile(configFile, "语音设置/格式类型", "0"));

            sliderValue.Value = Convert.ToInt32(CXmlFile.GetStringFromXmlFile(configFile, "语音设置/语速", "0"));
            playTimes.Value = Convert.ToInt32(CXmlFile.GetStringFromXmlFile(configFile, "语音设置/播放次数", "1"));
            newThread = new Thread(new ParameterizedThreadStart(doThread));

        }

        private void InitialControl()
        {

            foreach (ISpeechObjectToken va in _voice.GetVoices())
            {
                lstLanguage.Items.Add(va.GetDescription());
            }
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT8kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT8kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT8kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT8kHz16BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT11kHz8BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT11kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT11kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT11kHz16BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT12kHz8BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT12kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT12kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT12kHz16BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT16kHz8BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT16kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT16kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT16kHz16BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT22kHz8BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT22kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT22kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT22kHz16BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT24kHz8BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT24kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT24kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT24kHz16BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT32kHz8BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT32kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT32kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT32kHz16BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT44kHz8BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT44kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT44kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT44kHz16BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT48kHz8BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT48kHz8BitStereo);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT48kHz16BitMono);
            lstVoiceType.Items.Add(SpeechAudioFormatType.SAFT48kHz16BitStereo);
        }

        private void lstLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _voice.Voice = _voice.GetVoices().Item(lstLanguage.SelectedIndex);
        }
    

        private void lstVoiceType_SelectionChangeCommitted(object sender, EventArgs e)
        {

            _voice.AllowAudioOutputFormatChangesOnNextSet = false;
            switch (lstVoiceType.SelectedIndex)
            {
                case 0:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT8kHz16BitMono;
                    break;
                case 1:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT8kHz8BitStereo;
                    break;
                case 2:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT8kHz16BitMono;
                    break;
                case 3:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT8kHz16BitStereo;
                    break;
                case 4:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT11kHz8BitMono;
                    break;
                case 5:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT11kHz8BitStereo;
                    break;
                case 6:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT11kHz16BitMono;
                    break;
                case 7:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT11kHz16BitStereo;
                    break;
                case 8:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT12kHz8BitMono;
                    break;
                case 9:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT12kHz8BitStereo;
                    break;
                case 10:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT12kHz16BitMono;
                    break;
                case 11:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT12kHz16BitStereo;
                    break;
                case 12:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT16kHz8BitMono;
                    break;
                case 13:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT16kHz8BitStereo;
                    break;
                case 14:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT16kHz16BitMono;
                    break;
                case 15:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT16kHz16BitStereo;
                    break;
                case 16:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT22kHz8BitMono;
                    break;
                case 17:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT22kHz8BitStereo;
                    break;
                case 18:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT22kHz16BitMono;
                    break;
                case 19:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT22kHz16BitStereo;
                    break;
                case 20:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT24kHz8BitMono;
                    break;
                case 21:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT24kHz8BitStereo;
                    break;
                case 22:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT24kHz16BitMono;
                    break;
                case 23:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT24kHz16BitStereo;
                    break;
                case 24:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT32kHz8BitMono;
                    break;
                case 25:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT32kHz8BitStereo;
                    break;
                case 26:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT32kHz16BitMono;
                    break;
                case 27:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT32kHz16BitStereo;
                    break;
                case 28:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT44kHz8BitMono;
                    break;
                case 29:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT44kHz8BitStereo;
                    break;
                case 30:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT44kHz16BitMono;
                    break;
                case 31:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT44kHz16BitStereo;
                    break;
                case 32:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT48kHz8BitMono;
                    break;
                case 33:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT48kHz8BitStereo;
                    break;
                case 34:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT48kHz16BitMono;
                    break;
                case 35:
                    _voice.AudioOutputStream.Format.Type = SpeechAudioFormatType.SAFT48kHz16BitStereo;
                    break;
            }
            _voice.AudioOutputStream = _voice.AudioOutputStream;
        }

        private void sliderValue_ValueChanged(object sender, EventArgs e)
        {
            _voice.Rate = Convert.ToInt32(sliderValue.Value);
        }

        private void playTimes_ValueChanged(object sender, EventArgs e)
        {
            _times = Convert.ToInt32(playTimes.Value);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _voice.Speak(textBox1.Text, _spFlags);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
               
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           CXmlFile.SaveStringToXmlFile(lstLanguage.SelectedIndex.ToString(), configFile, "语音设置/语音类型");
           CXmlFile.SaveStringToXmlFile(lstVoiceType.SelectedIndex.ToString(), configFile, "语音设置/格式类型");
           CXmlFile.SaveStringToXmlFile(sliderValue.Value.ToString(), configFile, "语音设置/语速");
           CXmlFile.SaveStringToXmlFile(playTimes.Value.ToString(), configFile, "语音设置/播放次数");
            this.DialogResult = DialogResult.OK;
            
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        public void speakVoice()
        {
            if (newThread.ThreadState != ThreadState.Running)
            {
                newThread = new Thread(new ParameterizedThreadStart(doThread));
                newThread.Start(_speakTextList);
            }
        }





        private void DoEvent(int status ,string value)  //当跨线程调用时，调用该方法进行UI界面更新  
        {
            if (this.InvokeRequired)  //判断label1控件是否是调用线程(即newThread线程)创建的,也就是是否跨线程调用,如果是则返回true,否则返回false  
            {
                this.BeginInvoke(new SetEventDelegate(DoEvent), new object[] { status, value });  //异步调用setLabelText方法，并传递一个int参数  
            }
            else
            {
                switch (status)
                {
                    case 0:
                        BeginSpeak?.Invoke(value, null);
                        break;
                    case 1:
                        EndSpeak?.Invoke(value, null);
                        break;
                }
            }
        }


        private void doThread(object contextList)
        {
            while (!_working)
            {
                List<string> v = contextList  as List<string>;
                while (v.Count > 0)
                {
                    _working = true;
                    string value = v[0];

                    DoEvent(0, value);
                    for (int i = 0; i < _times; i++)
                    {
                        _voice.Speak(value);
                        Thread.Sleep(1000);
                    }
                    DoEvent(1, value);
                    v.RemoveAt(0);
                    Thread.Sleep(1000);
                    _working = false;
                }
                break;
            }




        }
    }
}
