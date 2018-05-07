using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Utility.DbHelper;

namespace ViewModel
{
    public class UserService
    {
        public GuestModel GetGuest(DbContext db, string email, string ip)
        {

            MySqlParameter[] mySqlParameters = { new MySqlParameter("email", email), new MySqlParameter("ip", ip) };
            var data = db.ExecuteQuery("select * from `person` where ContactInfo=@email and ip=@ip limit 1;", mySqlParameters);
            var temp = data.Tables[0];
            DataRow dr;
            if (temp.Rows.Count > 0)
            {
                dr = temp.Rows[0];
                GuestModel gm = new GuestModel();
                var dt = new DateTime();
                gm.Id = Convert.ToInt32(dr["Id"]);
                gm.IP = dr["IP"].ToString();
                gm.ContactInfo = dr["ContactInfo"].ToString();
                gm.Status = int.Parse(dr["Status"].ToString());
                bool flag = DateTime.TryParse(dr["FirstVisitedTime"].ToString(), out dt);
                gm.FirstVisitedTime = flag ? dt : DateTime.MinValue;
                return gm;
            }
            else
            {
                GuestModel gm = new GuestModel();
                gm.IP = ip;
                gm.Status = 1;
                gm.FirstVisitedTime = DateTime.Now;
                gm.ContactInfo = email;
                var paramters = new MySqlParameter[] { new MySqlParameter("ip", ip), new MySqlParameter("email", email), new MySqlParameter("id", SqlDbType.Int) };
                paramters[2].Direction = ParameterDirection.Output;
                var result = db.ExecuteNonQuery("insert into `person`(`IP`,`Status`,`ContactInfo`,`FirstVisitedTime`) values(@ip,'1',@email,'" + DateTime.Now + "');select @id= SCOPE_IDENTITY();", paramters);
                if (result)
                {
                    int id = Convert.ToInt32(paramters[2].Value);
                    gm.Id = id;
                    return gm;
                }
                else
                {
                    return new GuestModel();
                }

            }

        }
    }
}
