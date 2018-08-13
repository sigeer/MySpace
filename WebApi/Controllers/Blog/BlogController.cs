using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Utility;
using ViewModel;

namespace WebApi.Controllers
{
    public class BlogController : BaseApiController
    {
        [HttpPost]
        public ResponseMessage PostArticle(Article article)
        {
            ArticleSerivce asv = new ArticleSerivce();
            asv.SaveToDb(DbContext, article);
            return new ResponseMessage(Message.Success);
        }
        public  ResponseModel<List<ArticleSimple>> GetArticleList(int index, int count)
        {
            var start = (index - 1) * count;
            return new ArticleSerivce().GetArticleList(DbContext, start, count);
        }
        [HttpPost]
        public ResponseModel<List<Comment>> GetCommentList([FromBody]QueryModel queryModel)
        {
            return new CommentService().GetCommentList(DbContext, queryModel);
        }
        [HttpPost]
        public bool DeleteComment([FromBody]JObject jObject)
        {
            return CommentService.Delete(DbContext,jObject["id"].Value<int>());
        }
        [HttpPost]
        public bool ModifyComment([FromBody]JObject jObject)
        {
            return CommentService.Modify(DbContext, jObject["id"].Value<int>(), jObject["status"].Value<int>());
        }
        [HttpGet]
        public ResponseModel<List<KeyValue>> GetCommentSettings()
        {
            var data =  CommentService.GetBaseSettings(DbContext);
            return new ResponseModel<List<KeyValue>>(data,data.Count);
        }
    }
}