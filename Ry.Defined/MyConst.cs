using System;
using System.Windows.Forms;
using Ry.Defined.ErrorType;
using System.Collections.Generic;

namespace Ry.Defined
{
    public static  partial class MyConst
    {
        /// <summary>
        /// 用于函数的返回值
        /// </summary>
        public const int Success = 0;
        public const int Failed = 1;
        public static MyError Err;
        public static EnumDatabaseType MyDBType= EnumDatabaseType.ORACLE;
        public static DateTime CurrentDate = DateTime.Now;


        public static string RegKey = "Ren.Y";



        public static string CurrentPath = Application.StartupPath;
        /// <summary>
        /// 配置文件保存路径
        /// </summary>
        public static string ConfigFilePath = Application.StartupPath + "\\Configure";

        /// <summary>
        /// 窗体Layout的配置位置
        /// </summary>
        public static string ConfigFormLayoutFilePath = Application.StartupPath + "\\Configure\\Layout";

        /// <summary>
        /// 窗体其他信息保存文件
        /// </summary>
        public static string ConfigFormOthersProperty = Application.StartupPath + "\\Configure\\FormOthers.xml";

        /// <summary>
        /// 服务器配置文件路径
        /// </summary>
        public static string ServerSettingXMLFile = ConfigFilePath + "\\ServerConfig.xml";

        
    }
}
