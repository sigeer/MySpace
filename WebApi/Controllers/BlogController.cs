using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Utility;
using ViewModel;

namespace WebApi.Controllers
{
    public class BlogController : BaseApiController
    {
        [HttpPost]
        public ResponseModel PostArticle( Article article)
        {
            ArticleSerivce asv = new ArticleSerivce();
            asv.SaveToDb(DbContext, article);
            return new ResponseModel(Message.Success);
        }
    }
}