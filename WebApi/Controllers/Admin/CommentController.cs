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
        public ResponseModel<List<Comment>> GetList(int index,int count,string order,int? aId = null,int? pId= null,
                string str=null,int? statusId=null)
        {
            QueryModel queryModel = new QueryModel(index,count,order,aId,pId,str,statusId);
            return CommentService.GetCommentList(DbContext, queryModel);
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
        public ResponseModel<List<KeyValue>> GetBase()
        {
            var data =  CommentService.GetBaseSettings(DbContext);
            return new ResponseModel<List<KeyValue>>(data,data.Count);
        }
    }
}