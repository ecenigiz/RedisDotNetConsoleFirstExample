﻿using Newtonsoft.Json;
using RedisDotNetConsoleFirstExample.Redis.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace RedisDotNetConsoleFirstExample.Redis
{
    class RedisCache : ICache
    {
        private readonly IDatabase _redisDb;

        //Connection bilgisi initialize anında alınıyor
        public RedisCache()
        {
            _redisDb = RedisConnectionFactory.Connection.GetDatabase();
        }

        //Redis'e json formatında set işlemi yapılan metot
        public void Set<T>(string key, T objectToCache, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(DateTime.Now);
            var jsonObjectSerialize = JsonConvert.SerializeObject(objectToCache);
            _redisDb.StringSet(key, jsonObjectSerialize, expireTimeSpan);
        }

        //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
        public T Get<T>(string key)
        {
            var redisObject = _redisDb.StringGet(key);
            var jsonObjectDeserialize = JsonConvert.DeserializeObject<T>(redisObject);
            return redisObject.HasValue ? jsonObjectDeserialize : Activator.CreateInstance<T>();
        }

        //Redis te var olan key-value değerlerini silen metot
        public void Delete(string key)
        {
            _redisDb.KeyDelete(key);
        }

        //Gönderilen key parametresine göre redis'te bu key var mı yok mu bilgisini return eden metot
        public bool Exists(string key)
        {
            return _redisDb.KeyExists(key);
        }

        //Redis bağlantısını Dispose eden metot
        public void Dispose()
        {
            RedisConnectionFactory.Connection.Dispose();

        }
       
    }
}