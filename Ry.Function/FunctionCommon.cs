using System;
using System.Data;
using System.IO;
using System.Management;
using System.Media;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using XMLFileOperate;

namespace Ry.Function
{
    public class FunctionCommon
    {
        #region  其他常用操作


        /// <summary>
        /// 检查文件是否存在，如不存在是否创建文件夹及XML的文件头
        /// </summary>
        /// <param name="strFileName">检查的文件名称</param>
        /// <param name="CreatePath">是否创建文件夹</param>
        /// <param name="CreateXMLHead">是否创建XML文件头</param>
        /// <returns>文件是否存在</returns>
        public static bool PrepareFileExist(string strFileName, bool bCreatePath, bool bCreateXMLHead)
        {
            if (File.Exists(strFileName))
                return true;

            string str = Path.GetDirectoryName(strFileName);
            if (str != strFileName)
            {

                if (Directory.Exists(str) == false)
                {
                    if (bCreatePath)
                        Directory.CreateDirectory(str);
                    else
                        return false;
                }
            }

            if (bCreateXMLHead)
            {
                CXmlFile.CreateXmlFile(strFileName);
            }
            
            return true;
        }

        /// <summary>
        /// 检查并创建路径,只检查路径，不能传文件名
        /// </summary>
        /// <param name="strFileName">路径名</param>
        /// <param name="bCreatePath">是否创建路径</param>
        /// <returns></returns>
        public static bool PrepareDirectoryExist(string strFileName, bool bCreatePath)
        {
            if (File.Exists(strFileName))
                return true;

            if (Directory.Exists(strFileName) == false)
                {
                    if (bCreatePath)
                        Directory.CreateDirectory(strFileName);
                    else
                        return false;
                }

            return true;
        }

        #region 文件与流转换

        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }


       /// <summary>
        ///  将 byte[] 转成 Stream
       /// </summary>
       /// <param name="bytes"></param>
       /// <returns>返回 MemoryStream</returns>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }


        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            var fs = new FileStream(fileName, FileMode.Create);
            var bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 将文件独到流中，返回类型为MemoryStream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>返回类型为MemoryStream</returns>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            var bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        #endregion 文件与流转换

        #endregion 其他常用操作

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        public static string GetLocalDiskAlignment()
        {
            string diskName = "";


            var aa = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = aa.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (((string)mo.Properties["Model"].Value).ToString().IndexOf("USB") >= 0)
                    continue;
                diskName = (string)mo.Properties["Model"].Value;
            }
            if (diskName.Length > 16)
            {
                diskName = diskName.Substring(diskName.Length - 16, 16);
            }
            return diskName;
        }

        /// <summary>
        /// 得到磁盘序列号 
        /// </summary>
        /// <returns></returns>
        public static string GetLocalDiskAlignmentHex()
        {
            string str = EncodeAndDecode.GetMD5String(GetLocalDiskAlignment(), "Disk");
            str = str.Replace("-", "");

            return str;
        }

        public static bool GetAuthorityValues(string formName, string btnName, DataTable dt, string Authority)
        {
            string values = "";

            bool check = false;

            DataRow[] dr = dt.Select("NameEnglish = '" + formName + "' and BtnName = '" + btnName + "'");

            for (int i = 0; i < dr.Length; i++)
            {
                values = dr[i]["PermissionCode"].ToString();
            }

            if (Authority.IndexOf(values) < 0)
            {
                check = false;
            }
            else
            {
                check = true;
            }

            if (values == "")
            {
                check = false;
            }

            return check;
        }

        /// <summary>
        /// 得到字符串的拼音首字母
        /// </summary>
        /// <param name="chineseString"></param>
        /// <returns></returns>
        public static string GetEnglishHead(string chineseString)
        {
            string cc = Hz2Pin.Convert(chineseString);
            string str = "";
            foreach ( char c1 in cc.ToCharArray() )
            {
                str += Hz2Pin.Convert(c1.ToString())[0];

            }
            return str.ToLower();
        }
      
        /// <summary>
        /// 得到中文全拼
        /// </summary>
        /// <param name="chineseString"></param>
        /// <returns></returns>
        public static string GetEnglistString(string chineseString)
        {
            return Hz2Pin.Convert(chineseString);
        }

        /// <summary>
        /// 播放声音文件
        /// </summary>
        /// <param name="file"></param>
        public static void PlaySound(string file)
        {
            System.Media.SoundPlayer sp = new SoundPlayer(file);
            sp.Play();
        }

        /// <summary>
        /// 播放媒体文件
        /// </summary>
        /// <param name="fileName"></param>
        [DllImport("shell32.dll")]
        public extern static IntPtr ShellExecute(IntPtr hwnd,
                                                 string lpOperation,
                                                 string lpFile,
                                                 string lpParameters,
                                                 string lpDirectory,
                                                 int nShowCmd
                                                );
        public static void PlayMedia(string fileName)
        {
            if (File.Exists(fileName))
            {
                IntPtr p = IntPtr.Zero;
                ShellExecute(p, "open", fileName, null, null, 5);
            }
            else
            {
                MessageBox.Show(string.Format("请确认文件{0}存在！并且格式可以被执行！",fileName));
            }
        }
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(uint uType);


        /// <summary>
        /// 从XML文件中读取一个DataTable
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="address">XML文件地址</param>
        /// <returns></returns>
        public static DataTable XmlFile2Datatable(string address)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!File.Exists(address))
                {
                    throw new Exception("文件不存在!");
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(address);
                XmlNode root = xmlDoc.SelectSingleNode("DataTable");
                //读取表名
                dt.TableName = ((XmlElement)root).GetAttribute("TableName");
                //Console.WriteLine("读取表名： {0}", dt.TableName);
                //读取行数
                int CountOfRows = 0;
                if (!int.TryParse(((XmlElement)root).
                    GetAttribute("CountOfRows").ToString(), out CountOfRows))
                {
                    throw new Exception("行数转换失败");
                }
                //读取列数
                int CountOfColumns = 0;
                if (!int.TryParse(((XmlElement)root).
                    GetAttribute("CountOfColumns").ToString(), out CountOfColumns))
                {
                    throw new Exception("列数转换失败");
                }
                //从第一行中读取记录的列名
                foreach (XmlAttribute xa in root.ChildNodes[0].Attributes)
                {
                    dt.Columns.Add(xa.Value);
                    //Console.WriteLine("建立列： {0}", xa.Value);
                }
                //从后面的行中读取行信息
                for (int i = 1; i < root.ChildNodes.Count; i++)
                {
                    string[] array = new string[root.ChildNodes[0].Attributes.Count];
                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j] = root.ChildNodes[i].Attributes[j].Value.ToString();
                    }
                    dt.Rows.Add(array);
                    //Console.WriteLine("行插入成功");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new DataTable();
            }
            return dt;
        }


        /// <summary>
        /// 将DataTable的内容写入到XML文件中
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="address">XML文件地址</param>
        public static bool DataTable2XmlFile(DataTable dt, string address)
        {
            try
            {
                //如果文件DataTable.xml存在则直接删除
                if (File.Exists(address))
                {
                    File.Delete(address);
                }
                XmlTextWriter writer =
                    new XmlTextWriter(address, Encoding.GetEncoding("GBK"));
                writer.Formatting = Formatting.Indented;
                //XML文档创建开始
                writer.WriteStartDocument();
                writer.WriteComment("DataTable: " + dt.TableName);
                writer.WriteStartElement("DataTable"); //DataTable开始
                writer.WriteAttributeString("TableName", dt.TableName);
                writer.WriteAttributeString("CountOfRows", dt.Rows.Count.ToString());
                writer.WriteAttributeString("CountOfColumns", dt.Columns.Count.ToString());
                writer.WriteStartElement("ClomunName", ""); //ColumnName开始
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    writer.WriteAttributeString(
                        "Column" + i.ToString(), dt.Columns[i].ColumnName);
                }
                writer.WriteEndElement(); //ColumnName结束
                //按行各行
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    writer.WriteStartElement("Row" + j.ToString(), "");
                    //打印各列
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        writer.WriteAttributeString(
                            "Column" + k.ToString(), dt.Rows[j][k].ToString());
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement(); //DataTable结束
                writer.WriteEndDocument();
                writer.Close();
                //XML文档创建结束
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

    }

}