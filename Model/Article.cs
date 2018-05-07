using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{   
    public class Article:BaseModel
    {
        public int Id { get;set;}
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? LastModifyTime{get;set;}
        public int Status { get; set; }
    }
    public class Comment :BaseModel
    {
        public GuestModel Guest { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
