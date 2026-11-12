using Microsoft.Extensions.Logging;
using Project.Application.Interfaces.Repositories;
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

            
            string apiBaseUrl = "http://";

           



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

            builder.Services.AddHttpClient<IOperationRepository, ApiOperationRepository>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromMinutes(5);
            })
            .AddHttpMessageHandler<AuthHeaderHandler>();

            builder.Services.AddHttpClient<IManagerNoteRepository, ApiManagerNoteRepository>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromMinutes(5);
            })
            .AddHttpMessageHandler<AuthHeaderHandler>();



            builder.Services.AddTransient<AuthHeaderHandler>();


            
            
            

            // UI
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<EmployerRegisterPage>();
            builder.Services.AddTransient<EmployerRegisterPageViewModel>();

            builder.Services.AddTransient<EmployerIssuePackage>();
            builder.Services.AddTransient<EmployerIssueViewModel>();

            builder.Services.AddTransient<ManagerOperationPage>();
            builder.Services.AddTransient<ManagerOperationViewModel>();

            builder.Services.AddTransient<ManagerNotesPage>();
            builder.Services.AddTransient<ManagerNotesViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
