using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Windows;

namespace Work
{
    class Mysql
    {
        
        private MySqlConnection cnt = null;                  //与MySql数据库的连接
        private MySqlCommand cmd = null;                 //要执行的SQL命令
        private MySqlDataAdapter adap = null;           //数据库适配器，用于同步本地数据（DataSet）与数据库数据，包含要执行的SQL命令（MySqlCommand）
        private MySqlTransaction tran = null;
        private MySqlScript script = null;


        public Mysql()
        {
            GetAppCon gac = new GetAppCon();

            string DBName = gac.ReadSetting("DBName");
            string DBURL = gac.ReadSetting("DBURL");
            string DBUser = gac.ReadSetting("DBUser");
            string DBPassword = gac.ReadSetting("DBPwd");
            string DBPort = gac.ReadSetting("DBPort");
            
            string s = $"Host={DBURL};Database={DBName};Username={DBUser};Password={DBPassword};Charset=utf8;Port={DBPort}";
            cnt = new MySqlConnection(s);
        }

        public void ExecScript(string sql)
        {
            script = new MySqlScript(cnt);
            script.Query = sql;
            int count = script.Execute();
        }

        public Mysql(string url, string name, string user, string pwd, string port)
        {
            string s = $"Host={url};Database={name};Username={user};Password={pwd};Charset=utf8;Port={port}";
            cnt = new MySqlConnection(s);
        }
        //SQL查询
        public DataSet Select(string sql)
        {
            DataSet ds = new DataSet();

            cmd = cnt.CreateCommand();
            cmd.CommandText = sql;
            adap = new MySqlDataAdapter(cmd);
            adap.Fill(ds);

            return ds;
        }

        public void Open()
        {
            cnt.Open();
        }

        public void Close()
        {
            cnt.Close();
        }

        public void Execute(string sql)
        {
            cmd = new MySqlCommand(sql, cnt);
            cmd.ExecuteNonQuery();
        }

        public void Begin()
        {
            tran = cnt.BeginTransaction();
            cmd = cnt.CreateCommand();
            cmd.Transaction = tran;
        }

        public void Commit()
        {
            tran.Commit();
        }

        public void Rollback()
        {
            tran.Rollback();
        }
    }
}