using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UEditorNetCore;
using Utility.DbHelper;
using Utility.RedisHelper;

namespace WebApi.Controllers
{
    [EnableCors("any")] //设置跨域处理的 代理
    public class BaseApiController : Controller
    {
        private DbContext context;
        protected DbContext DbContext => context= HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
        //protected RedisHelper Redis = new RedisHelper("47.94.167.66");
        protected SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a secret that needs to be at least 16 characters long"));
    }
    [EnableCors("any")]
    [Route("api/[controller]")]
    public class UEditorController : Controller
    {
        private UEditorService ue;
        public UEditorController(UEditorService ue)
        {
            this.ue = ue;
        }

        public void Do()
        {
            ue.DoAction(HttpContext);
        }

    }
}