using System;
using System.Data;
using System.IO;
using System.Management;
using System.Media;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
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

            //}


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
        /// 将dbtable 存到指定的文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        #region DataTableToXml
       
        public static string DataTableToXml(DataTable dt, string sName)
        {
            if (dt != null)
            {
                MemoryStream ms = null;
                XmlTextWriter XmlWt = null;
                try
                {
                    ms = new MemoryStream();
                    //根据ms实例化XmlWt
                    XmlWt = new XmlTextWriter(ms, System.Text.Encoding.Unicode);
                    //获取ds中的数据
                    dt.TableName = string.IsNullOrEmpty(sName) ? "dt2xml" : sName;
                    dt.WriteXml(XmlWt, XmlWriteMode.WriteSchema);
                    int count = (int)ms.Length;
                    byte[] temp = new byte[count];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(temp, 0, count);
                    //返回Unicode编码的文本
                    System.Text.UnicodeEncoding ucode = new System.Text.UnicodeEncoding();
                    string returnValue = ucode.GetString(temp).Trim();
                    return returnValue;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //释放资源
                    if (XmlWt != null)
                    {
                        XmlWt.Close();
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            else
            {
                return "";
            }
        }

        public static DataSet XmlToDataSet(string xmlString)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlString);
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmldoc.InnerXml);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                reader.Close();
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                reader.Close();
                throw ex;
            }
        }
        #endregion
        public static DataSet FileToDataSet(string file)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(file);
            return ds;
        }

        public static DataTable FileToDataTable(string file)
        {
            DataTable dt = new DataTable();
            dt.ReadXml(file);
            return dt;
        }

        public static void DataSetTofile(DataSet ds, string file)
        {
            ds.WriteXml(file);
        }

        public static void DataTableToFile(DataTable dt,string file)
        {
            dt.WriteXml(file);
        }

        /// <summary>
        /// 将datatable转为xml
        /// </summary>
        /// <param name="vTable">要生成XML的DataTable</param>
        /// <returns></returns>
        public static string DataTable2Xml(DataTable vTable)
        {
            if (null == vTable) return string.Empty;
            StringWriter writer = new StringWriter();
            vTable.WriteXml(writer);
            string xmlstr = writer.ToString();
            writer.Close();
            return xmlstr;
        }

        /// <summary>
        /// 将XML生成DataTable
        /// </summary>
        /// <param name="xmlStr">XML字符串</param>
        /// <returns></returns>
        public static DataTable Xml2DataTable(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据               
                    ds.ReadXml(Xmlrdr);
                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    return null;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            return null;
        }



    }
}