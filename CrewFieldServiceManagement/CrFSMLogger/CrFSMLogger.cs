using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CrFSMLogger
{
    public class CrFSMLogger
    {
        private static CrFSMLogger instance;
        private static TextWriter txtWrt;
        private static string fullPath = Common.Config.LogPath+"cfms_log.log";


        public static CrFSMLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CrFSMLogger();
                }
                return instance;
            }
        }

        private CrFSMLogger() 
        {
            if (!Directory.Exists(Common.Config.LogPath))
            {
                Directory.CreateDirectory(Common.Config.LogPath);
            }

            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Close();
            }
            

        }
        

        public void WriteToLog(string _message)
        {
            using (txtWrt = new StreamWriter(fullPath, true))
            {
                txtWrt.WriteLine(string.Format("[{0}]: {1}", DateTime.Now, _message));
                txtWrt.Close();
            }
        }

        public void WriteToLog(Exception _exception)
        {
            using (txtWrt = new StreamWriter(fullPath, true))
            {
                while (_exception.InnerException != null)
                {
                    txtWrt.WriteLine(string.Format("[{0}]: {1}", DateTime.Now, _exception.Message));
                    _exception = _exception.InnerException;
                }
                txtWrt.Close();
            }
        }
    }
}
