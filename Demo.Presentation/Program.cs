using Demo.BLL.Services;
using Demo.DAL.Data.Context;
using Demo.DAL.Models;
using Demo.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService,EmployeeService>();



            builder.Services.AddAutoMapper(typeof(BLL.AssemblyReference).Assembly);
            //builder.Services.AddScoped(typeof(DAL.Repositories.IGenericRepository), typeof(IGenericRepository));

             builder.Services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
             builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            ///builder.Services.AddScoped(typeof(IGenericRepository), typeof(IGenericRepository));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

          //  app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
