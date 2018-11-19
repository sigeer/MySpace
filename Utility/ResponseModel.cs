using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ResponseList<T>
    {
        public T Data{get;set;}
        public int Count{get;set;}
        public ResponseList(T list,int count)
        {
            Data = list;
            Count = count;
        }
        public ResponseList()
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
        public string[] Params {get;set;}
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
        public ResponseMessage(string data =null, string status=null, string info=null,string[] p = null)
        {
            Data = data??"";
            Status = status??"";
            Info = info??"";
            Params = p;
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
