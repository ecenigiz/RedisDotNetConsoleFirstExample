using RedisDotNetConsoleFirstExample.Entities;
using RedisDotNetConsoleFirstExample.Redis;
using RedisDotNetConsoleFirstExample.Redis.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace RedisDotNetConsoleFirstExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ICache redisCache = new RedisCache();

            var userToBeCached = new User
            {
                Id = 1,
                FirstName = "ece",
                LastName = "nigiz"
            };

            var key1 = "user" + 1;

            redisCache.Set(key1, userToBeCached, DateTime.Now.AddMinutes(30));//30 dakikalığına user objemizi redis'e atıyoruz

            if (redisCache.Exists(key1))
            {
                var userRedisResponse = redisCache.Get<User>(key1);
                Console.WriteLine(userRedisResponse.FirstName+ userRedisResponse.LastName);
            }

            //list üzerinden işlemler;
            
            var listKey = "itemList";
            var townListToCached = prepareDataToCache();
             foreach (var item in townListToCached)
             {
                 redisCache.Set(listKey + item.Id.ToString(), item, DateTime.Now.AddMinutes(30));//30 dakikalığına user objemizi redis'e atıyoruz
             }

            foreach (var item in townListToCached)
            {
                if (redisCache.Exists(listKey + item.Id.ToString()))
                {
                    var userRedisResponse = redisCache.Get<Town>(listKey + item.Id.ToString());
                    Console.WriteLine(userRedisResponse.Name);
                }
            }
            Console.ReadLine();
        }


        static Town[] prepareDataToCache()
        {
            Town[] towns = {
                new Town { Id = 1, Name = "Kadıköy",City = new City{ Id = 1, Name = "Istanbul", Country = "Türkiye"}},
                new Town { Id = 1, Name = "Beşiktaş",City = new City{ Id = 1, Name = "Istanbul", Country = "Türkiye"}}
            };
            return towns;
        }
    }
}