using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment  _hostingEnvironment;
        public BlogController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
       [HttpPost]
       public async Task<IActionResult> Upload()
       {
           string directoryPath = Path.Combine(_hostingEnvironment.WebRootPath , "Upload");
           var fileResult = await UploadSingleFile(directoryPath,FileType.Image,true);
           if (fileResult!=null)
           {
               if (fileResult.Result==Message.Success)
               {
                    return Ok(new {url="/Upload/"+fileResult.NewName});
               }
               else
               {
                   return Ok(fileResult.Result);
               }
           }
           else
           {
                return Ok("no data");
           }
       }
       [HttpPost]
       public string Post([FromBody]Article article)
       {
           var result = ArticleSerivce.SaveToDb(DbContext,article);
           return result;
       }
        [HttpGet]
        public ResponseList<List<ArticleSimple>> GetList(int index, int count)
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