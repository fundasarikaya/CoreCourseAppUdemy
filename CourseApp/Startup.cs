using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.Data.Abstract;
using CourseApp.Data.Concrete;
using CourseApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CourseApp
{
    //dışarıdan dahil edilmek istenilen harici kutuphaneler buradan eklenir yada core icerisindeki kutuphanelerinde buradan tanıtmamız gerekir
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //mvc ye cevirmek icin
            services.AddMvc();
            //datacontext dosyasını tanıtmak icin public IConfiguration Configuration; ekledik ve ctor ekledik
            services.AddDbContext<DataContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataConnection"));
                options.EnableSensitiveDataLogging(true);
            }
            );
            services.AddTransient<ICourseRepository, EfCourseRepository>();
            services.AddTransient<IInstructorRepository, EfInstructorRepository>();
            services.AddTransient<IGenericRepository<Contact>,GenericRepository<Contact>>();
            services.AddTransient<IGenericRepository<Address>, GenericRepository<Address>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});


            app.UseDeveloperExceptionPage();//bir hata oldugunda geliştirme asamasında gorelim
            app.UseStatusCodePages();//hata sayfasını bize gostersin diye
            app.UseStaticFiles();
            //varsayılan olarak wwwwroot icersindeki dosyaları ulaşılabilir olarak ayarlıyoruz.
            //npm ekledigimiz icin onun klasorunu de dahil etmemiz gerekir cunku usestaticfiles default olarak wwwroot klasorunu dahil eder
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
                RequestPath = new PathString("/vendor")
                //vendor'a istek geldiginde node_modules klasoru erişilebilir olacak
            });
            //controller/action/id?
            app.UseMvcWithDefaultRoute();
            //bu ayarlardan sonra solution kısmına controller models ve views klasorlerini oluştur.
        }
    }
}
