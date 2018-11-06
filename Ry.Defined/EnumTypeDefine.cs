namespace Ry.Defined
{

    public enum Result:int {Success = 0,Failed = 1}

    /// <summary>
    /// 数据库操作方式
    /// </summary>
    public enum EnumDBOperate :int
    {
        Null   = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
        Query  = 4,
        OrderBy = 5,
        SaveInsert = 6,
        SaveUpdate = 7,
        SaveOrderBy = 8
    }


    /// <summary>
    /// 系统设置级别 
    /// </summary>
    public enum EnumSettingLevel : int { System = 1, Localhost = 2, User = 3 }
    
    public partial struct User 
    {
        public string Role;
        public string Authority;
        public string Name;
        public string UserID;
        public bool CheckAuthority(string RightName)
        {
            if ((Authority+",").IndexOf(RightName+",")>=0)
            {
                return true;
            }
            return false;
        }
    }


    public enum EnumDatabaseType
    {
        ORACLE = 0,
        SQLSERVER = 1,
        SQLITE = 2,
        ACCESS = 3,
        MYSQL = 4
    } ;


    public enum EnumCustomDateType : int
    {
        Today = 1,
        Yesterday = 2,
        Last3Days = 3,
        Last4Days = 4,
        Recently3Days = 5,
        Recently7Days = 6,
        ThisMonth = 7,
        LastMonth = 8,
        ThisYear = 9,
        LastYear = 10,
        Custom = 11
    }

    


}
