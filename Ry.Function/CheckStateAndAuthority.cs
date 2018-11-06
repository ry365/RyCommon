namespace Ry.Function
{
    public class CheckStateAndAuthority
    {

        public static bool CanOperate(string authority, string code)
        {
            string[] codelist = authority.Split(',');
            foreach (string cd in codelist)
            {
                if (cd == code)
                    return true;
            }
            return false;
        }

        public static bool CanOperate(string authority, string code, string deviceName, string PartName)
        {
            return false;
        }

    }
}
