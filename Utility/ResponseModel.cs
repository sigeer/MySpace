using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ResponseModel<T>
    {
        public T Data{get;set;}
        public int Count{get;set;}
        public ResponseModel(T list,int count)
        {
            Data = list;
            Count = count;
        }
        public ResponseModel()
        {
            Data = default(T);
            Count = 0;
        }
    }
    public class ResponseMessage
    {
        public string Data { get; set; }
        public string Status { get; set; }
        public string Info { get; set; }
        public ResponseMessage()
        {
            Data = "";
            Status = Message.Success;
            Info = "";
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public ResponseMessage(string data, string status, string info)
        {
            Data = data??"";
            Status = status;
            Info = info??"";
        }
        public ResponseMessage(string status)
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
