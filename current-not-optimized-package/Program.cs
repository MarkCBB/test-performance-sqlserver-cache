using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.SqlServer;

namespace dotnet_cache_performance
{
    class Program    
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Process started");
            
            var cache = new SqlServerCache(new SqlServerCacheOptions()
            {
                ConnectionString = "Server=localhost;Initial Catalog=CacheSampleDb; Trusted_Connection=True;",
                SchemaName = "dbo",
                TableName = "CacheSample"
            });

            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(10)
            };

            string keyTestName = "test";
            byte[] dataArray = new byte[1000];
            byte counter = 0;
            for (int i = 0; i < 1000; i++)
            {
                dataArray[i] = counter++;
            }

            cache.Set(keyTestName, dataArray, options);

            // this is to warm the connection and IO with the database
            for (int i = 0; i < 1000; i++)
            {
                cache.Get(keyTestName);
            }
            
            int nQueries = 100000;
            var timer = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < nQueries; i++)
            {
                cache.Get(keyTestName);
            }
            timer.Stop();

            Console.WriteLine("A total of " + nQueries + " queries took: " + timer.ElapsedMilliseconds + " millis");
        }
    }
}
