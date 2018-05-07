using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Utility.DbHelper
{
    public class DbContext
    {
        private string connectStr;
        public DbContext(string conn)
        {
            connectStr = conn;
        }
        public DataSet ExecuteQuery(string queryCmd,params MySqlParameter[] parameters )
        {
            return MySqlHelper.ExecuteDataset(connectStr, queryCmd, parameters);
            
        }
        public bool ExecuteNonQuery(string exeCmd, params MySqlParameter[] parameters)
        {
            return MySqlHelper.ExecuteNonQuery(connectStr, exeCmd, parameters)>0;
        }
        public bool IsExisted(string queryCmd, params MySqlParameter[] parameters)
        {
            var reader = MySqlHelper.ExecuteReader(connectStr, queryCmd, parameters);
            return reader.HasRows;
        }
    }
}
