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
        public ResponseModel PostArticle(Article article)
        {
            ArticleSerivce asv = new ArticleSerivce();
            asv.SaveToDb(DbContext, article);
            return new ResponseModel(Message.Success);
        }
        public List<Article> GetArticleList(int index, int count)
        {
            var start = (index - 1) * count;
            return new ArticleSerivce().GetArticleList(DbContext, start, count);
        }
    }
}