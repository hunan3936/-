using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Mode;
using System.Xml;
using DataAccessDll;
using System.Data.SqlTypes;

namespace Controller
{
    public class DoctorInfoControll
    {
        private MyLog.MyLog log = new MyLog.MyLog();
        public ReturnValue DoctorInfoFun(string inParam)
        {
            ReturnValue frv = new ReturnValue();
            #region
            string outStr = @"<Respnse>
                                <Header>
                                  <SendTime>{0}</SendTime>
                                  <MsgID>{1}</MsgID>
                                  <Code>{2}</Code>
                                  <Desc>{3}</Desc>
                                 </Header>
                                </Respnse>";
            #endregion
            string MsgID = string.Empty;
            try
            {
                frv.inputStr = inParam;
                frv.funName = MyTool.MethodInfo();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(inParam);
                MsgID = doc.SelectSingleNode("Request/Header/MsgID").InnerText;
                List<DoctorInfo> doctorInfos = new List<DoctorInfo>();
                XmlHandle(doc, doctorInfos);
                //存储数据
                GetSqlToDataBase(doctorInfos);
                frv.isOk = ReturnCode.成功;
            }
            catch (Exception ex)
            {
                frv.isOk = ReturnCode.异常;
                frv.msg ="异常："+ ex.Message;
            }
            finally
            {
                string msg = frv.isOk == ReturnCode.成功 ? frv.isOk.ToString() : frv.msg;
                frv.outputStr = string.Format(outStr, DateTime.Now.ToString("yyyyMMddHHmmssfff"), MsgID, (int)frv.isOk, msg);
                frv.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                log.WriteLog(frv);
            }
            return frv;
        }

        private void GetSqlToDataBase(List<DoctorInfo> doctorInfos)
        {
            #region
            string DoctorInfoSql = string.Empty;
            string SpecialityInfoSql = string.Empty;
            if (DataAccess.dBType == DBType.OracleDB)
            {
                DoctorInfoSql = @"insert into  DoctorInfo(
                       DoctorID,AccessID,OrgID,IDCardNo,DoctorName,Birthday,Sex,Mobile,Phone,Email,
                       Postcode,DoctorType,DoctorTitle,DoctorProfile,Photo,MZ,CensusProvince,
                       CensusCity,CensusCounty,CensusStreet,CensusDetailAddress,CurrentProvince,
                       CurrentCity,CurrentCounty,CurrentStreet,CurrentDetailAddress,MaritalStatus,
                       EducationLevel,School,PoliticalStatus,HealthyStatic,IsSpecialty,MedicalSkill,
                       DataSrc,PYM,WBM,Status,IsValid,CreaterID,CreaterName,CreaterTime,ModifierID,ModifierName,
                       ModifyTime,DeleterID,DeleterName,DeleterTime,Remarks 
                       ) values(
                       :DoctorID,:AccessID,:OrgID,:IDCardNo,:DoctorName,:Birthday,:Sex,:Mobile,:Phone,:Email,
                       :Postcode,:DoctorType,:DoctorTitle,:DoctorProfile,:Photo,:MZ,:CensusProvince,
                       :CensusCity,:CensusCounty,:CensusStreet,:CensusDetailAddress,:CurrentProvince,
                       :CurrentCity,:CurrentCounty,:CurrentStreet,:CurrentDetailAddress,:MaritalStatus,
                       :EducationLevel,:School,:PoliticalStatus,:HealthyStatic,:IsSpecialty,:MedicalSkill,
                       :DataSrc,:PYM,:WBM,:Status,:IsValid,:CreaterID,:CreaterName,:CreaterTime,:ModifierID,:ModifierName,
                       :ModifyTime,:DeleterID,:DeleterName,:DeleterTime,:Remarks 
                       )";
                SpecialityInfoSql = @"insert into  SpecialityInfo(
                       DoctorID,StudyAddress,StudyStartTime,StudyEndTime,ServiceNum,LearnExperience,PracticalExperience,
                       MedicalSkill,HistoricalRecords
                       ) values(
                       :DoctorID,:StudyAddress,:StudyStartTime,:StudyEndTime,:ServiceNum,:LearnExperience,:PracticalExperience,
                       :MedicalSkill,:HistoricalRecords
                       )";
            }
            else
            {

                DoctorInfoSql = @"insert into  DoctorInfo(
                       DoctorID,AccessID,OrgID,IDCardNo,DoctorName,Birthday,Sex,Mobile,Phone,Email,
                       Postcode,DoctorType,DoctorTitle,DoctorProfile,Photo,MZ,CensusProvince,
                       CensusCity,CensusCounty,CensusStreet,CensusDetailAddress,CurrentProvince,
                       CurrentCity,CurrentCounty,CurrentStreet,CurrentDetailAddress,MaritalStatus,
                       EducationLevel,School,PoliticalStatus,HealthyStatic,IsSpecialty,MedicalSkill,
                       DataSrc,PYM,WBM,Status,IsValid,CreaterID,CreaterName,CreaterTime,ModifierID,ModifierName,
                       ModifyTime,DeleterID,DeleterName,DeleterTime,Remarks 
                       ) values(
                       @DoctorID,@AccessID,@OrgID,@IDCardNo,@DoctorName,@Birthday,@Sex,@Mobile,@Phone,@Email,
                       @Postcode,@DoctorType,@DoctorTitle,@DoctorProfile,@Photo,@MZ,@CensusProvince,
                       @CensusCity,@CensusCounty,@CensusStreet,@CensusDetailAddress,@CurrentProvince,
                       @CurrentCity,@CurrentCounty,@CurrentStreet,@CurrentDetailAddress,@MaritalStatus,
                       @EducationLevel,@School,@PoliticalStatus,@HealthyStatic,@IsSpecialty,@MedicalSkill,
                       @DataSrc,@PYM,@WBM,@Status,@IsValid,@CreaterID,@CreaterName,@CreaterTime,@ModifierID,@ModifierName,
                       @ModifyTime,@DeleterID,@DeleterName,@DeleterTime,@Remarks 
                       )";
                SpecialityInfoSql = @"insert into  SpecialityInfo(
                       DoctorID,StudyAddress,StudyStartTime,StudyEndTime,ServiceNum,LearnExperience,PracticalExperience,
                       MedicalSkill,HistoricalRecords
                       ) values(
                       @DoctorID,@StudyAddress,@StudyStartTime,@StudyEndTime,@ServiceNum,@LearnExperience,@PracticalExperience,
                       @MedicalSkill,@HistoricalRecords
                       )";
            }
            #endregion

            DataAccess da = new DataAccess();
            try
            {
                if (da.StratTran())
                {
                    foreach (DoctorInfo info in doctorInfos)
                    {
                        int ret = da.ExecuteSqlTran(DoctorInfoSql, info);
                        if (ret < 0)
                            throw new Exception();
                        if (info.SpecialList != null && info.SpecialList.Count > 0)
                        {
                            ret = da.ExecuteSqlTran(SpecialityInfoSql, info.SpecialList);
                            if (ret < 0)
                                throw new Exception();
                        }
                    }
                    da.ExecuteSqlTran("", null, true);  //提交事务
                }
            }
            catch (Exception ex)
            {
                da.CloseConTran();
                throw new Exception(ex.Message);
            }
        }

        private void XmlHandle(XmlDocument doc ,List<DoctorInfo> doctorInfos)
        {
            XmlNodeList nodeList = doc.SelectNodes("Request/DoctorInfo");
            ValidNodeList(nodeList, "【DoctorInfo】");
            foreach (XmlNode node in nodeList)
            {
                DoctorInfo doctorInfo = new DoctorInfo();
                #region  DoctorInfo  信息
                doctorInfo.DoctorID = GetXmlString(node.SelectSingleNode("DoctorID"), "【DoctorID】",true);
                doctorInfo.AccessID = GetXmlString(node.SelectSingleNode("AccessID"), "【AccessID】",true);
                doctorInfo.OrgID = GetXmlString(node.SelectSingleNode("OrgID"), "【OrgID】");
                doctorInfo.DoctorName = GetXmlString(node.SelectSingleNode("DoctorName"), "【DoctorName】");
                doctorInfo.IDCardNo = GetXmlString(node.SelectSingleNode("IDCardNo"), "【IDCardNo】");
                doctorInfo.Birthday = GetXmlDateTime(node.SelectSingleNode("Birthday"), "【Birthday】");
                doctorInfo.Sex = GetXmlString(node.SelectSingleNode("Sex"), "【Sex】");
                doctorInfo.Mobile = GetXmlString(node.SelectSingleNode("Mobile"), "【Mobile】");
                doctorInfo.Phone = GetXmlString(node.SelectSingleNode("Phone"), "【Phone】");
                doctorInfo.Email = GetXmlString(node.SelectSingleNode("Email"), "【Email】");
                doctorInfo.Postcode = GetXmlString(node.SelectSingleNode("Postcode"), "【Postcode】");
                doctorInfo.DoctorType = GetXmlString(node.SelectSingleNode("DoctorType"), "【DoctorType】");
                doctorInfo.DoctorTitle = GetXmlString(node.SelectSingleNode("DoctorTitle"), "【DoctorTitle】");
                doctorInfo.DoctorProfile = GetXmlString(node.SelectSingleNode("DoctorProfile"), "【DoctorProfile】");
                doctorInfo.Photo = GetXmlString(node.SelectSingleNode("Photo"), "【Photo】");
                doctorInfo.MZ = GetXmlInt(node.SelectSingleNode("MZ"), "【MZ】");
                doctorInfo.CensusProvince = GetXmlString(node.SelectSingleNode("CensusProvince"), "【CensusProvince】");
                doctorInfo.CensusCity = GetXmlString(node.SelectSingleNode("CensusCity"), "【CensusCity】");
                doctorInfo.CensusCounty = GetXmlString(node.SelectSingleNode("CensusCounty"), "【CensusCounty】");
                doctorInfo.CensusStreet = GetXmlString(node.SelectSingleNode("CensusStreet"), "【CensusStreet】");
                doctorInfo.CensusDetailAddress = GetXmlString(node.SelectSingleNode("CensusDetailAddress"), "【CensusDetailAddress】");
                doctorInfo.CurrentProvince = GetXmlString(node.SelectSingleNode("CurrentProvince"), "【CurrentProvince】");
                doctorInfo.CurrentCity = GetXmlString(node.SelectSingleNode("CurrentCity"), "【CurrentCity】");
                doctorInfo.CurrentCounty = GetXmlString(node.SelectSingleNode("CurrentCounty"), "【CurrentCounty】");
                doctorInfo.CurrentStreet = GetXmlString(node.SelectSingleNode("CurrentStreet"), "【CurrentStreet】");
                doctorInfo.CurrentDetailAddress = GetXmlString(node.SelectSingleNode("CurrentDetailAddress"), "【CurrentDetailAddress】");
                doctorInfo.MaritalStatus = GetXmlString(node.SelectSingleNode("MaritalStatus"), "【MaritalStatus】");
                doctorInfo.EducationLevel = GetXmlString(node.SelectSingleNode("EducationLevel"), "【EducationLevel】");
                doctorInfo.School = GetXmlString(node.SelectSingleNode("School"), "【School】");
                doctorInfo.PoliticalStatus = GetXmlString(node.SelectSingleNode("PoliticalStatus"), "【PoliticalStatus】");
                doctorInfo.HealthyStatic = GetXmlString(node.SelectSingleNode("HealthyStatic"), "【HealthyStatic】");        
                doctorInfo.IsSpecialty = GetXmlString(node.SelectSingleNode("IsSpecialty"), "【IsSpecialty】");
                doctorInfo.MedicalSkill = GetXmlString(node.SelectSingleNode("MedicalSkill"), "【MedicalSkill】");
                doctorInfo.DataSrc = GetXmlString(node.SelectSingleNode("DataSrc"), "【DataSrc】");
                doctorInfo.PYM = GetXmlString(node.SelectSingleNode("PYM"), "【PYM】");
                doctorInfo.WBM = GetXmlString(node.SelectSingleNode("WBM"), "【WBM】");
                doctorInfo.Status = GetXmlInt(node.SelectSingleNode("Status"), "【Status】");
                doctorInfo.IsValid = GetXmlInt(node.SelectSingleNode("IsValid"), "【IsValid】");
                doctorInfo.CreaterID = GetXmlString(node.SelectSingleNode("CreaterID"), "【CreaterID】");
                doctorInfo.CreaterName = GetXmlString(node.SelectSingleNode("CreaterName"), "【CreaterName】");
                doctorInfo.CreaterTime = GetXmlDateTime(node.SelectSingleNode("CreaterTime"), "【CreaterTime】");
                doctorInfo.ModifierID = GetXmlString(node.SelectSingleNode("ModifierID"), "【ModifierID】");
                doctorInfo.ModifierName = GetXmlString(node.SelectSingleNode("ModifierName"), "【ModifierName】");
                doctorInfo.ModifyTime = GetXmlDateTime(node.SelectSingleNode("ModifyTime"), "【ModifyTime】");
                doctorInfo.DeleterID = GetXmlString(node.SelectSingleNode("DeleterID"), "【DeleterID】");
                doctorInfo.DeleterName = GetXmlString(node.SelectSingleNode("DeleterName"), "【DeleterName】");
                doctorInfo.DeleterTime = GetXmlDateTime(node.SelectSingleNode("DeleterTime"), "【DeleterTime】");
                doctorInfo.Remarks = GetXmlString(node.SelectSingleNode("Remarks"), "【Remarks】");
                #endregion
                XmlNodeListHandle(node.SelectNodes("SpecialityInfo"), doctorInfo);
                doctorInfos.Add(doctorInfo);
            }
        }

        private void XmlNodeListHandle(XmlNodeList nodeList, DoctorInfo doctorInfo)
        {
            if (ValidNodeList(nodeList, "【SpecialityInfo】", false))
            {
                foreach (XmlNode node in nodeList)
                {
                    SpecialityInfo info = new SpecialityInfo();
                    #region
                    info.DoctorID = GetXmlString(node.SelectSingleNode("DoctorID"), "【DoctorID】",true);
                    info.StudyAddress = GetXmlString(node.SelectSingleNode("StudyAddress"), "【StudyAddress】");
                    info.StudyStartTime = GetXmlDateTime(node.SelectSingleNode("StudyStartTime"), "【StudyStartTime】");
                    info.StudyEndTime = GetXmlDateTime(node.SelectSingleNode("StudyEndTime"), "【StudyEndTime】");
                    info.ServiceNum = GetXmlInt(node.SelectSingleNode("ServiceNum"), "【ServiceNum】");
                    info.LearnExperience = GetXmlString(node.SelectSingleNode("LearnExperience"), "【LearnExperience】");
                    info.PracticalExperience = GetXmlString(node.SelectSingleNode("PracticalExperience"), "【PracticalExperience】");
                    info.MedicalSkill = GetXmlString(node.SelectSingleNode("MedicalSkill"), "【MedicalSkill】");
                    info.HistoricalRecords = GetXmlString(node.SelectSingleNode("HistoricalRecords"), "【HistoricalRecords】");
                    #endregion
                    doctorInfo.SpecialList.Add(info);
                }
            }
        }
        /// <summary>
        /// XML字符串获取，并验证节点是否存在且不为控制
        /// </summary>
        /// <param name="node"></param>
        /// <param name="msg"></param>
        /// <param name="flag">true验证数据</param>
        /// <returns></returns>
        private string GetXmlString(XmlNode node, string msg = "", bool flag = false)
        {
            if (node != null)
            {
                if(flag && string.IsNullOrEmpty(node.InnerText))
                    throw new Exception(msg + "节点为空！");
                return node.InnerText;
            }
            if (flag)
                throw new Exception(msg + "节点不存在！");
            return string.Empty;
        }
        /// <summary>
        /// XML日期字段获取，并验证节点是否存在且合法验证
        /// </summary>
        /// <param name="node"></param>
        /// <param name="msg"></param>
        /// <param name="flag">true验证数据</param>
        /// <returns></returns>
        private DateTime? GetXmlDateTime(XmlNode node, string msg = "", bool flag = false)
        {
            DateTime dt = DateTime.Now;
            if (node != null && DateTime.TryParse(node.InnerText,out dt))
                return dt;
            if (flag)
            {
                string str = node == null ? msg + "节点不存在！" : msg + "节点值不合法！";
                throw new Exception(str);
            }
            return null;
        }
        /// <summary>
        ///  XML整数获取，并验证节点是否存在且合法验
        /// </summary>
        /// <param name="node"></param>
        /// <param name="msg"></param>
        /// <param name="flag">true验证数据</param>
        /// <returns></returns>
        private int GetXmlInt(XmlNode node, string msg = "", bool flag = false)
        {
            int ret =  int.MinValue; 
            if (node != null && int.TryParse(node.InnerText,out ret))
                return ret;
            if (flag)
            {
                string str = node == null ? msg + "节点不存在！" : msg + "节点值不合法！";
                throw new Exception(str);
            }
            return ret;
        }

        /// <summary>
        /// XML节点列表验证
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="msg"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private bool ValidNodeList(XmlNodeList nodeList,string msg ="", bool flag = true)
        {
            if (nodeList != null && nodeList.Count >0)
                return true;
            if (flag)
                throw new Exception(msg+"节点不存在，或者没有子节点！");
            return false;
        }
    }
}
