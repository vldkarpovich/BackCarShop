using BackCarShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;

namespace BackCarShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BsonClassMap.RegisterClassMap<Location>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(l => l._long).SetElementName("long");
            });

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
