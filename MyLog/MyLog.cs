using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Common;
using log4net;

namespace MyLog
{
    public class MyLog
    {
        private ILog m_log;

        private const string LOG4NET_CONFIG = "ConfigLog\\Config_log4net.xml";
        static  MyLog()
        {
            XmlDocument doc = new XmlDocument();
            //使用当前dll路径
            string sPath = MyTool.GetAssemblyPath(typeof(MyLog));
            if (!sPath.EndsWith("\\"))
            {
                sPath += "\\";
            }
            sPath += LOG4NET_CONFIG;
            doc.Load(@sPath);
            XmlElement myElement = doc.DocumentElement;
            log4net.Config.XmlConfigurator.Configure(myElement);
        }

        public static string GetAssemblyPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        private ILog GetLog(LogToPostition logPostition)
        {
            m_log = LogManager.GetLogger(logPostition.ToString());
            return m_log;
        }

        public void WriteLog(object obj, LogToPostition logPostition = LogToPostition.LogToFile, LogLevel logLevel = LogLevel.MustLog)
        {
            switch (logLevel)
            {
                case LogLevel.ErrorLog:
                    GetLog(logPostition).Error(obj);
                    break;
                case LogLevel.ExceptionLog:
                    GetLog(logPostition).Fatal(obj);
                    break;
                default:
                    GetLog(logPostition).Info(obj);
                    break;
            }
        }
    }

}
