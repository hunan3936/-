using NUnit.Framework;
using DataAccessDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

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
                    int ret = da.ExecuteSql(sqlStr, new
                    {
                        id = i,
                        name = "测试"+i,
                        createtime = new SqlDateTime()
                    });
                    if (ret < 0)
                        throw new Exception();
                }
                da.ExecuteSql("", null, true);
            }
            catch (Exception ex)
            {
                da.CloseConTran();
            }
            finally
            {
            }

        }
    }


    public class test
    {
        public System.Data.SqlTypes.SqlDateTime CreateTime { get; set; }
    }
}