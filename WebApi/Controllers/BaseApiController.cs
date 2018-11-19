using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Utility;
using Utility.Constans;
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
        protected async Task<UploadFileInfo> UploadSingleFile(string targetPath,FileType fileType,bool needExtension = false)
        {
            if (HttpContext.Request.Form.Files.Count==0)
            {
                return null;
            }
            UploadFileInfo result = new UploadFileInfo();
            var item = HttpContext.Request.Form.Files[0];
            string fileName = item.FileName;
            result.OldName = fileName;
            if (fileName.Length > 100)
            {
                result.Result = Info.Invalid_FileName;
                return result;
            }
            if (!AllowdFormat.IsAllowed(fileName, fileType))
            {
                switch (fileType)
                {
                    case FileType.Image:
                        result.Result = Info.File_OnlyImage;
                        break;
                    case FileType.Excel:
                        result.Result = Info.File_OnlyExcel;
                        break;
                    default:
                        break;
                }
                return result;
            }
            if (item.Length > 1024 * 1024 * 20)
            {
                result.Result = Info.Invalid_FileSize;
                return result;
            }
            string extension = string.Empty;
            if (needExtension&&fileName.IndexOf(".") >= 0)
            {
                extension = fileName.Substring(fileName.LastIndexOf("."), (fileName.Length - fileName.LastIndexOf(".")));
            }
            using (Stream s = item.OpenReadStream())
            {
                var tempFileName = Guid.NewGuid().ToString() + extension;
                var filePath = await SaveFiles(targetPath, tempFileName.ToString(), s);
                if (string.IsNullOrEmpty(filePath))
                {
                    result.Result = Info.File_Others;
                    return result;
                    //非代码控制的其他原因导致的上传失败
                }
                else
                {
                    result.NewName = tempFileName;
                    result.Result = Message.Success;
                    result.TotalPath = filePath;
                }
            }

            return result;
        }
    }
    public class UploadFileInfo
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string TotalPath { get; set; }
        public string Result { get; set; }

    }
}