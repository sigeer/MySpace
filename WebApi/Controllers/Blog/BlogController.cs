using Microsoft.AspNetCore.Mvc;
using Model;
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
        public  ResponseModel<List<Article>> GetArticleList(int index, int count)
        {
            var start = (index - 1) * count;
            return new ArticleSerivce().GetArticleList(DbContext, start, count);
        }
        [HttpPost]
        public ResponseModel<List<Comment>> GetCommentList(QueryModel queryModel)
        {
            return new CommentService().GetCommentList(DbContext, queryModel);
        }
    }
}