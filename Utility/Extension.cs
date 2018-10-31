using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Utility
{
    public enum FileType
    {
        Image = 1,
        Excel = 2
    }
    public static class AllowdFormat
    {
        public static string[] Images { get; set; } = new string[11] { ".PNG", ".BMP", ".JPE", ".JPEG", ".JPG", ".JFIF", ".GIF", ".TIF", ".TIFF", ".DIB", ".PDF" };
        public static string[] Excel { get; set; } = new string[2] { ".XLS", ".XLSX" };
        public static bool IsAllowed(string fileName, FileType fileType)
        {
            fileName = fileName.ToUpper();
            var fileLen = fileName.Length;
            string[] unknownType;
            switch (fileType)
            {
                case FileType.Image:
                    unknownType = Images;
                    break;
                case FileType.Excel:
                    unknownType = Excel;
                    break;
                default:
                    unknownType = new string[] { };
                    break;
            }
            foreach (var item in unknownType)
            {
                var itemlen = item.Length;
                var index = fileName.LastIndexOf(item);
                if ((index + itemlen) == fileLen)
                {
                    return true;
                }
            }
            return false;
        }

    }    
    public class Extension
    {
    }
    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage =
            "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic owinContext = request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }
    }
}
