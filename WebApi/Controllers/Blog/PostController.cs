using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Utility;
using ViewModel;

namespace WebApi.Controllers
{
   public class PostController: BaseApiController
   {
        private readonly IHostingEnvironment  _hostingEnvironment;
        public PostController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
       [HttpPost]
       public async Task<IActionResult> Upload()
       {
            if (HttpContext.Request.Form.Files.Count!=1)
            {
                return Ok(new { errorMsg = "only 1 file"});
            }
            var file = HttpContext.Request.Form.Files[0];
            using (var temp = file.OpenReadStream())
            {
                string directoryPath = Path.Combine(_hostingEnvironment.WebRootPath , "Upload");
                var filePath = await SaveFiles(directoryPath,file.FileName,temp);
                return Ok(new {url="/Upload/"+file.FileName});
            }
       }
       [HttpPost]
       public string Content([FromBody]Article article)
       {
           var result = new ArticleSerivce().SaveToDb(DbContext,article);
           return result;
       }
   }
}