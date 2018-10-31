using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Utility;
using ViewModel;

namespace WebApi.Controllers
{
    [Route("api/guest/blog/[action]")]
    public class BlogController : BaseApiController
    {

        [HttpGet]
        public ResponseModel<List<ArticleSimple>> Index(int index, int count)
        {
            var start = (index - 1) * count;
            return  ArticleSerivce.GetArticleList(DbContext, start, count);
        }
        public Article GetArticle(int id)
        {
            return new ArticleSerivce().GetArticle(DbContext, id);
        }
        public ResponseModel<List<Comment>> GetComment(int id)
        {
            return CommentService.GetCommentsInArticle(DbContext, id);
        }
        [HttpPost]
        public string SendComment([FromBody]dynamic model)
        {
            var email = model.Email.ToString();
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            int aid = int.Parse(model.AId.ToString());
            string comment = model.Comment.ToString();
            var guest = new UserService().GetGuest(DbContext, email, ip.ToString());
            var result = CommentService.PostComment(DbContext, comment, guest,aid);
            return result;
        }

    }
}