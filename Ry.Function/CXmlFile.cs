using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XMLFileOperate
{
    public static class CXmlFile
    {
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
        public static string GetStringFromXmlFile(string strXMLFileName, string node, string def = "")
        {
            if (PrepareFileExist(strXMLFileName, true, true) == false)
            {
                SaveStringToXmlFile(def, strXMLFileName, node);
                return def;

            }

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(strXMLFileName);
            string nodeName = node;
            XmlNode root = xmldoc.SelectSingleNode("/config/" + nodeName);

            if (root == null)
            {
                SaveStringToXmlFile(def, strXMLFileName, node);
                return def;
            }


            return root.InnerText;

        }

        /// <summary>
        /// 保存信息到XML文件
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="strXMLFileName">xml文件名</param>
        /// <param name="node">xml内的节点名称 多级节点使用"/"分隔</param>
        public static void SaveStringToXmlFile(string str, string strXMLFileName, string node)
        {
            if (PrepareFileExist(strXMLFileName, true, true) == false)
            {
                if (CreateXmlFile(strXMLFileName) == false)
                    throw new Exception(string.Format("创建文件:{0}  出错!\r\n错误位置:{1}",
                        strXMLFileName, "SaveStringListToXmlFile"));
            }

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(strXMLFileName);
            string nodeName = node;
            XmlNode root = xmldoc.SelectSingleNode("/config");
            root = CXmlFile.CreateElementEx(xmldoc, root, nodeName);
            if (root == null)
            {
                try
                {
                    XmlWrite(strXMLFileName, node, str);
                }
                catch (Exception)
                {

                    throw new Exception(string.Format("创建节点是出错！\r\n操作文件:{1}\r\n错误位置:{0}",
                    "SaveStringListToXmlFile", strXMLFileName));
                }
            }
            else
            {
                root.RemoveAll();
            }
            root.InnerText = str;
            xmldoc.Save(strXMLFileName);
        }

        public static void SaveStringListToXmlFile(List<string> lst, string strXMLFileName, string node)
        {
            if (PrepareFileExist(strXMLFileName, true, true) == false)
            {
                throw new Exception(string.Format("创建文件:{0}  出错!\r\n错误位置:{1}",
                    strXMLFileName, "SaveStringListToXmlFile"));
            }

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(strXMLFileName);
            string nodeName = node;
            XmlNode root = xmldoc.SelectSingleNode("/config");
            root = CXmlFile.CreateElementEx(xmldoc, root, nodeName);
            if (root == null)
            {
                throw new Exception(string.Format("创建节点是出错！\r\n操作文件:{1}\r\n错误位置:{0}",
                    "SaveStringListToXmlFile", strXMLFileName));
            }
            else
            {
                root.RemoveAll();
            }

            foreach (string value in lst)
            {
                XmlElement doc = xmldoc.CreateElement("Item");
                doc.InnerText = value;
                root.AppendChild(doc);
            }
            xmldoc.Save(strXMLFileName);
        }
        /// <summary>
        /// 得到XML文件中节点的值
        /// </summary>
        /// <param name="strXMLFileName">xml文件名</param>
        /// <param name="node">xml内的节点名称 多级节点使用"/"分隔</param>
        public static List<string> GetStringListFromXmlFile(string strXMLFileName, string node, List<string> lst = null)
        {
            List<string> result = new List<string>();
            if (PrepareFileExist(strXMLFileName, true, true) == false)
            {
                throw new Exception(string.Format("创建文件:{0}  出错!\r\n错误位置:{1}",
                    strXMLFileName, "SaveStringListToXmlFile"));
            }

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(strXMLFileName); 
            string nodeName = node;
            XmlNode root = xmldoc.SelectSingleNode("/config/" + nodeName);
            if (root == null)
            {
                root = xmldoc.SelectSingleNode("/config");
                root = CXmlFile.CreateElementEx(xmldoc, root, nodeName);
                if (lst != null)
                {
                    foreach (string value in lst)
                    {
                        XmlElement doc = xmldoc.CreateElement("Item");
                        doc.InnerText = value;
                        root.AppendChild(doc);
                    }
                }
                xmldoc.Save(strXMLFileName);
                return lst;
            }

            XmlNodeList mm = root.ChildNodes;
            foreach (XmlNode mNode in mm)
            {
                result.Add(mNode.InnerText);
            }

            return result;
        }

        /// <summary>
        /// 读取一个XML中的节点
        /// </summary>
        /// <param name="fileName">路径名称</param>
        /// <param name="XmlCh沈阳和光系统集成有限公司 医学影像图分析系统
        /// <returns>节点值</returns>
        static public string XmlRead(string fileName, string XmlChildName)
        {
            string XmlValues = "";

            XmlDocument XDoc = new XmlDocument();
            XDoc.Load(@fileName);

            //try
            XmlNodeList List = XDoc.DocumentElement.ChildNodes;

            foreach (XmlNode XN in List)
            {
                XmlElement XE = (XmlElement)XN;
                if (XE.Name == XmlChildName)
                {
                    XmlValues = XE.InnerText;
                }
            }
            //XDoc.Save("xmlMixRecord.xml");
            //}
            //catch
            //{
            //   // MessageBox.Show("xmlMixRecord.xml丢失或被修改！");
            //}

            return XmlValues;
        }
        /// <summary>
        /// 对一个XML的节点进行写操作
        /// </summary>
        /// <param name="fileName">路径名称</param>
        /// <param name="XmlChildName">节点名称</param>
        /// <param name="values">节点值</param>
        static public void XmlWrite(string fileName, string XmlChildName, string values)
        {
            if (PrepareFileExist(fileName, true, true) == false)
            {
                throw new Exception(string.Format("创建文件:{0}  出错!\r\n错误位置:{1}"));
            }

            XmlDocument XDoc = new XmlDocument();
            XDoc.Load(@fileName);
            XmlNodeList List = XDoc.DocumentElement.ChildNodes;

            foreach (XmlNode XN in List)
            {
                XmlElement XE = (XmlElement)XN;
                if (XE.Name == XmlChildName)
                {
                    XE.InnerText = values;
                }
            }

            XDoc.Save(@fileName);

        }

        /// <summary>
        /// 在文档中创建多级Element 多级元素中间用/号间隔
        /// </summary>
        /// <returns></returns>
        static public XmlNode CreateElementEx(XmlDocument xmldoc, XmlNode xNode, string strElement)
        {
            if (xNode == null) return null;
            string[] Element = strElement.Split('/');
            XmlNode root;

            foreach (string s in Element)
            {
                root = xNode.SelectSingleNode(s);
                if (root == null)
                {
                    XmlElement d = xmldoc.CreateElement(s);
                    xNode.AppendChild(d);
                }
                xNode = xNode.SelectSingleNode(s);
            }
            return xNode;
        }

        /// <summary>
        /// 创建一个空XML文件。
        /// </summary>
        /// <param name="fileName"> 指定的文件名 </param>
        /// <returns></returns>
        public static bool CreateXmlFile(string fileName)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml("<config ProduceName='DarkKevin.com.cn' Ver='v4.0'>" +
                            "<title>医学系统</title>" +
                            "</config>");

                //Create an XML declaration. 
                XmlDeclaration xmldecl;
                xmldecl = doc.CreateXmlDeclaration("1.0", null, null);

                //Add the new node to the document.
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmldecl, root);
                doc.Save(fileName);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加根下的一个元素
        /// </summary>
        /// <param name="fileName">路径名</param>
        /// <param name="createNodeName">新建名称</param>
        /// <param name="rootName">根名称</param>
        /// <param name="natureName">属性名称集合</param>
        /// <param name="natureValues">属性值集合</param>
        static public void XmlAddNode(string fileName, string createNodeName, string rootName, string[] natureName, string[] natureValues)
        {


            if (natureName == null || natureValues == null)
            {
                XmlDocument XDoc = new XmlDocument();

                XDoc.Load(@fileName);

                XmlNode xmldocSelect = XDoc.SelectSingleNode(rootName);
                XmlElement XEL = XDoc.CreateElement(createNodeName);

                xmldocSelect.AppendChild(XEL);

                XDoc.Save(@fileName);
                return;
            }
            if (natureName.Length == natureValues.Length)
            {
                XmlDocument XDoc = new XmlDocument();

                XDoc.Load(@fileName);

                XmlNode xmldocSelect = XDoc.SelectSingleNode(rootName);
                XmlElement XEL = XDoc.CreateElement(createNodeName);

                //XEL.SetAttribute("a", "1");
                //
                for (int i = 0; i < natureName.Length; i++)
                {
                    XEL.SetAttribute(natureName[i], natureValues[i]);
                }

                xmldocSelect.AppendChild(XEL);

                XDoc.Save(@fileName);
            }
            else
            {

            }
        }

    }
}
