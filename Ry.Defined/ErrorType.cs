using System;
using System.Collections;
using System.Collections.Generic;

namespace Ry.Defined.ErrorType
{
    public enum EnumHintType : int
    {
        Warning = 0,
        Error = 1,
        Hint = 2
    }

    public struct ErrorInfo
    {
        public EnumHintType ErrorType;
        public int ErrorID;
        public string ErrorString;
    }


    public struct MyError
    {  
       public string ErrorSystem;
       public string ErrorSite;
       public int ErrorID;
       public string ErrorHint;
       public List<ErrorInfo> ErrorList;

        public void InitErrorList(List<ErrorInfo> value)
        {
            ErrorList = value;
        }

       public void SetError(int errorID, string errSite, string errSystem, string ErrorHint)
       {
           ErrorSystem = errSystem;
           ErrorSite = errSite;
           this.ErrorID = errorID;
           this.ErrorHint = ErrorHint;
       }


       public void AddSite(string errSite)
       {
           ErrorSite = errSite + ErrorSite;
       }

        public string ErrorString
        {
            get
            {
                foreach (ErrorInfo v in ErrorList)
                {
                    if (v.ErrorID == ErrorID)
                        return v.ErrorString;
                }
                return String.Format("对应的错误ID:{0}没有找到对应的错误内容！",ErrorID);
            }
        }

    }


}
