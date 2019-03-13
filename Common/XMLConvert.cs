using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    public class XMLConvert
    {
        #region
        public static T DeserializeToObject<T>(String strXml)
        {
            using (StringReader reader = new StringReader(strXml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return    (T)serializer.Deserialize(reader);
            }
        }
        #endregion

        #region
        public static string SerializeToXML<T>(T t)
        {
            string strXml = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(stream, t);
                stream.Position = 0;
                using (StreamReader sr = new StreamReader(stream))
                {
                    strXml = sr.ReadToEnd();
                }
            }
            return strXml;
        }
        #endregion




        #region  序列化
        /// <summary>
        /// XML字符串序列化为对象
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <param name="t">对象类型</param>
        /// <returns></returns>
        public static Object XmlToObject(String xml, Type t)
        {

            if (String.IsNullOrEmpty(xml))
                return null;
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            XmlNode node = null;
            for (int i = 0; i < xmldoc.ChildNodes.Count; i++)
            {
                if (xmldoc.ChildNodes[i].NodeType == XmlNodeType.Element)
                {
                    node = xmldoc.ChildNodes[i];
                    break;
                }
            }
            return NodeToObj(node, t);
        }
        /// <summary>
        /// XML字符串序列化为对象
        /// </summary>
        /// <typeparam name="T">XML字符串</typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T XmlToObject<T>(string xml) where T : class
        {
            object obj = XmlToObject(xml, typeof(T));
            return obj as T;
        }

        private static void SetArrValue(XmlNode n, Type type, Array arrObj, int index)
        {
            if (type.Equals(typeof(String[])))
            {
                if (n != null && n.FirstChild != null)
                {
                    arrObj.SetValue(n.FirstChild.Value, index);

                }
                else
                {
                    arrObj.SetValue(string.Empty, index);
                }
            }
            else if (type.Equals(typeof(int[])))
            {
                if (n != null && n.FirstChild != null && !String.IsNullOrEmpty(n.FirstChild.Value))
                {
                    arrObj.SetValue(int.Parse(n.FirstChild.Value), index);
                }
            }
            else if (type.Equals(typeof(long[])))
            {
                if (n != null && n.FirstChild != null && !String.IsNullOrEmpty(n.FirstChild.Value))
                {
                    arrObj.SetValue(long.Parse(n.FirstChild.Value), index);

                }
            }
            else
            {
                //是类对象
                Object objValue = NodeToObj(n, arrObj.GetType().GetElementType());
                arrObj.SetValue(objValue, index);
            }
        }

        private static void SetValue(XmlNode pn, String name, Type type, Object obj, MethodInfo mi)
        {
            if (type.IsArray)
            {

                //数组
                XmlNodeList list = pn.SelectNodes(name);
                Object arr = XmlListToObj(list, obj, type);
                if (arr != null)
                    mi.Invoke(obj, new object[] { arr });
            }
            else
            {
                if (type.Equals(typeof(string)))
                {
                    XmlNode n = pn.SelectSingleNode(name);
                    if (n != null && n.FirstChild != null)
                    {
                        if (n.FirstChild.Value != null)
                        {
                            mi.Invoke(obj, new object[] { n.FirstChild.Value });
                        }
                        else
                        {
                            mi.Invoke(obj, new object[] { n.InnerXml });
                        }
                    }
                    else
                    {
                        mi.Invoke(obj, new object[] { string.Empty });
                    }
                }
                else if (type.Equals(typeof(DateTime)))
                {
                    XmlNode n = pn.SelectSingleNode(name);
                    if (n != null && n.FirstChild != null)
                    {
                        if (n.FirstChild.Value != null)
                        {
                            mi.Invoke(obj, new object[] { Convert.ToDateTime(n.FirstChild.Value) });
                        }
                        else
                        {
                            mi.Invoke(obj, new object[] { Convert.ToDateTime(n.InnerXml) });
                        }
                    }
                    else
                    {
                        mi.Invoke(obj, new object[] { DateTime.Now });
                    }
                }
                else if (type.Equals(typeof(int)))
                {
                    XmlNode n = pn.SelectSingleNode(name);
                    if (n != null && n.FirstChild != null && !String.IsNullOrEmpty(n.FirstChild.Value))
                    {
                        mi.Invoke(obj, new object[] { int.Parse(n.FirstChild.Value) });
                    }
                }
                else if (type.Equals(typeof(long)))
                {
                    XmlNode n = pn.SelectSingleNode(name);
                    if (n != null && n.FirstChild != null && !String.IsNullOrEmpty(n.FirstChild.Value))
                    {
                        mi.Invoke(obj, new object[] { long.Parse(n.FirstChild.Value) });
                    }
                }
                else if (type.Equals(typeof(decimal)))
                {
                    XmlNode n = pn.SelectSingleNode(name);
                    if (n != null && n.FirstChild != null && !String.IsNullOrEmpty(n.FirstChild.Value))
                    {
                        mi.Invoke(obj, new object[] { decimal.Parse(n.FirstChild.Value) });
                    }
                }
                else if (type.BaseType != null && type.BaseType.Equals(typeof(Enum)))
                {
                    XmlNode n = pn.SelectSingleNode(name);
                    if (n != null && n.FirstChild != null && !String.IsNullOrEmpty(n.FirstChild.Value))
                    {
                        mi.Invoke(obj, new object[] { Int32.Parse((n.FirstChild.Value)) });
                    }
                }
                else
                {
                    XmlNode n = pn.SelectSingleNode(name);
                    if (n != null)
                    {
                        Object o = NodeToObj(n, type);
                        if (o != null)
                            mi.Invoke(obj, new object[] { o });
                    }
                }
            }
        }
        private static Object NodeToObj(XmlNode n, Type t)
        {
            Object obj = Activator.CreateInstance(t);

            MethodInfo[] mi = obj.GetType().GetMethods();
            for (int i = 0; i < mi.Length; i++)
            {
                MethodInfo m = mi[i];
                if (!m.IsPublic)
                    continue;
                string name = m.Name;
                if (name.IndexOf("set_") == -1)
                    continue;
                string[] arrname = name.Split('_');
                name = string.Empty;
                for (int k = 1; k < arrname.Length; k++)
                {
                    name += arrname[k];
                    if (k < arrname.Length - 1)
                        name += "_";
                }
                ParameterInfo[] pi = m.GetParameters();

                SetValue(n, name, pi[0].ParameterType, obj, m);
            }
            return obj;
        }
        private static Object XmlListToObj(XmlNodeList list, Object obj, Type type)
        {
            var arr = Array.CreateInstance(type.GetElementType(), list.Count);
            Type t = type.GetElementType();
            for (int i = 0; i < arr.Length; i++)
            {
                XmlNode n = list[i];
                SetArrValue(n, type, arr, i);
            }
            return arr;
        }

        #endregion

        #region 反序列化
        /// <summary>
        /// 反序列化XML对象为XML字符串
        /// </summary>
        /// <param name="obj">XML对象</param>
        /// <returns></returns>
        public static String Serialize(Object obj)
        {
            if (obj == null)
                return "";
            Type t = obj.GetType();
            StringBuilder sb = new StringBuilder();
            SerializeObj(obj, sb, t.Name);
            return sb.ToString();
        }
        /// <summary>
        /// 反序列化XML对象为XML字符串
        /// </summary>
        /// <param name="obj">XML对象</param>
        /// <param name="rootTag">根元素名称</param>
        /// <returns></returns>
        public static String Serialize(Object obj, string rootTag)
        {
            if (obj == null)
                return "";
            StringBuilder sb = new StringBuilder();
            SerializeObj(obj, sb, rootTag);
            return sb.ToString();
        }
        private static void SetValue(Object value, StringBuilder sb)
        {
            String strValue = "";
            if (value != null)
                strValue = value.ToString();
            sb.Append(strValue);
        }
        private static void SerializeObj(Object obj, StringBuilder sb, String tagName)
        {
            if (obj == null)
            {
                sb.Append("<");
                sb.Append(tagName);
                sb.Append(">");
                sb.Append("</");
                sb.Append(tagName);
                sb.Append(">");
                return;
            }
            Type t = obj.GetType();

            //是基础类型
            if (t.Equals(typeof(int)) || t.Equals(typeof(DateTime)) || t.Equals(typeof(float)) || t.Equals(typeof(double)) || t.Equals(typeof(long)) || t.Equals(typeof(string)) || t.Equals(typeof(decimal)))
            {
                sb.Append("<");
                sb.Append(tagName);
                sb.Append(">");
                SetValue(obj, sb);
                sb.Append("</");
                sb.Append(tagName);
                sb.Append(">");
            }
            else if (t.IsArray)
            {
                Array arr = obj as Array;
                for (int i = 0; i < arr.Length; i++)
                {
                    SerializeObj(arr.GetValue(i), sb, tagName);
                }
                //是数组
            }
            else
            {
                sb.Append("<");
                sb.Append(tagName);
                sb.Append(">");
                //是对象需分解
                MethodInfo[] mi = obj.GetType().GetMethods();
                for (int i = 0; i < mi.Length; i++)
                {
                    MethodInfo m = mi[i];
                    if (!m.IsPublic)
                        continue;
                    string name = m.Name;
                    if (name.IndexOf("get_") == -1)
                        continue;
                    string[] arrname = name.Split('_');
                    name = string.Empty;
                    for (int k = 1; k < arrname.Length; k++)
                    {
                        name += arrname[k];
                        if (k < arrname.Length - 1)
                            name += "_";
                    }
                    Object value = m.Invoke(obj, null);

                    SerializeObj(value, sb, name);
                }
                sb.Append("</");
                sb.Append(tagName);
                sb.Append(">");
            }

        }
        #endregion
    }
}
