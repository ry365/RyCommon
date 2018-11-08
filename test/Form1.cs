using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace test
{
    public partial class Form1 : Form
    {
        public OracleConnection ORAconn;
        public OracleCommand ORAcmd;
        public OracleDataAdapter ORAadp;


        public Form1()
        {
            InitializeComponent();
        }



        public string ConnectOracle()
        {
            try
            {
                if (ORAconn == null)
                    if (ORAconn == null)
                    {
                        string connString =
                            "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.115.155)" +
                            "(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORACLE)));" +
                            "Persist Security Info=True;User ID=us;Password=US;";
                        ORAconn = new OracleConnection(connString);
                        ORAcmd = new OracleCommand("", ORAconn);
                        ORAadp = new OracleDataAdapter(ORAcmd);
                        ORAconn.Open();
                    }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ConnectOracle();
            DataTable dt = new DataTable();
            dt = Ry.Function.FunctionCommon.ReadFromXml("C:\\aa.xml");
            MessageBox.Show(dt.Rows[0][1].ToString());

        }
    }
}
