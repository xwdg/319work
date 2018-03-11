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
        private MySqlConnection cnt = null;
        private MySqlCommand cmd = null;
        private MySqlDataAdapter adap = null;

        public Mysql(string db, string user, string pwd, string host)
        {
            string s = $"Host={host};Database={db};Username={user };Password={pwd}";
            cnt = new MySqlConnection(s);
        }

        public DataSet CX(string sql)
        {
            DataSet ds = new DataSet();
            cnt.Open();
            cmd = cnt.CreateCommand();
            cmd.CommandText = sql;
            adap = new MySqlDataAdapter(cmd);
            adap.Fill(ds);
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