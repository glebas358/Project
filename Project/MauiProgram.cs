using Microsoft.Extensions.Logging;
using Project.Application.Interfaces.Repositories;
using Project.Application.Interfaces.Services;
using Project.Application.Services;
using Project.Infrastructure;
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
            
            //через раздачу на телефоне (нужен впн)
            //string apiBaseUrl = "http://10.103.140.227:5071/";

            //при разработке
            string apiBaseUrl = "http://192.168.0.109:5071/";



            builder.Services.AddHttpClient<IEmployeeRepository, ApiEmployeeRepository>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromMinutes(5);
            })
            .AddHttpMessageHandler<AuthHeaderHandler>();

            builder.Services.AddHttpClient<IPackageRepository, ApiPackageRepository>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromMinutes(5);
            })
            .AddHttpMessageHandler<AuthHeaderHandler>();

            builder.Services.AddHttpClient<IReturnReasonRepository, ApiReturnReasonRepository>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromMinutes(5);
            })
            .AddHttpMessageHandler<AuthHeaderHandler>();



            // Infrastructure


            builder.Services.AddTransient<AuthHeaderHandler>();

          

            





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
