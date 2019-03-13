using NUnit.Framework;
using DataAccessDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;

namespace DataAccessDll.Tests
{
    [TestFixture()]
    public class DataAccessTests
    {

        [Test()]
        public void ExecuteSqlTest()
        {
            DataAccess da = new DataAccess();
            DataAccess.Connecting = "Server=.;Database=testUser;User Id=sa;Password=hunan393653149;pooling=true;min pool size = 1;max pool size=5";
            DataAccess.dBType = Common.DBType.SQLService;

            string getObjectSql = "select   *   from  tabletest01  where id=15";
            test t = da.GetObject<test>(getObjectSql,null);

            DateTime dt = new DateTime();

            string sqlStr = "insert into tabletest01(id,name,createtime) values(@id,@name,@createtime)";
            try
            {
                da.StratTran();
                for (int i = 0; i < 2; i++)
                {
                    int ret = da.ExecuteSqlTran(sqlStr, new
                    {
                        id = i,
                        name = "测试"+i,
                        createtime = new SqlDateTime()
                    });
                    if (ret < 0)
                        throw new Exception();
                }
                da.ExecuteSqlTran("", null, true);
            }
            catch (Exception ex)
            {
                da.CloseConTran();
            }
            finally
            {
            }

        }

        [Test()]
        public void ImageTest()
        {
            try
            {
                string filePath = @"C:\Users\hunan\Desktop\还款.png";
                string filePath01 = @"C:\Users\hunan\Desktop\还款01.png";
                Image image = System.Drawing.Image.FromFile(@"C:\Users\hunan\Desktop\还款.png");
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                string format = string.Empty;
                GetImageFormat(fs, out format);
                byte[] b = new byte[fs.Length];
                fs.Read(b, 0, b.Length);
                fs.Dispose();
                string str = Convert.ToBase64String(b);
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(str));
                Image.FromStream(ms).Save(filePath01);
            }
            catch (Exception ex)
            {

            }
           
        }

        private System.Drawing.Imaging.ImageFormat GetImageFormat(Stream file, out string format)
        {
             

            byte[] sb = new byte[2];  //这次读取的就是直接0-1的位置长度了.
            file.Read(sb, 0, sb.Length);
            //根据文件头判断
            string strFlag = sb[0].ToString() + sb[1].ToString();
            //察看格式类型
            switch (strFlag)
            {
                //JPG格式
                case "255216":
                    format = ".jpg";
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                //GIF格式
                case "7173":
                    format = ".gif";
                    return System.Drawing.Imaging.ImageFormat.Gif;
                //BMP格式
                case "6677":
                    format = ".bmp";
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                //PNG格式
                case "13780":
                    format = ".png";
                    return System.Drawing.Imaging.ImageFormat.Png;
                //其他格式
                default:
                    format = string.Empty;
                    return null;
            }
        }
    }


    public class test
    {
        public System.Data.SqlTypes.SqlDateTime CreateTime { get; set; }
    }
}