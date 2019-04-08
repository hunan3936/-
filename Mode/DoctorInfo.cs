using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mode
{
    public class DoctorInfo
    {
        /// <summary>
        /// 人员流水号
        /// </summary>
        public string DoctorID { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string AccessID { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string OrgID { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 性别 0未知的性别1男性2女性9未说明的性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Postcode { get; set; }
        /// <summary>
        /// 角色类型 0 省管理员1 中医馆管理员2 医生3 护士
        /// </summary>
        public string DoctorType { get; set; }
        /// <summary>
        /// 职称/职位
        /// </summary>
        public string DoctorTitle { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string DoctorProfile { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public int MZ { get; set; }
        /// <summary>
        /// 户籍地址-省
        /// </summary>
        public string CensusProvince { get; set; }
        /// <summary>
        /// 户籍地址-市
        /// </summary>
        public string CensusCity { get; set; }
        /// <summary>
        /// 户籍地址-区县
        /// </summary>
        public string CensusCounty { get; set; }
        /// <summary>
        /// 户籍地址-街道(乡镇)
        /// </summary>
        public string CensusStreet { get; set; }
        /// <summary>
        /// 户籍详细地址
        /// </summary>
        public string CensusDetailAddress { get; set; }
        /// <summary>
        /// 现住地址-省
        /// </summary>
        public string CurrentProvince { get; set; }
        /// <summary>
        /// 现住地址-市
        /// </summary>
        public string CurrentCity { get; set; }
        /// <summary>
        /// 现住地址-区县
        /// </summary>
        public string CurrentCounty { get; set; }
        /// <summary>
        /// 现住地址-街道(乡镇)
        /// </summary>
        public string CurrentStreet { get; set; }
        /// <summary>
        /// 现住详细地址
        /// </summary>
        public string CurrentDetailAddress { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string MaritalStatus { get; set; }
        /// <summary>
        /// 文化程度
        /// </summary>
        public string EducationLevel { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 政治面貌01中共党员02中共预备党员03共青团员04民革党员05民盟盟员06民建会员07民进会员08农工党党员09致公党党员10九三学社社员11台盟盟员12无党派人士13群众
        /// </summary>
        public string PoliticalStatus { get; set; }
        /// <summary>
        /// 健康状况
        /// </summary>
        public string HealthyStatic { get; set; }
        /// <summary>
        /// 是否中医医术确有专长人员 0是1否
        /// </summary>
        public string IsSpecialty { get; set; }
        /// <summary>
        /// 医术专长综述
        /// </summary>
        public string MedicalSkill { get; set; }
        /// <summary>
        /// 数据来源0 本系统1 app2 接口3 导入4 外部系统
        /// </summary>
        public string DataSrc { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string PYM { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        public string WBM { get; set; }
        /// <summary>
        /// 删除状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 正常标识
        /// </summary>
        public int IsValid { get; set; }
        /// <summary>
        /// 创建者编码
        /// </summary>
        public string CreaterID { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreaterName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreaterTime { get; set; }
        /// <summary>
        /// 修改者编码
        /// </summary>
        public string ModifierID { get; set; }
        /// <summary>
        /// 修改者姓名
        /// </summary>
        public string ModifierName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 删除者编码
        /// </summary>
        public string DeleterID { get; set; }
        /// <summary>
        /// 删除者姓名
        /// </summary>
        public string DeleterName { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleterTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// OrgName 机构名称    s[50]
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// DepName 科室名称 s[50]
        /// </summary>
        public string DepName { get; set; }
        /// <summary>
        /// DepCode 科室编码    s[50]
        /// </summary>
        public string DepCode { get; set; }
        /// <summary>
        /// 考生是否通过标记  s[20]
        /// </summary>
        public string ValidateFlag { get; set; }


        private List<SpecialityInfo> _SpecialList;
        public List<SpecialityInfo> SpecialList
        {
            get { if (_SpecialList == null) _SpecialList = new List<SpecialityInfo>();return _SpecialList; }
            set { _SpecialList = value; }
        }
    }
}
