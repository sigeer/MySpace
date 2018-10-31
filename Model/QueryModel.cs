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
        public QueryModel(){}
        public QueryModel(int index,int count,string order,int? aId=null,int? pId=null,string str=null,int? statusId=null)
        {
            Index = index;
            Count = count;
            Order = order;
            Filter = new FilterModel()
            {
                ArticleId = aId.HasValue?aId.Value:0,
                PosterId = pId.HasValue?pId.Value:0,
                Str = str??"",
                Status = statusId.HasValue?statusId.Value:0
            };
        }
    }
    public class FilterModel
    {
        public int ArticleId { get; set; }
        public int PosterId { get; set; }
        public string Str { get; set; }
        public int Status{get;set;}
    }
}
