using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;

namespace Work
{
    class Mysql
    {
        
        private MySqlConnection cnt = null;                  //与MySql数据库的连接
        private MySqlCommand cmd = null;                 //要执行的SQL命令
        private MySqlDataAdapter adap = null;           //数据库适配器，用于同步本地数据（DataSet）与数据库数据，包含要执行的SQL命令（MySqlCommand）
        private MySqlTransaction tran = null;

        public Mysql(string db, string user, string pwd, string host)
        {
            string s = $"Host={host};Database={db};Username={user};Password={pwd};charset=utf8";
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