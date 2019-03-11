using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyLog;
using Common;
using Controller;

namespace WebServer
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        private static MyLog.MyLog log = new MyLog.MyLog();
        [WebMethod(Description = "医护人员信息上传")]
        public string UploadDoctorInfo(string inParam)
        {
            ReturnValue frv = new ReturnValue();
            try
            {
                frv.IPAddress = GetWebIPAddress.GetIPAddress;
                frv.inputStr = inParam;
                frv.funName = MyTool.MethodInfo();
                DoctorInfoControll doctorInfo = new DoctorInfoControll();
                ReturnValue rv = doctorInfo.DoctorInfoFun(inParam);
                frv.isOk = rv.isOk;
                frv.msg = rv.msg;
                frv.outputStr = rv.outputStr;
                
            }
            catch (Exception ex)
            {
                frv.isOk = ReturnCode.异常;
                frv.msg = ex.Message;
            }
            finally
            {
                frv.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                log.WriteLog(frv);
            }
            return frv.outputStr;
        }
    }
}
