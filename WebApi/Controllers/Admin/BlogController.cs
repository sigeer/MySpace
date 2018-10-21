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
    [Route("api/admin/blog/[action]")]
    public class BlogController : BaseApiController
    {
        [HttpPost]
        public ResponseMessage Post(Article article)
        {
            ArticleSerivce asv = new ArticleSerivce();
            asv.SaveToDb(DbContext, article);
            return new ResponseMessage(Message.Success);
        }
        [HttpGet]
        public ResponseModel<List<ArticleSimple>> GetList(int index, int count)
        {
            var start = (index - 1) * count;
            return ArticleSerivce.GetArticleList(DbContext, start, count);
        }
        [HttpPost]
        public bool Delete([FromBody]JObject jObject)
        {
            return ArticleSerivce.Delete(DbContext, jObject["id"].Value<int>());
        }
    }
}