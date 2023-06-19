using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using webapp.Models;
namespace webapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ����������� � SQL �������
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); // ���� ������ ����������� �� DefaultConnection � appsettings.json
            });
            //services.AddControllersWithViews();
            services.AddMvc(); // ����� MVC ������ �����������

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) // ���������� ���������� ��� ������� �������������� ������ ����� ������
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection(); // �������� http �� https - ��� ������� ������ ����� ��������
            app.UseStaticFiles(); // ������ �� ��������

            app.UseRouting(); // �������� ������������� - "��������" ����

            //app.UseAuthorization(); // ��������� ����������� - ����� �� ������������

            // ��������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "create",
                    pattern: "{controller=Home}/{action=EmployeeCreate}");
                endpoints.MapControllerRoute(
                    name: "detail",
                    pattern: "{controller=Home}/{action=EmployeeDetail}/{id?}");
                endpoints.MapControllerRoute(
                    name: "update",
                    pattern: "{controller=Home}/{action=EmployeeEdit}/{id?}");
                endpoints.MapControllerRoute(
                    name: "delete",
                    pattern: "{controller=Home}/{action=EmployeeDelete}/{id?}");
                
            });
        }
    }
}
