using System.IO;

namespace Ry.Function
{

    public struct ServerInfo
    {
        public string ServerName;
        public string ServerIP;
        public string IPAddress;
        public int Port;
        public string UserName;
        public string Passwords;
        public string RootPath;
    }

    abstract class  FileCommunicate
    {
        public const int SUCCESS = 0;
        public const int FAILED = 1 ;
        public abstract int ConnectServer(ServerInfo svr);

        public abstract int DisConnect();

        public abstract int UploadFile(string strFormFileName, string strToFileName);

        public abstract int DownloadFile(string strFormFileName, string strToFileName);
    }

    class LocalhostFile : FileCommunicate
    {
        private ServerInfo configInfo;
        public override int ConnectServer(ServerInfo svr)
        {
            configInfo = svr;
            return SUCCESS;
        }

        public override int UploadFile(string strFormFileName, string strToFileName)
        {
            File.Copy(strFormFileName, strToFileName,true);
            return SUCCESS;
        }

        public override int DownloadFile(string strFormFileName, string strToFileName)
        {
            File.Copy(strFormFileName, strToFileName, true);
            return SUCCESS;
        }

        public override int DisConnect()
        {
            return SUCCESS;
        }


    }


    class FTPFile : FileCommunicate
    {
        private FTPClient ftp = new FTPClient();
        public override int ConnectServer(ServerInfo svr)
        {
            ftp.RemoteHost = svr.ServerIP;
            ftp.RemotePort = svr.Port;
            ftp.RemoteUser = svr.UserName;
            ftp.RemotePass = svr.Passwords;
            ftp.RemotePath = svr.RootPath;
            ftp.Connect();
            ftp.PrepareDir(svr.RootPath,true,true);
            ftp.ChDir(svr.RootPath);
            return SUCCESS;
        }

        public override int UploadFile(string strFormFileName, string strToFileName)
        {
            ftp.Put(strFormFileName);
            return SUCCESS;
        }

        public override int DownloadFile(string strFormFileName, string strToFileName)
        {
            string strPath = strToFileName.Substring(0, strToFileName.LastIndexOf("\\"));
            string strName = strToFileName.Substring(strToFileName.LastIndexOf("\\")+1,strToFileName.Length);
            ftp.Get(strFormFileName, strPath, strName);
            return SUCCESS;
        }

        public override int DisConnect()
        {
            ftp.DisConnect();
            
            return SUCCESS;
        }


    }
}
