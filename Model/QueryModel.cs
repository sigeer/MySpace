using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class QueryModel
    {
        public int Index { get; set; }
        public int Count { get; set; }
        public string Order { get; set; }
        public FilterModel Filter { get; set; }
    }
    public class FilterModel
    {
        public int ArticleId { get; set; }
        public int PosterId { get; set; }
        public string Str { get; set; }
        public int Status{get;set;}
    }
}
