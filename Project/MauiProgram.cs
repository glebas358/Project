using Microsoft.Extensions.Logging;
using Project.Application.Interfaces.Repositories;
using Project.Application.Interfaces.Services;
using Project.Application.Services;
using Project.Infrastructure.Repositories;
using Project.UI.ViewModels;
using Project.UI.Views;
using Project.ViewModels;
using System.Net.Http;

namespace Project
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            
           

         
            string apiBaseUrl = "http://";





            builder.Services.AddSingleton(sp => new HttpClient
            {
                BaseAddress = new Uri(apiBaseUrl),
                Timeout = TimeSpan.FromMinutes(5) 
            });


            // Infrastructure
            builder.Services.AddTransient<IPackageRepository, ApiPackageRepository>();
            //builder.Services.AddTransient<IClientRepository, ApiClientRepository>();
            //builder.Services.AddTransient<IStorageLocationRepository, ApiStorageLocationRepository>();
            //builder.Services.AddTransient<IOperationRepository, ApiOperationRepository>();
            builder.Services.AddTransient<IEmployeeRepository, ApiEmployeeRepository>();

            // Application
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IPackageService, PackageService>();
            

            // UI
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<EmployerRegisterPage>();
            builder.Services.AddTransient<EmployerRegisterPageViewModel>();

            builder.Services.AddTransient<EmployerIssuePackage>();
            builder.Services.AddTransient<EmployerIssueViewModel>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
