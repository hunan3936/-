using NUnit.Framework;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller.Tests
{
    [TestFixture()]
    public class DoctorInfoControllTests
    {
        [Test()]
        public void DoctorInfoFunTest()
        {
            string str = "<Request><Header><SendTime>2016-01-01-02:02:02.222</SendTime><MsgID>20160101020202</MsgID></Header><DoctorInfo><DoctorID>001测试</DoctorID><AccessID></AccessID><OrgID>01</OrgID><DoctorName>测试</DoctorName><IDCardNo></IDCardNo><Birthday>2018-09-08</Birthday><Sex>男</Sex><Mobile></Mobile><Phone></Phone><Email></Email><Postcode></Postcode><DoctorType></DoctorType><DoctorTitle></DoctorTitle><DoctorProfile></DoctorProfile><Photo></Photo><MZ>1</MZ><CensusProvince></CensusProvince><CensusCity></CensusCity><CensusCounty></CensusCounty><CensusStreet></CensusStreet><CensusDetailAddress></CensusDetailAddress><CurrentProvince></CurrentProvince><CurrentCity></CurrentCity><CurrentCounty></CurrentCounty><CurrentStreet></CurrentStreet><CurrentDetailAddress></CurrentDetailAddress><MaritalStatus></MaritalStatus><EducationLevel></EducationLevel><School></School><PoliticalStatus></PoliticalStatus><HealthyStatic></HealthyStatic><IsSpecialty></IsSpecialty><MedicalSkill></MedicalSkill><DataSrc></DataSrc><PYM></PYM><WBM></WBM><Status></Status><IsValid></IsValid><CreaterID></CreaterID><CreaterName></CreaterName><CreaterTime>2019-01-11 11:01:01.333</CreaterTime><ModifierID></ModifierID><ModifierName></ModifierName><ModifyTime></ModifyTime><DeleterID></DeleterID><DeleterName></DeleterName><DeleterTime></DeleterTime><Remarks></Remarks><SpecialityInfo><DoctorID>001测试</DoctorID><StudyAddress></StudyAddress><StudyStartTime></StudyStartTime><StudyEndTime></StudyEndTime><ServiceNum></ServiceNum><LearnExperience></LearnExperience><PracticalExperience></PracticalExperience><MedicalSkill></MedicalSkill><HistoricalRecords></HistoricalRecords></SpecialityInfo></DoctorInfo></Request>";
            DoctorInfoControll doc = new DoctorInfoControll();
            string outStr = doc.DoctorInfoFun(str).outputStr;
            if (outStr != null)
            {
            }
        }

        public void TestObject()
        {
            try
            {
                string str = "<Request><Header><SendTime>2016-01-01-02:02:02.222</SendTime><MsgID>20160101020202</MsgID></Header><DoctorInfo><DoctorID>001测试</DoctorID><AccessID></AccessID><OrgID>01</OrgID><DoctorName>测试</DoctorName><IDCardNo></IDCardNo><Birthday>2018-09-08</Birthday><Sex>男</Sex><Mobile></Mobile><Phone></Phone><Email></Email><Postcode></Postcode><DoctorType></DoctorType><DoctorTitle></DoctorTitle><DoctorProfile></DoctorProfile><Photo></Photo><MZ>1</MZ><CensusProvince></CensusProvince><CensusCity></CensusCity><CensusCounty></CensusCounty><CensusStreet></CensusStreet><CensusDetailAddress></CensusDetailAddress><CurrentProvince></CurrentProvince><CurrentCity></CurrentCity><CurrentCounty></CurrentCounty><CurrentStreet></CurrentStreet><CurrentDetailAddress></CurrentDetailAddress><MaritalStatus></MaritalStatus><EducationLevel></EducationLevel><School></School><PoliticalStatus></PoliticalStatus><HealthyStatic></HealthyStatic><IsSpecialty></IsSpecialty><MedicalSkill></MedicalSkill><DataSrc></DataSrc><PYM></PYM><WBM></WBM><Status></Status><IsValid></IsValid><CreaterID></CreaterID><CreaterName></CreaterName><CreaterTime>2019-01-11 11:01:01.333</CreaterTime><ModifierID></ModifierID><ModifierName></ModifierName><ModifyTime></ModifyTime><DeleterID></DeleterID><DeleterName></DeleterName><DeleterTime></DeleterTime><Remarks></Remarks><SpecialityInfo><DoctorID>001测试</DoctorID><StudyAddress></StudyAddress><StudyStartTime></StudyStartTime><StudyEndTime></StudyEndTime><ServiceNum></ServiceNum><LearnExperience></LearnExperience><PracticalExperience></PracticalExperience><MedicalSkill></MedicalSkill><HistoricalRecords></HistoricalRecords></SpecialityInfo></DoctorInfo></Request>";
                
            }
            catch (Exception ex)
            {
            }
        }
    }
}