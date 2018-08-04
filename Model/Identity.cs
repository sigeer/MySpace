using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model
{
    public class Identity
    {
        public string HeadPic { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public DateTime DateTime { get; set; }
        public string Guid { get; set; }
        public Stream CreateQrCode()
        {
            return null;
        }
        public string CreatID()
        {
            return "";
        }
        public string ValidCode(string code)
        {
            return "";
        }
    }
    public class LoginModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

}
