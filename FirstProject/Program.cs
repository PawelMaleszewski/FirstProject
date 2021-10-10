using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FirstProject
{
    public class Program
    {
        private static IStore _store = new Store(); // here create new instance
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Configure(app =>
                    {
                        app.UseDeveloperExceptionPage();
                        app.UseRouting();

                        app.UseDefaultFiles();
                        app.UseStaticFiles();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/products", async context =>
                            {
                                await context.Response.WriteAsync(JsonSerializer.Serialize(_store.GetProducts()));
                            });
                            endpoints.MapPost("/product", async context =>
                            {
                                var name = context.Request.Query["name"].ToString();
                                var price = decimal.Parse(context.Request.Query["price"].ToString());
                                var category = (Category)int.Parse(context.Request.Query["category"].ToString());
                                await context.Response.WriteAsync(JsonSerializer.Serialize(_store.Add(name, price, category)));
                            });
                            endpoints.MapDelete("/product", async context =>
                            {
                                var id = int.Parse(context.Request.Query["Id"].ToString());
                                await context.Response.WriteAsync(_store.Remove(id).ToString());
                            });
                            endpoints.MapPut("/product", async context =>
                            {
                                // todo frontend
                                var id = int.Parse(context.Request.Query["name"].ToString());
                                var name = context.Request.Query["name"].ToString();
                                var price = decimal.Parse(context.Request.Query["price"].ToString());
                                var category = (Category)int.Parse(context.Request.Query["category"].ToString());
                                await context.Response.WriteAsync(_store.Update(id, name, price, category).ToString());
                            });
                        });
                    });
                });
    }
}