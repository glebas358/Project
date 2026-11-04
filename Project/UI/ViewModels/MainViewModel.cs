
using CommunityToolkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Application.Interfaces.Services;
using Project.Application.Services;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;


namespace Project.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        //private readonly IDeadlineService _deadlineService;

        [ObservableProperty]
        private string _login = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private UserRole _selectedRole = UserRole.Employee;

        [ObservableProperty]
        private string _resultMessage = string.Empty;

        [ObservableProperty]
        private bool _isBusy = false;


        public List<UserRole> Roles { get; } = new()
        {
        UserRole.Employee,
        UserRole.Manager
        };
        
        public MainViewModel(IAuthService authService)
        {
            _authService = authService;
            //_deadlineService = deadlineService;
        }

        [RelayCommand]
        private async Task CheckAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) && string.IsNullOrWhiteSpace(Password)) return;

            HapticFeedback.Default.Perform(HapticFeedbackType.Click);

            IsBusy = true;
            try
            {
                var employee = await _authService.LoginAsync(Login, Password);


                if (employee == null)
                {
                    ResultMessage = "Неверный логин или пароль";
                    return;
                }

                

                if (employee.Role == UserRole.Employee)
                {


                    //ResultMessage = $"Добро пожаловать, {employee.Name} ({employee.Role})";
                    //_ = Task.Run(async () => await _deadlineService.CheckExpiredPackagesAsync());




                    if (Microsoft.Maui.Controls.Application.Current?.MainPage is AppShell shell)
                    {
                     shell.InitializeAsEmployee();
                    }


                }
                else if (employee.Role == UserRole.Manager)
                {
                    ResultMessage = $"Добро пожаловать, {employee.Name} ({employee.Role})";
                }

            }
            catch (Exception ex) { ResultMessage = $"Ошибка передачи данных. Попробуйте позднее \n{ex}"; }

            finally { IsBusy = false; }
        }

    }
}
