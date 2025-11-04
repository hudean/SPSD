
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using SPSD.WebApi.Filters;

namespace SPSD.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(options => {
                options.Filters.Add<GlobalActionFilter>();
            }).AddNewtonsoftJson();
            //builder.Services.AddControllers().AddJsonOptions(config =>
            //{
            //    //此设定解决JsonResult中文被编码的问题
            //    config.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

            //    config.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            //    config.JsonSerializerOptions.Converters.Add(new DateTimeNullableConvert());
            //});
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddCors();

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // Ensure database is created and migrations are applied
                //dbContext.Database.Migrate();
                var isSuccess = dbContext.Database.EnsureCreated();
                if (isSuccess)
                {
                    DbInitializer.Initialize(dbContext);
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
              
            }
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                       //.AllowCredentials();
            });

            app.UseDefaultFiles();//使用静态文件

            // 启用静态文件中间件
            app.UseStaticFiles();

           
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
            //});

            app.UseAuthorization();


            app.MapControllers();
            // 启用前端文件的路由（确保前端文件放在wwwroot目录下）
            app.MapFallbackToFile("index.html");
            // 启用前端文件的路由，访问根路径时返回 wwwroot/dist/index.html
            //app.MapFallbackToFile("/dist/{*path:nonfile}", "dist/index.html");
            app.Run();
        }
    }
}
//https://www.cnblogs.com/gyhgis/p/15517043.html
//https://www.cnblogs.com/PrintY/p/15979690.html
//https://www.cnblogs.com/xxxin/p/17888558.html