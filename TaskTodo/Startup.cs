using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskTodo.ApplicationCore.Interfaces;
using TaskTodo.ApplicationCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;

namespace TaskTodo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 当需要从配置文件中获取配置的时候需要添加IConfiguration注入
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// 要声明（或者“关联”）具体的类到每个接口上，需要写在 StartUp 类的 ConfigureServices 方法里
        /// ConfigureServices 方法负责的是把东西添加到 服务容器 里，或者 ASP.NET Core 的说法是服务的集合。
        /// 
        /// 当一个请求进来，将会被发送到 TodoController，当控制器需要一个ITodoItemService 时，
        /// ASP.NET Core 会在 可用服务集合 里查找并自动给出 FakeTodoItemService。
        /// 因为服务是从服务容器里“注入(injected)”的，这个模式被称为 依赖注入(dependency injection)。
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //数据库服务注册,使用内存数据库
            //services.AddDbContext<ApplicationDbContext>(c =>c.UseInMemoryDatabase("TodoItem"));

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Indentity
            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            //仓储服务注册
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //应用服务注册
            services.AddScoped<ITodoItemService, TodoItemService>();

            //1添加AddMvc与AddControllersWithViews中间件
            //services.AddMvc 这行添加了一些服务，它们是 ASP.NET Core 系统内部依赖的
            //你在应用里所需的任何其它服务，也都要在这个地方添加到服务容器里。
            services.AddMvc();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //
            app.UseRouting();

            //引用静态文件
            app.UseStaticFiles();

            //添加认证
            app.UseAuthentication();

            //添加授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //1修改中间件终结点映射到路由
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
