using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using Dapper;
namespace DataAccessDll
{

    public class DataAccess
    {
       static  DataAccess()
        {
            Connecting = ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;
            string ProviderName = ConfigurationManager.ConnectionStrings["SqlConnStr"].ProviderName;
            //dBType =  !string.IsNullOrEmpty(ProviderName) ? (DBType)Enum.Parse(typeof(DBType), ProviderName) : DBType.OracleDB;
            if (!Enum.TryParse(ProviderName, out _dBType))
                dBType = DBType.OracleDB;
            //用于测试
            //Connecting = "Data Source =(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 172.18.40.15)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User Id=tcmdc; Password=tcm123456;";

        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string Connecting { get; set; }
        private static DBType _dBType;
        /// <summary>
        /// 调用数据库类型
        /// </summary>
        public static DBType dBType { get { return _dBType; } set { _dBType = value; } }

        public int ExecuteSqlTran(string sqlStr, object param, bool flag = false)
        {
            int ret = -1;
            if (!string.IsNullOrEmpty(sqlStr))
            {
                ret = ConTran.Execute(sqlStr, param, Tran);
                if (ret < 0)
                    throw new Exception("执行结果：" + ret + "，语句执行失败！");
            }
            if (flag)
            {
                if (string.IsNullOrEmpty(sqlStr))
                    ret = 0;
                Tran.Commit();
                CloseObject();
            }
            return ret;
        }


        public object ExecuteScalarTran(string sqlStr, object param)
        {
            return ConTran.ExecuteScalar(sqlStr, param, Tran);
        }

        private void OpenDB(IDbConnection Con)
        {
            try
            {
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库打开异常:" + ex.Message);
            }
        }
        private void CloseDB(IDbConnection Con)
        {
            try
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库关闭异常:" + ex.Message);
            }
        }

        private IDbConnection ConTran;
        private IDbTransaction Tran;


        public bool StratTran()
        {
            bool result = false;
            try
            {
                getComTran();
                Tran = ConTran.BeginTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("事务初始化失败！" + ex.Message);
            }
            return result;
        }

        private void getComTran()
        {
            switch (dBType)
            {
                case DBType.SQLService:
                    ConTran = new SqlConnection();
                    break;
                case DBType.MySQLDB:
                    ConTran = new MySqlConnection();
                    break;
                case DBType.OracleDB:
                    ConTran = new OracleConnection();
                    break;
                default:
                    throw new Exception("【" + dBType.ToString() + "】未实现数据库访问类型");
            }
            ConTran.ConnectionString = Connecting;
            OpenDB(ConTran);
        }


        private void CloseObject()
        {
            Tran?.Dispose();
            CloseDB(ConTran);
        }
        public void CloseConTran()
        {
            Tran.Rollback();
            CloseObject();
        }


        public T GetObject<T>(string sqlStr, object param)
        {
            using (IDbConnection idbCon = new SqlConnection(Connecting))
            {
                return idbCon.QuerySingle<T>(sqlStr, param);
            }
        }

        public List<T> GetListObject<T>(string sqlStr, object param = null)
        {
            using (IDbConnection idbCon = new SqlConnection(Connecting))
            {
                return idbCon.Query<T>(sqlStr,param).ToList();
                //return idbCon.Query<T>(sqlStr, param).ToList<T>();
                //return idbCon.Query<T>(sqlStr, param).AsList<T>();
                //return SqlMapper.Query<T>(idbCon, sqlStr, param).ToList<T>();

            }
        }
        public int ExecuteSql(string strSql, object param)
        {
            int ret = -1;
            using (IDbConnection idbCon = new SqlConnection(Connecting))
            {
                ret = idbCon.Execute(strSql, param);
                //idbCon.ExecuteScalar
            }
            return ret;
        }

        public object ExecuteScalar(string sqlStr, object param)
        {
            object obj = null;
            using (IDbConnection idbCon = new SqlConnection(Connecting))
            {
                obj = idbCon.ExecuteScalar(sqlStr, param);
            }
            return obj;
        }

    }



}
