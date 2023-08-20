using Microsoft.EntityFrameworkCore;
using ACRPhone.Webhook.Models;
using ACRPhone.Webhook.Configuration;
using NLL.Webhook.Configuration;

namespace ACRPhone.Webhook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Make sure AppData Folder exist. We use it for sqlite db and elmah logs. AppData is not AppData from .net4 We simply like the name and use it. It could have been any folder name
            if (!Directory.Exists("AppData"))
            {
                Directory.CreateDirectory("AppData");
            }          

            builder.Services.AddDbContext<WebhookContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            // Extension method for configuring custom authentication
            builder.Services.AddCustomAuth();
            // Extension method for configuring application specific services (e.g. repositories)
            builder.Services.AddApplicationServices();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            var app = builder.Build();

            //Create DB and tables
            var webhookContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<WebhookContext>();
            webhookContext?.Database.EnsureCreated();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseFileServer();
            app.MapControllers();
            app.Run();

  

        }
    }
}
