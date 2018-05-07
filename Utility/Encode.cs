using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class Encode
    {
        private Encode()
        {
            CreateRSAKey();
        }
        public static Encode GetInstance()
        {
            Encode en = new Encode();
            return en;
        }
        /// <summary>
        /// 创建RSA公钥私钥
        /// </summary>
        public void CreateRSAKey()
        {
            //设置[公钥私钥]文件路径
            string privateKeyPath = @"PrivateKey.xml";
            string publicKeyPath = @"PublicKey.xml";
            //创建RSA对象
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //生成RSA[公钥私钥]
            string privateKey = rsa.ToXmlString(true);
            string publicKey = rsa.ToXmlString(false);
            //将密钥写入指定路径
            File.WriteAllText(privateKeyPath, privateKey);//文件内包含公钥和私钥
            File.WriteAllText(publicKeyPath, publicKey);//文件内只包含公钥
        }
        /// <summary>
        /// 使用RSA实现加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <returns></returns>
        public string RSAEncrypt(string data)
        {
            //C#默认只能使用[公钥]进行加密(想使用[公钥解密]可使用第三方组件BouncyCastle来实现)
            string publicKeyPath = @"PublicKey.xml";
            string publicKey = File.ReadAllText(publicKeyPath);
            //创建RSA对象并载入[公钥]
            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
            rsaPublic.FromXmlString(publicKey);
            //对数据进行加密
            byte[] publicValue = rsaPublic.Encrypt(Encoding.UTF8.GetBytes(data), false);
            string publicStr = Convert.ToBase64String(publicValue);//使用Base64将byte转换为string
            return publicStr;
        }
        /// <summary>
        /// 使用RSA实现解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <returns></returns>
        public string RSADecrypt(string data)
        {
            //C#默认只能使用[私钥]进行解密(想使用[私钥加密]可使用第三方组件BouncyCastle来实现)
            string privateKeyPath = @"PrivateKey.xml";
            string privateKey = File.ReadAllText(privateKeyPath);
            //创建RSA对象并载入[私钥]
            RSACryptoServiceProvider rsaPrivate = new RSACryptoServiceProvider();
            rsaPrivate.FromXmlString(privateKey);
            //对数据进行解密
            byte[] privateValue = rsaPrivate.Decrypt(Convert.FromBase64String(data), false);//使用Base64将string转换为byte
            string privateStr = Encoding.UTF8.GetString(privateValue);
            return privateStr;
        }
    }
}





