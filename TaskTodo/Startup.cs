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
        /// ����Ҫ�������ļ��л�ȡ���õ�ʱ����Ҫ���IConfigurationע��
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// Ҫ���������ߡ���������������ൽÿ���ӿ��ϣ���Ҫд�� StartUp ��� ConfigureServices ������
        /// ConfigureServices ����������ǰѶ�����ӵ� �������� ����� ASP.NET Core ��˵���Ƿ���ļ��ϡ�
        /// 
        /// ��һ��������������ᱻ���͵� TodoController������������Ҫһ��ITodoItemService ʱ��
        /// ASP.NET Core ���� ���÷��񼯺� ����Ҳ��Զ����� FakeTodoItemService��
        /// ��Ϊ�����Ǵӷ��������ע��(injected)���ģ����ģʽ����Ϊ ����ע��(dependency injection)��
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //���ݿ����ע��,ʹ���ڴ����ݿ�
            //services.AddDbContext<ApplicationDbContext>(c =>c.UseInMemoryDatabase("TodoItem"));

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Indentity
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //   .AddEntityFrameworkStores<ApplicationDbContext>()
            //   .AddDefaultTokenProviders();

            //�ִ�����ע��
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //Ӧ�÷���ע��
            services.AddScoped<ITodoItemService, TodoItemService>();

            //1���AddMvc��AddControllersWithViews�м��
            //services.AddMvc ���������һЩ���������� ASP.NET Core ϵͳ�ڲ�������
            //����Ӧ����������κ���������Ҳ��Ҫ������ط���ӵ����������
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

            //���þ�̬�ļ�
            app.UseStaticFiles();

            //�����֤
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //1�޸��м���ս��ӳ�䵽·��
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
