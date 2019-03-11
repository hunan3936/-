using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mode
{
    public class SpecialityInfo
    {
        /// <summary>
        /// 人员流水号
        /// </summary>
        public string DoctorID { get; set; }
        /// <summary>
        /// 跟师学习地点
        /// </summary>
        public string StudyAddress { get; set; }
        /// <summary>
        /// 跟师学习开始日期
        /// </summary>
        public DateTime? StudyStartTime { get; set; }
        /// <summary>
        /// 跟师学习结束日期
        /// </summary>
        public DateTime? StudyEndTime { get; set; }
        /// <summary>
        /// 近五年服务人数
        /// </summary>
        public int ServiceNum { get; set; }
        /// <summary>
        /// 文化学习经历
        /// </summary>
        public string LearnExperience { get; set; }
        /// <summary>
        /// 跟师学习医术及实践经历
        /// </summary>
        public string PracticalExperience { get; set; }
        /// <summary>
        /// -医术专长综述
        /// </summary>
        public string MedicalSkill { get; set; }
        /// <summary>
        /// 回顾性中医医术实践资料5例
        /// </summary>
        public string HistoricalRecords { get; set; }
    }
}
