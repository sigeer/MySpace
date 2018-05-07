using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ResponseModel
    {
        public string Data { get; set; }
        public string Status { get; set; }
        public string Info { get; set; }
        public ResponseModel()
        {
            Data = "";
            Status = Message.Success;
            Info = "";
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public ResponseModel(string data, string status, string info)
        {
            Data = data??"";
            Status = status;
            Info = info??"";
        }
        public ResponseModel(string status)
        {
            Status = status;
            Data = "";
            Info = "";
        }
    }
    public static class Message
    {
        public static readonly string Success = "Success";
        public static readonly string OK = "OK";
        public static readonly string Error = "Error";
    }
}
