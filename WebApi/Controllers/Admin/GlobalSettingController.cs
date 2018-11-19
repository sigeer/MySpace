using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers.Admin
{
     [Route("api/admin/globalsetting/[action]")]
    public class GlobalSettingController : BaseApiController
    {
        [HttpPost]
        public void WebHook()
        {
           var forms = HttpContext.Request.Form;
           foreach (var item in forms)
           {
               Console.WriteLine(item.Key + ":" + item.Value);
               Console.WriteLine("---------------------");
           }
        }
    }
}