using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Utility.DbHelper;

namespace WebApi.Controllers
{
    [EnableCors("any")] //设置跨域处理的 代理
    public class BaseApiController : Controller
    {
        private DbContext context;
        protected DbContext DbContext => context= HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
        //protected RedisHelper Redis = new RedisHelper("47.94.167.66");
        protected SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a secret that needs to be at least 16 characters long"));

        protected async Task<string> SaveFiles(string directoryPath, string fileName,Stream s)
        {
            try
            {
                byte[] bs = new byte[s.Length];
                s.Read(bs, 0, bs.Length);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filePath = Path.Combine(directoryPath, fileName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await fs.WriteAsync(bs, 0, bs.Length);
                fs.Close();
                return filePath;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}