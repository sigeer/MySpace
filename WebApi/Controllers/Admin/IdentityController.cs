using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Utility;
using Utility.QrCodeHelper;
using ViewModel;

namespace WebApi.Controllers
{
    public class IdentityController : BaseApiController
    {
        
        [HttpPost]
        public IActionResult GetToken(string pwd)
        {
            var result = IdentityService.GetUser(DbContext, pwd);
            if (result!=null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, result.NickName),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddHours(2)).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
                };
                //var token = new JwtSecurityToken("test","test",claims, DateTime.Now, DateTime.Now.AddDays(1), new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256));
                var token = new JwtSecurityToken(
                    new JwtHeader(new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims));
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return new ObjectResult(jwtToken);
            }
            else
            {
                return Content(Message.Error);
            }

        }
        /// <summary>
        /// 生成二维码  
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public byte[] GetQrCode()
        {
            var guid = Guid.NewGuid().ToString() + "|" + DateTime.Now.AddMinutes(5).ToString("yyyyMMddHHmm") + "token";
            //将这个guid存放到数据库
            //手机端扫描二维码后 对这个guid进行加密 再发送过来
            //服务端接受到数据后 再解密 并与数据库的guid对比
            return QrCode.GetFile(guid);
        }
        /// <summary>
        /// 验证 
        /// </summary>
        /// <returns></returns>
        [HttpPost]

        public ResponseModel ValidCode(string code)
        {
            var result = IdentityService.QrValid(DbContext, code);
            if (result)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
                };
                var token = new JwtSecurityToken(
                    new JwtHeader(new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims));
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return new ResponseModel(Message.Success, jwtToken, "");
            }
            else
            {
                return new ResponseModel(Message.Error);
            }
        }

        [Authorize]
        public string GetUser()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}