using Karim.CRUD.BLL.Services.DepartmentServices;
using Karim.CRUD.BLL.Services.EmployeeServices;
using Karim.CRUD.BLL.ThirdPartyServices.AttachmentService;
using Karim.CRUD.BLL.ThirdPartyServices.EmailSettings;
using Karim.CRUD.DAL.Entities.Identity;
using Karim.CRUD.DAL.Persistence.Data;
using Karim.CRUD.DAL.Persistence.Initializer;
using Karim.CRUD.DAL.Persistence.Repository.DepartmentRepository;
using Karim.CRUD.DAL.Persistence.Repository.EmployeeRepository;
using Karim.CRUD.DAL.Persistence.UnitOfWork;
using Karim.CRUD.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Karim.CRUD.PL;

public class Program
{
    public static async Task Main(string[] args)
    {
        #region Configure Services
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        #region Configurations For DbContext File
        builder.Services
            .AddDbContext<ApplicationDbContext>(
                optionsBuilder => optionsBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            ); 
        #endregion

        #region Dependancy Injection For Department Module

        builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();

        #endregion

        #region Dependancy Injection For Employee Module

        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped(typeof(IAttachmentService), typeof(AttachmentService));

        #endregion

        #region Dependancy Injection For UnitOfWork
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Dependancy Injection For Identity Module
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
        {
            //When The Acc Shoild Be Locked Out ?
            identityOptions.Lockout.AllowedForNewUsers = true;
            identityOptions.Lockout.MaxFailedAccessAttempts = 10;
            //For How Long The Acc Will Be Locked ?
            identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(18);

            //If I Want To Make The Acc Be Confirmed By Phone Number & Email And Not Opend Till The Acc Is Confirmed
            //identityOptions.SignIn.RequireConfirmedAccount = true;
            //identityOptions.SignIn.RequireConfirmedPhoneNumber = true;
            //identityOptions.SignIn.RequireConfirmedEmail = true;

            //Email That Will Be Registered Must Be Unique
            identityOptions.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        #endregion

        #region Dependancy Injection For Application Initializer
        builder.Services.AddScoped(typeof(IDbInitializer), typeof(DbInitializer));
        #endregion

        #region Configure MailSettings Properties To Get It's Values From appsettings

        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
        builder.Services.AddTransient(typeof(IEmailSettings), typeof(EmailSettings));

        #endregion

        #endregion

        var app = builder.Build();
        await app.InitializeDbAsync();

        #region Configure Kestrell Middleware
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        #endregion

        app.Run();
    }
}
