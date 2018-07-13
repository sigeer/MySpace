using System;
using StackExchange.Redis;
namespace Utility.RedisHelper
{
    public class RedisHelper
    {
        private ConnectionMultiplexer client;
        private string host;
        public void GetConn()
        {
            if (client == null || client.IsConnected)
            {
                client = ConnectionMultiplexer.Connect(host);
            }
            //return client;
        }
        public RedisHelper(string host)
        {
            this.host = host;
            GetConn();
        }
        public void SetValue(string key,string value)
        {
            RedisKey redisKey = key;
            RedisValue redisValue = value;
            client.GetDatabase().StringAppend(redisKey,value);
        }
        public string GetValue(string key)
        {
            RedisKey redisKey = key;
            return client.GetDatabase().StringGet(redisKey);
        }
    }
}