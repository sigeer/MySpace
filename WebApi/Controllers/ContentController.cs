using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Utility;
using ViewModel;

namespace WebApi.Controllers
{
    public class ContentController : BaseApiController
    {
        public ResponseModel<List<ArticleSimple>> GetTitles(int index, int count)
        {
            var start = (index - 1) * count;
            return new ArticleSerivce().GetArticleList(DbContext, start, count);
        }
        public Article GetArticle(int Aid)
        {
            return new ArticleSerivce().GetArticle(DbContext, Aid);
        }
        public ResponseModel<List<Comment>> GetComment(int Aid)
        {
            return new CommentService().GetCommentsInArticle(DbContext, Aid);
        }
        [HttpPost]
        public string SendComment([FromBody]dynamic model)
        {
            var email = model.Email.ToString();
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            int aid = int.Parse(model.AId.ToString());
            string comment = model.Comment.ToString();
            var guest = new UserService().GetGuest(DbContext, email, ip.ToString());
            var result = new CommentService().PostComment(DbContext, comment, guest,aid);
            return result;
        }
    }
}