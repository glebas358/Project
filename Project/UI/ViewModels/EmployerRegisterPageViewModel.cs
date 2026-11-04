using CommunityToolkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Project.Application.Interfaces.Services;
using Project.Application.Services;
using Project.Domain.Enums;
using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
namespace Project.UI.ViewModels
{
    public partial class EmployerRegisterPageViewModel : ObservableObject
    {
        private readonly IPackageService _packageService;

        [ObservableProperty]
        private string _number = string.Empty;

        [ObservableProperty]
        private string _zone = string.Empty;

        [ObservableProperty]
        private string _cell = string.Empty;

        [ObservableProperty]
        private string _resultMessage = string.Empty;
        [ObservableProperty]
        private string _resultColor = string.Empty;

        [ObservableProperty]
        private bool _isBusy = false;

        public EmployerRegisterPageViewModel(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Number) || string.IsNullOrWhiteSpace(Zone) || string.IsNullOrWhiteSpace(Cell)) return;

            HapticFeedback.Default.Perform(HapticFeedbackType.Click);

            IsBusy = true;

            try
            {

                var package = await _packageService.RegistrAsync(Number, Zone, Cell);

                if (package == null)
                {
                    ResultColor = "red";
                    ResultMessage = "Ошибка. Посылка отсутствует или уже зарегистрирована";
                    Number = string.Empty;
                    Zone = string.Empty;
                    Cell = string.Empty;
                    return;
                }

                ResultColor = "#009BAC";
                ResultMessage = "Посылка успешно зарегистрирована!";
                Number = string.Empty;
                Zone = string.Empty;
                Cell = string.Empty;

            }
            catch (Exception ex) { ResultMessage = ex.Message; }

            finally { IsBusy = false; }




        }



    }
}
