using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json.Linq;
using Utility;
using ViewModel;

namespace WebApi.Controllers.Admin
{
    //[Produces("application/json")]
    [Route("api/admin/comment/[action]")]
    public class CommentController : BaseApiController
    {
        [HttpGet]
        public ResponseList<List<Comment>> GetList(int index,int count,string order,int? articleid = null,int? posterid = null,
                string str=null,int? status = null)
        {
            QueryModel queryModel = new QueryModel(index,count,order, articleid, posterid, str, status);
            return CommentService.GetCommentList(DbContext, queryModel);
        }
        [HttpGet]
        public ResponseList<List<Comment>> GetTrash(int index, int count, string order, int? articleid = null, int? posterid = null,
                string str = null, int? status = null)
        {
            QueryModel queryModel = new QueryModel(index, count, order, articleid, posterid, str, status);
            return CommentService.GetCommentTrash(DbContext, queryModel);
        }
        [HttpPost]
        public bool Delete([FromBody]JObject jObject)
        {
            return CommentService.Delete(DbContext,jObject["id"].Value<int>());
        }
        [HttpPost]
        public bool Modify([FromBody]JObject jObject)
        {
            return CommentService.Modify(DbContext, jObject["id"].Value<int>(), jObject["status"].Value<int>());
        }
        [HttpGet]
        public ResponseList<List<KeyValue>> GetBase()
        {
            var data =  CommentService.GetBaseSettings(DbContext);
            return new ResponseList<List<KeyValue>>(data,data.Count);
        }
    }
}