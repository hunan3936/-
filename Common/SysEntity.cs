using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Common
{
    public class SysEntity
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 接口调用的web地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// web地址类名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 访问的方法
        /// </summary>
        public string FunName { get; set; }
        /// <summary>
        /// 获取数据的数据库连接地址
        /// </summary>
        public string Connecting { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DBType DbType { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Web访问方式
        /// </summary>
        public WebAccess WebAccessMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OutputFormat { get; set; }



    }

    public   class EntityList
    {
        public static List<SysEntity> ListEntityList = new List<SysEntity>();
        private const string EntityListXml = "EntityXml\\EntityListXml.xml";
        static EntityList()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                //使用当前dll路径
                string sPath = MyTool.GetAssemblyPath(typeof(EntityList));
                if (!sPath.EndsWith("\\"))
                {
                    sPath += "\\";
                }
                sPath += EntityListXml;

                doc.Load(@sPath);

                XmlNodeList list = doc.SelectNodes("Root/Item");
                foreach (XmlNode node in list)
                {
                    SysEntity entity = new SysEntity();
                    entity.Name = node.SelectSingleNode("Name") != null ? node.SelectSingleNode("Name").InnerText : "";
                    entity.Connecting = node.SelectSingleNode("Connecting") != null ? node.SelectSingleNode("Connecting").InnerText : "";
                    entity.DbType = node.SelectSingleNode("DbType") != null &&  !string.IsNullOrEmpty(node.SelectSingleNode("DbType").InnerText) ?  (DBType)Enum.Parse(typeof(DBType), node.SelectSingleNode("DbType").InnerText) : DBType.OracleDB;
                    entity.FunName = node.SelectSingleNode("FunName") != null ? node.SelectSingleNode("FunName").InnerText : "";
                 
                    entity.Url = node.SelectSingleNode("Url") != null ? node.SelectSingleNode("Url").InnerText : "";
                    
                    entity.ClassName = node.SelectSingleNode("ClassName") != null ? node.SelectSingleNode("ClassName").InnerText : "";
                    entity.WebAccessMode = node.SelectSingleNode("WebAccessMode") != null && !string.IsNullOrEmpty(node.SelectSingleNode("WebAccessMode").InnerText) ? (WebAccess)Enum.Parse(typeof(WebAccess), node.SelectSingleNode("WebAccessMode").InnerText) : WebAccess.Agent;

                    entity.OutputFormat = ((node.SelectSingleNode("OutputFormat") != null) ? node.SelectSingleNode("OutputFormat").InnerText : "");
                    ListEntityList.Add(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetAssemblyPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }


        public static bool GetEntity( out SysEntity sysEntity,string name)
        {
            bool  reBool = false;
            sysEntity = null;
            foreach (SysEntity obj in ListEntityList)
            {
                if (obj.Name.Equals(name))
                {
                    sysEntity = obj;
                    reBool = true;
                    break;
                }
            }
            return reBool;
        }


        public  SysEntity this[string name]
        {
            get
            {
                foreach (SysEntity entity in ListEntityList)
                {
                    if (entity.Name.Equals(name))
                    {
                        return entity;
                    }
                }
                return null;
            }
           
        }


    }






    public class PrimaryKeyInfo
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string PrimaryKey { get; set; }
        /// <summary>
        /// 是否在上传国家
        /// </summary>
        public bool UpKey { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        public string KeyValues { get; set; }
        
    }
}
