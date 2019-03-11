using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ReturnValue
    {
        ReturnCode _isOk;
        string _inputStr;
        string _outputStr;
        object _outObject;
        string _startTime;
        string _endTime;
        LogLevel _logLevel;
        string _msg;
        string _funName;
        string _url;
        string _IPAddress;
        /// <summary>
        /// 成功标记 RenturnCode
        /// </summary>
        public ReturnCode isOk
        {
            get { return _isOk; }
            set { _isOk = value; }
        }
        /// <summary>
        /// 输入参数
        /// </summary>
        public string inputStr
        {
            get { return _inputStr; }
            set { _inputStr = value; }
        }

        /// <summary>
        /// 输出参数
        /// </summary>
        public string outputStr
        {
            get { return _outputStr; }
            set { _outputStr = value; }
        }
        /// <summary>
        /// 返回对象
        /// </summary>
        public object outObject
        {
            get { return _outObject; }
            set { _outObject = value; }
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string startTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string endTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        /// <summary>
        /// 日志级别 LogLevel
        /// </summary>
        public LogLevel logLevel
        {
            get { return _logLevel; }
            set { _logLevel = value; }
        }
        /// <summary>
        /// 返回信息或者错误信息
        /// </summary>
        public string msg
        {
            get { return _msg; }
            set { _msg = value; }
        }
        /// <summary>
        /// 当前方法名称
        /// </summary>
        public string funName
        {
            get { return _funName; }
            set { _funName = value; }
        }


        public string url
        {
            get { return _url; }
            set { _url = value; }
        }


        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        public  ReturnValue()
        {
            _isOk = ReturnCode.失败;
            _inputStr = string.Empty;
            _outputStr = string.Empty;
            _outObject = null;
            _startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            _endTime = string.Empty;
            _logLevel = LogLevel.GeneralLog;
            _msg = string.Empty;
            _funName = string.Empty;
            _url = string.Empty;
            _IPAddress = string.Empty;
        }
    }

    public enum ReturnCode
    {
        失败 = 0,
        成功 = 1,
        异常 = 3,
        超时 = 9,
        
    } 
    public enum LogLevel
    {
        ExceptionLog = 0, //异常日志
        ErrorLog = 1,//错误日志
        MustLog = 3,//必须保存日志
        GeneralLog = 4,//一般日志
        All = 5

    }
    /// <summary>
    /// 必须与Config_log4net文件中的logger节点名字一致
    /// </summary>
    public enum LogToPostition
    {
        LogToFile = 1, //保存到文本
        LogToDatabase = 2, //保存到数据库
        LogToFileAndDatabase = 3  //都保存
    }


    public enum DBType
    {
        SQLService = 0,
        MySQLDB = 1,
        OracleDB = 2,
    }

    public enum WebAccess
    {
        HttpWeb=0,
        Agent =1,
        WebRefer =2,
    }


}
