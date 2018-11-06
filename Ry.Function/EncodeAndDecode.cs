using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace  Ry.Function
{
    #region 加密解密
    public class EncodeAndDecode
    {

        /// <summary>
        /// 有密钥的MD5加密
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMD5String(string str, string key)
        {
            string PW = "";

            byte[] OldStr = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] NewStr;

            HMACMD5 md5 = new HMACMD5();
            md5.Key = System.Text.Encoding.UTF8.GetBytes(key);
            NewStr = md5.ComputeHash(OldStr);

            if (NewStr.Length > 0)
            {
                PW = BitConverter.ToString(NewStr);
            }
            return PW;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>加密后的字符串</returns>
        private static string GetMD5String(string str)
        {
            string PW = "";

            byte[] OldStr = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] NewStr;

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            NewStr = md5.ComputeHash(OldStr);

            if (NewStr.Length > 0)
            {
                PW = BitConverter.ToString(NewStr);
            }
            return PW;
        }


        /// <summary>
        /// 对文件进行加密
        /// </summary>
        /// <param name="inName">输入文件名</param>
        /// <param name="outName">加密后的文件名</param>
        /// <param name="desKey"></param>
        /// <param name="desIV"></param>
        private static void EncryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV),

    CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        //解密文件
        private static void DecryptData(String inName, String outName, byte[] desKey, byte[]
    desIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV),

    CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }





        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }


        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        
        public static string ryEncodeEx1(string value)
        {
            byte[] aaa = System.Text.UnicodeEncoding.UTF8.GetBytes(value);

            for (uint i = 0; i < aaa.Length; i++)
            {

                uint m = (uint)(aaa[i] + 1);
                if (m == 256)
                    m = 0;
                aaa[i] = (byte)m;
            }
            return System.Text.UnicodeEncoding.UTF8.GetString(aaa);
        }

        public static string ryDecodeEx1(string value)
        {
            byte[] aaa = System.Text.UnicodeEncoding.UTF8.GetBytes(value);

            for (uint i = 0; i < aaa.Length; i++)
            {

                uint m = (uint)(aaa[i] -1);
                if (m == 0)
                      m = 256;
                aaa[i] = (byte)m;
            }
            return System.Text.UnicodeEncoding.UTF8.GetString(aaa);
        }

        public static byte[] CharArrayToByteArray(char[] array)
        {
            byte[] ss = new byte[array.Length*2];
            for (int i = 0,j = 0 ;i< array.Length ; i++, j++)
            {
                ss[j++] = (byte) array[i];
                ss[j] = (byte) ((array[i]) >> 8);
            }
            return ss;
        }

        public static char[] ByteArrayToCharArray(byte[] array)
        {
            char[] ss = new char[array.Length / 2];
            for (int i = 0, j = 0; i < ss.Length; i++, j++)
            {
                ss[i] = (char)((array[j + 1]) << 8);
                ss[i] |= (char)(array[j++] & 0xFF);
                
            }
            return ss;
        }

        public static string ryEncodeEx(string value,int bit)
        {
            int x =bit % 8;
            char[] aaa = value.ToCharArray();
            for (uint i = 0; i < aaa.Length; i++)
            {

                uint m = (uint)(aaa[i] << x);
                uint a = (uint)(m & 0xFFFF);
                a |= (uint)(m >> 16);
                aaa[i] = (char)a;
            }
            return new string(aaa);
        }

        public static string ryDecodeEx(string value, int bit)
        {
            int x = bit % 8;
            char[] aaa = value.ToCharArray();
            for (uint i = 0; i < aaa.Length; i++)
            {
                uint m = (uint)(aaa[i] << 16);
                m = (uint)(m >> x);


                uint a = (uint)(m & 0xFFFF);

                a |= (uint)(m >> 16);

                aaa[i] = (char)a;
            }
            return new string(aaa);
        }


        public static string ryEncode(string value)
        {
            char[] aaa = value.ToCharArray();
            for (short i = 0 ; i < aaa.Length ; i ++)
            {

                ushort m = (ushort)(aaa[i] << 2);
                ushort a = (ushort)(m & 0xFF);
                a |= (ushort)(m >> 8);
                aaa[i] = (char)a;
            }
            return new string(aaa);
        }

        public static string ryDecode(string value)
        {
            char[] aaa = value.ToCharArray();
            for (int i = 0; i < aaa.Length; i++)
            {
                ushort m = (ushort)(aaa[i] << 8);
                m = (ushort)(m >> 2);


                ushort a = (ushort)(m & 0xFF);

                a |= (ushort)(m >> 8);

                aaa[i] = (char)a;
            }
            return new string(aaa);
        }



    #endregion 加密解密
    }
}