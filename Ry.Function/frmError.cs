using System;
using System.Windows.Forms;
using Ry.Defined;
using Ry.Defined.ErrorType;

namespace Ry.Appbase
{
    public partial class frmError : Form
    {

       public void setHintMessage(string SystemMessage,string ErrorSite)
       {
           string err;
           if (string.IsNullOrEmpty(ErrorSite))
               err = MyConst.Err.ErrorSite;
           else
               err = MyConst.Err.ErrorSite + "--" + ErrorSite;


           textBox1.Text = string.Format("错误ＩＤ：Ry - {0}\r\n\r\n错误位置：{1}\r\n\r\n错误信息：{2}\r\n\r\n错误提示：{3}",
                  MyConst.Err.ErrorID, err , MyConst.Err.ErrorString, MyConst.Err.ErrorHint);
           if (SystemMessage != null)
           textBox2.Text = SystemMessage.Replace("\n","\r\n");
       }

       public static  void ShowError(EnumHintType ErrorType, MyError e,string site)
       {
           frmError frm = new frmError();
            switch (ErrorType)
           {
               case EnumHintType.Error:
                   frm.Text = "执行过程中发行错误";
                   break;
               case EnumHintType.Warning:
                   frm.Text = "警告";
                   break;
               case EnumHintType.Hint:
                   frm.Text = "提示";
                   break;
           }
           frm.setHintMessage(e.ErrorSystem, site);

           frm.ShowDialog();
       }
        

       public static void ShowError(EnumHintType ErrorType, Exception e, string site)
       {
           frmError frm = new frmError();
            switch (ErrorType)
           {
               case EnumHintType.Error:
                   frm.Text = "执行过程中发行错误";
                   break;
               case EnumHintType.Warning:
                   frm.Text = "警告";
                   break;
               case EnumHintType.Hint:
                   frm.Text = "提示";
                   break;
           }
           frm.setHintMessage(e.Message + "\r\n" + e.StackTrace,  site);
           frm.ShowDialog();
       }


    
        public frmError()
        {
            InitializeComponent();
        }

        public static void ShowError(string Site)
        {
            frmError frm = new frmError();
            frm.setHintMessage(MyConst.Err.ErrorSystem, Site);
            frm.ShowDialog();
        }


    }
}
