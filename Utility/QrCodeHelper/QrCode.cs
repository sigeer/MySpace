using QRCoder;
using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;

namespace Utility.QrCodeHelper
{
    public class QrCode
    {
        private static Bitmap CreateQrCode(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(50);
            return qrCodeImage;
        }
        public static byte[] GetFile(string text)
        {
            var temp = CreateQrCode(text);
            using (MemoryStream stream = new MemoryStream())
            {
               temp.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
        
    }
}
