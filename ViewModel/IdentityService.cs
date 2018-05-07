using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Utility.DbHelper;

namespace ViewModel
{
    //由于本系统是单用户系统  不需要在登录时获取其他信息 只需验证即可
    public class IdentityService
    {
        private static readonly string privateKey = "sigeerSpace";
        public static bool LoginValid(DbContext db, string pwd)
        {
            return db.IsExisted("SELECT * FROM `admin` WHERE PASSWORD=@pwd LIMIT 1;",new MySql.Data.MySqlClient.MySqlParameter("pwd",pwd));
        }
        public static Identity GetUser(DbContext db, string pwd)
        {

            var data = db.ExecuteQuery("SELECT * FROM `admin` WHERE PASSWORD=@pwd LIMIT 1;", new MySql.Data.MySqlClient.MySqlParameter("pwd", pwd));
            
            if (data.Tables[0].Rows.Count<=0)
            {
                return null;
            }
            var item = data.Tables[0].Rows[0];
            Identity id = new Identity();
            id.NickName = item["NickName"].ToString();
            return id;
        }
        public static bool QrValid(DbContext db ,string sendCode)
        {
            Utility.Encode en = Utility.Encode.GetInstance();
            string newData = en.RSADecrypt(sendCode);
            return LoginValid(db,newData);
        }
    }
}
