using System;
using System.Windows.Forms;
using Ry.Defined;

namespace Ry.Function
{
    public class MyConvert
    {

        public static string GetDateConditionString(EnumCustomDateType DateT, string Field)
        {
            switch (MyConst.MyDBType)
            {
                    case EnumDatabaseType.ORACLE:
                        switch (DateT)
                        {
                            case EnumCustomDateType.Last3Days:
                                return string.Format("{0} >= to_date({1},'yyyy-mm-dd 00:00:00') -3 and {0} <= to_date({1},'yyyy-mm-dd 23:59:59') -3  ", 
                                    Field, MyConst.CurrentDate);
                                break;
                            case EnumCustomDateType.Last4Days:
                                return string.Format("{0} >= to_date({1},'yyyy-mm-dd 00:00:00') -4 and {0} <= to_date({1},'yyyy-mm-dd 23:59:59') -4  ",
                                    Field, MyConst.CurrentDate);
                                break;
                            case EnumCustomDateType.Today:
                                return string.Format("{0} >= to_date({1},'yyyy-mm-dd 00:00:00') and {0} <= to_date({1},'yyyy-mm-dd 23:59:59') ", 
                                    Field, MyConst.CurrentDate);
                                break;
                            case EnumCustomDateType.Yesterday:
                                return string.Format("{0} >= to_date({1},'yyyy-mm-dd 00:00:00')-1 and {0} <= to_date({1},'yyyy-mm-dd 23:59:59')-1 ",
                                    Field, MyConst.CurrentDate);
                                break;
                            case EnumCustomDateType.Recently3Days:
                                return string.Format(
                                    "{0} >= to_date({1},'yyyy-mm-dd 00:00:00')-3 and {0} <= to_date({1},'yyyy-mm-dd 23:59:59') ",
                                    Field, MyConst.CurrentDate);
                                break;
                            case EnumCustomDateType.Recently7Days:
                                return string.Format(
                                    "{0} >= to_date({1},'yyyy-mm-dd 00:00:00')-7 and {0} <= to_date({1},'yyyy-mm-dd 23:59:59') ",
                                    Field, MyConst.CurrentDate);
                                break;
                            case EnumCustomDateType.LastMonth:
                                return string.Format(
                                    "{0} >= to_date({1},'yyyy-mm-01 00:00:00') and {0} <= to_date({2},'yyyy-mm-dd 23:59:59') ",
                                    Field, MyConst.CurrentDate.AddMonths(1 - MyConst.CurrentDate.Day), MyConst.CurrentDate.AddMonths( - MyConst.CurrentDate.Day));
                                break;
                            case EnumCustomDateType.ThisMonth:
                                return string.Format(
                                    "{0} >= to_date({1},'yyyy-mm-01 00:00:00') and {0} <= to_date({1},'yyyy-mm-dd 23:59:59') ",
                                    Field, MyConst.CurrentDate);
                                break;
                           case EnumCustomDateType.ThisYear:
                                return string.Format(
                                    "{0} >= to_date({1},'yyyy-01-01 00:00:00') and {0} <= to_date({1},'yyyy-mm-dd 23:59:59') ",
                                    Field, MyConst.CurrentDate);
                               break;
                            
                    }
                    break;
                case EnumDatabaseType.SQLSERVER:
                    switch (DateT)
                    {
                        case EnumCustomDateType.Last3Days:
                            return string.Format("DateDiff(dd,{0},{1})=3",
                                Field, MyConst.CurrentDate);
                            break;
                        case EnumCustomDateType.Last4Days:
                            return string.Format("DateDiff(dd,{0},{1})=4",
                                Field, MyConst.CurrentDate);
                            break;
                        case EnumCustomDateType.Today:
                            return string.Format("DateDiff(dd,{0},{1})=0",
                                Field, MyConst.CurrentDate);
                            break;
                        case EnumCustomDateType.Yesterday:
                            return string.Format("DateDiff(dd,{0},{1})=1",
                                Field, MyConst.CurrentDate);
                            break;
                        case EnumCustomDateType.Recently3Days:
                            return string.Format("DateDiff(dd,{0},{1})<=3",
                                Field, MyConst.CurrentDate);
                            break;
                        case EnumCustomDateType.Recently7Days:
                            return string.Format("DateDiff(dd,{0},{1})<=7",
                                Field, MyConst.CurrentDate);
                            break;
                        case EnumCustomDateType.LastMonth:
                            return string.Format(
                                "month{0} = month{1}",
                                Field, MyConst.CurrentDate.AddMonths(-1));
                            break;
                        case EnumCustomDateType.ThisMonth:
                            return string.Format(
                                "month{0} = month{1}",
                                Field, MyConst.CurrentDate);
                            break;
                        case EnumCustomDateType.ThisYear:
                            return string.Format(
                                "year{0} = year{1}",
                                Field, MyConst.CurrentDate);
                            break;

                    }
                    break;

            }
            return "";
        }

        #region 判断转换

        public static bool IsDateTime(object date)
        {
            bool flag = true;

            try
            {
                Convert.ToDateTime(date);
            }
            catch
            {
                flag = false;
            }

            return flag;
        }

        public static bool IsInt(object date)
        {
            bool flag = true;

            try
            {
                Convert.ToInt32(date);
            }
            catch
            {
                flag = false;
            }

            return flag;
        }

        public static bool IsFloat(object date)
        {
            bool flag = true;

            try
            {
                Convert.ToSingle(date);
            }
            catch
            {
                flag = false;
            }

            return flag;
        }

        public static bool IsToDecimal(object date)
        {
            bool flag = true;

            try
            {
                Convert.ToDecimal(date);
            }
            catch
            {
                flag = false;
            }

            return flag;
        }

        public static bool IsBool(object date)
        {
            bool flag = true;

            try
            {
                Convert.ToBoolean(date);
            }
            catch
            {
                flag = false;
            }

            return flag;
        }
        #endregion 判断转换
    }
}