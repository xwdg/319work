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

        public Mysql(string db, string user, string pwd, string host)
        {
            string s = $"Host={host};Database={db};Username={user };Password={pwd}";
            cnt = new MySqlConnection(s);
        }

        //SQL查询
        public DataSet CX(string sql)
        {
            DataSet ds = new DataSet();
            cnt.Open();
            cmd = cnt.CreateCommand();
            cmd.CommandText = sql;
            adap = new MySqlDataAdapter(cmd);
            adap.Fill(ds);
            cnt.Close();
            return ds;
            //mscd = new MySqlCommand(sql, msc);
            //MySqlDataReader reader = mscd.ExecuteReader();
            //reader.fill
            //while (reader.Read())
            //    if (reader.HasRows)
            //    {
            //        temp = reader.GetString(0);
            //        msc.Close();
            //        return temp;
            //    }
            //return "sb";
        }
    }
}