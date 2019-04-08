using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mode
{
    public class RetrunObj
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public List<DoctorInfo> Data { get; set; }

        public RetrunObj(List<DoctorInfo> DoctorInfos)
        {
            Data = DoctorInfos;
            Code = "1";
            Msg = "成功!";
        }
        public RetrunObj(string code,string msg,List<DoctorInfo> DoctorInfos)
        {
            Data = DoctorInfos;
            Code = code;
            Msg = msg;
        }

        public RetrunObj(string msg)
        {
            Code = "0"; //失败
            Msg = msg;
        }
    }
}
