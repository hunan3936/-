using NUnit.Framework;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mode;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        [Test()]
        public void TestObject()
        {
            try
            {
                string str = "<Request><Header><SendTime>2016-01-01-02:02:02.222</SendTime><MsgID>20160101020202</MsgID></Header><Body><DoctorInfo><DoctorID>001测试</DoctorID><AccessID></AccessID><OrgID>01</OrgID><DoctorName>测试</DoctorName><IDCardNo></IDCardNo><Birthday>2018-09-08</Birthday><Sex>男</Sex><Mobile></Mobile><Phone></Phone><Email></Email><Postcode></Postcode><DoctorType></DoctorType><DoctorTitle></DoctorTitle><DoctorProfile></DoctorProfile><Photo></Photo><MZ>1</MZ><CensusProvince></CensusProvince><CensusCity></CensusCity><CensusCounty></CensusCounty><CensusStreet></CensusStreet><CensusDetailAddress></CensusDetailAddress><CurrentProvince></CurrentProvince><CurrentCity></CurrentCity><CurrentCounty></CurrentCounty><CurrentStreet></CurrentStreet><CurrentDetailAddress></CurrentDetailAddress><MaritalStatus></MaritalStatus><EducationLevel></EducationLevel><School></School><PoliticalStatus></PoliticalStatus><HealthyStatic></HealthyStatic><IsSpecialty></IsSpecialty><MedicalSkill></MedicalSkill><DataSrc></DataSrc><PYM></PYM><WBM></WBM><Status></Status><IsValid></IsValid><CreaterID></CreaterID><CreaterName></CreaterName><CreaterTime>2019-01-11 11:01:01.333</CreaterTime><ModifierID></ModifierID><ModifierName></ModifierName><ModifyTime></ModifyTime><DeleterID></DeleterID><DeleterName></DeleterName><DeleterTime></DeleterTime><Remarks></Remarks><SpecialityInfo><DoctorID>001测试001</DoctorID><StudyAddress></StudyAddress><StudyStartTime></StudyStartTime><StudyEndTime></StudyEndTime><ServiceNum></ServiceNum><LearnExperience></LearnExperience><PracticalExperience></PracticalExperience><MedicalSkill></MedicalSkill><HistoricalRecords></HistoricalRecords></SpecialityInfo></DoctorInfo></Body></Request>";

                string str02 = "<Request><Header><MsgID /><SendTime>2016-01-01-02:02:02.222</SendTime></Header><Body></Body></Request>";

                StringReader sr = new StringReader(str02);
                XmlReader xmlReader = XmlReader.Create(sr);
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                    }
                    string str01 = xmlReader.Name;

                }
                xmlReader?.Close();
                sr?.Dispose();

            }
            catch (Exception ex)
            {
            }
        }

        [Test()]
        public void TestXML()
        {
            try
            {

                

                testClass info = new testClass();

                info.Str = string.Empty;
                
                info.SpecialList.Add(new testProsen());

                testProsen test = new testProsen();
                test.Name = "测试";
                test.Age = 10;
                DateTime dt = new DateTime(1988, 8, 8);
                dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
                test.Birthday = dt;
                
                info.SpecialList.Add(test);

                string str02 = XmlHandle.JsonToString(info);
                //{\"Str\":\"\",\"CreateTime\":null,\"SpecialList\":[{\"Name\":null,\"Age\":0,\"Birthday\":null},{\"Name\":\"测试\",\"Age\":10,\"Birthday\":\"1988-08-08 00:00:00.000\"}]}


                string str0001 = "{\"CreateTime\":null,\"SpecialList\":[{\"Age\":0,\"Birthday\":null},{\"Name\":\"测试\",\"Age\":10,\"Birthday\":\"1988-08-08 00:00:00.000\"}]}";
                testClass obj0001 = XmlHandle.JsonToObject<testClass>(str0001);
                testClass t01 = XmlHandle.JsonToObject<testClass>(str02);

                DateTime dt02 = DateTime.Now;


                string   str001 = XmlHandle.Serializer(info);




                string str = XmlHandle.Serializer(info.GetType(), info);


                



                 testClass  t = (testClass) XmlHandle.Deserialize(info.GetType(), str);

                testClass t1 = (testClass)XmlHandle.Deserialize(info.GetType(), str02);
            }
            catch (Exception ex)
            {
            }
        }



    }

    [Serializable]
    public class testClass
    {
        [XmlElement(IsNullable = true, Namespace = "",Form = XmlSchemaForm.None)]
        public string Str { get; set; }

        public DateTime? CreateTime { get; set; }

        private List<testProsen> _SpecialList;
        
        public List<testProsen> SpecialList
        {
            get { if (_SpecialList == null) _SpecialList = new List<testProsen>(); return _SpecialList; }
            set { _SpecialList = value; }
        }
    }

    public class testProsen
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public DateTime? Birthday { get; set; }
    }

    public class XmlHandle
    {
        #region 反序列化  
        /// <summary>  
        /// 反序列化  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="xml">XML字符串</param>  
        /// <returns></returns>  
        public static object Deserialize(Type type, string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }

        public static T Deserialize<T>(string xml)
        {
            return (T)Deserialize(typeof(T), xml);
        }

        /// <summary>  
        /// 反序列化  
        /// </summary>  
        /// <param name="type"></param>  
        /// <param name="xml"></param>  
        /// <returns></returns>  
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }

        public static T Deserialize<T>( Stream stream)
        {
            return (T)Deserialize(typeof(T), stream);
        }
        #endregion

        #region 序列化  
        /// <summary>  
        /// 序列化  
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="obj">对象</param>  
        /// <returns></returns>  
        public static string Serializer(object obj)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //Add an empty namespace and empty value
            ns.Add(string.Empty, string.Empty);
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            string str = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                xs.Serialize(ms, obj, ns);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    str = sr.ReadToEnd();
                }
            }
            return str;
        }



        public static string Serializer(Type type, object obj)
        {
            XmlSerializer xml = new XmlSerializer(type);
            string str = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                xml.Serialize(ms, obj);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                     str = sr.ReadToEnd();
                }
            }
            return str;
        }
        #endregion


        #region
        public static string JsonToString(object obj)
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            //iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.FFFFFFFK";
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            iso.DateTimeStyles = System.Globalization.DateTimeStyles.AssumeLocal;
            return  JsonConvert.SerializeObject(obj,iso);
        }

        public static T JsonToObject<T>(string jsonStr)
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            //iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.FFFFFFFK";
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            iso.DateTimeStyles = System.Globalization.DateTimeStyles.AssumeLocal;
            return JsonConvert.DeserializeObject<T>(jsonStr, iso);
        }
        #endregion
    }
}