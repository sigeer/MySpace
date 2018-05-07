using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{   
    public class Article:BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Nohtml { get; set; }
        public DateTime? CreateTime { get; set; }
        public List<ArticleHistory> Histories { get; set; }
        public List<Comment> Comments { get; set; }
        public int Status { get; set; }
    }
    public class ArticleHistory : BaseModel
    {
        public string MainContent { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }

    public class Comment :BaseModel
    {
        public GuestModel Guest { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
