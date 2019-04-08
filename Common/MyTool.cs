using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
   public class MyTool
    {
        public static string MethodInfo()
        {
           // string str = System.Reflection.MethodBase.GetCurrentMethod().Name;
            MethodBase mb = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            string name = mb.Name;
            string[] full = mb.DeclaringType.FullName.Split('.');
            return full[full.Length - 1] + "." + name;
        }

        public static string GetAssemblyPath(Type tpye)
        {
            string codeBase = tpye.Assembly.CodeBase;  //Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }


        /// <summary> 
        /// 获取MAC地址(返回第一个物理以太网卡的mac地址) 
        /// </summary> 
        /// <returns>成功返回mac地址，失败返回null</returns> 

        public static string GetMacAddress()
        {
            string macAddress = null;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (adapter.NetworkInterfaceType.ToString().Equals("Ethernet")) //是以太网卡
                    {
                        string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                        RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                        if (rk != null)
                        {
                            // 区分 PnpInstanceID     
                            // 如果前面有 PCI 就是本机的真实网卡    
                            // MediaSubType 为 01 则是常见网卡，02为无线网卡。    
                            string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                            int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                            if (fPnpInstanceID.Length > 3 && fPnpInstanceID.Substring(0, 3) == "PCI") //是物理网卡
                            {
                                macAddress =  adapter.GetPhysicalAddress().ToString();
                                break;
                            }
                            else if (fMediaSubType == 1) //虚拟网卡
                                continue;
                            else if (fMediaSubType == 2) //无线网卡(上面判断Ethernet时已经排除了)
                                continue;
                        }
                    }
                }
            }
            catch
            {
                macAddress = null;
            }
            return macAddress;
        }


        public static string GetIPAddress()
        {
            string ipAddress = null;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (adapter.NetworkInterfaceType.ToString().Equals("Ethernet")) //是以太网卡
                    {

                        string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                        RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                        if (rk != null)
                        {
                            // 区分 PnpInstanceID     
                            // 如果前面有 PCI 就是本机的真实网卡    
                            // MediaSubType 为 01 则是常见网卡，02为无线网卡。    
                            string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                            int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                            if (fPnpInstanceID.Length > 3 && fPnpInstanceID.Substring(0, 3) == "PCI") //是物理网卡
                            {
                                IPInterfaceProperties fIPInterfaceProperties = adapter.GetIPProperties();
                                UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = fIPInterfaceProperties.UnicastAddresses;
                                foreach (UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                                {
                                    if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                                    {
                                        ipAddress = UnicastIPAddressInformation.Address.ToString();
                                        break;
                                    }
                                }
                                break;
                            }
                            else if (fMediaSubType == 1) //虚拟网卡
                                continue;
                            else if (fMediaSubType == 2) //无线网卡(上面判断Ethernet时已经排除了)
                                continue;
                        }
                    }
                }
            }
            catch
            {
                ipAddress = null;
            }
            return ipAddress;
        }



        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password,Encoding encode)
        {
            var md5 = new MD5CryptoServiceProvider();
            //string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            string t2 = BitConverter.ToString(md5.ComputeHash(encode.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }


        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password, Encoding encode)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            byte[] encryptedBytes = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:X2}", encryptedBytes[i]);
            }
            return sb.ToString();
            /*
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;*/
        }

        

        public static string MD5Encrypt(string password, Encoding encode, int md5nun = 32)
        {
            if (md5nun == 32)
            {
                return MD5Encrypt32(password, encode);
            }
            else if (md5nun == 16)
            {
                return MD5Encrypt16(password, encode);
            }
            return "";
        }

        public  static string GetSerialNo(int random = 0, string suffix = "")
        {
            DateTime dt = DateTime.Now;
            string randomStr = string.Empty;
            if (random > 0)
            {
                double d = new Random().NextDouble();
                randomStr =  Math.Floor(d * Math.Pow(10, random)).ToString().PadLeft(random,'0');

            }
            return dt.ToString("yyyyMMddHHmmssfff") + randomStr + suffix;
        }

        public static string ObjToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
