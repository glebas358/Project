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
    public partial class EmployerIssueViewModel : ObservableObject
    {
        private readonly IPackageService _packageService;

        public List<string> SearchTypes { get; } = new()
        {
            "По номеру посылки",
            "По номеру телефона",
            "По ФИО получателя"
        };

        public bool HasResultMessage => !string.IsNullOrWhiteSpace(ResultMessage);

        public bool HasDescription => !string.IsNullOrWhiteSpace(Description);

        
        public ObservableCollection<Package> FoundPackages { get; } = new();

        [ObservableProperty]
        private int _selectedSearchTypeIndex = 0;

        [ObservableProperty]
        private bool hasButtons = false;

        [ObservableProperty]
        private bool _isBusy = false;


        [ObservableProperty]
        private string _searchQuery = string.Empty;



        [ObservableProperty]
        private string _resultMessage = string.Empty;


        [ObservableProperty]
        private string _description = string.Empty;

        public SearchType CurrentSearchType => SelectedSearchTypeIndex switch
        {
            0 => SearchType.ByNumber,
            1 => SearchType.ByPhone,
            2 => SearchType.ByName,
            _ => SearchType.ByNumber
        };


        partial void OnSelectedSearchTypeIndexChanged(int value)
        {
           
            OnPropertyChanged(nameof(SearchPlaceholder));

            
            SearchQuery = string.Empty;
            ResultMessage = string.Empty;
            Description = string.Empty;
            HasButtons = false;
            FoundPackages.Clear();
        }

        partial void OnResultMessageChanged(string value)
        {
            OnPropertyChanged(nameof(HasResultMessage));
        }

        partial void OnDescriptionChanged(string value)
        {
            OnPropertyChanged(nameof(HasDescription));
        }



        public string SearchPlaceholder => CurrentSearchType switch
        {
            SearchType.ByNumber => "Введите номер (например, TRK-1001)",
            SearchType.ByPhone => "Введите телефон (например, +79001112233)",
            SearchType.ByName => "Введите ФИО (например, Иванов Иван)",
            _ => "Введите запрос"
        };

        public EmployerIssueViewModel(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;

            HapticFeedback.Default.Perform(HapticFeedbackType.Click);

            IsBusy = true;


            try
            {
                switch (CurrentSearchType)
                {
                    case SearchType.ByNumber:
                        

                        var package = await _packageService.SearchByNumberAsync(SearchQuery);
                        if (package == null)
                        {
                            ResultMessage = "Посылка не найдена";
                            Description = string.Empty;
                        }
                        else
                        {
                            ResultMessage = $"Посылка {package.Number} в зоне {package.StorageLocation.Zone}, ячейка {package.StorageLocation.Cell}";
                            Description = $"Продавец: {package.Seller}. Содержимое: {package.Contents}";
                            HasButtons = true;
                        }
                        break;

                    case SearchType.ByPhone:

                        

                        var packagePhone = await _packageService.SearchByPhoneAsync(SearchQuery);

                        if (packagePhone != null && packagePhone.Any())
                        {

                            foreach (var p in packagePhone)
                            {

                                FoundPackages.Add(p);
                                
                            } 
                                
                            ResultMessage = $"Найдено посылок: {packagePhone.Count}";
                            
                        }
                        else
                        { 
                            ResultMessage = "Посылка не найдена";
                            FoundPackages.Clear();

                        }

                        break;

                    case SearchType.ByName:
                        
                        var namePackage = await _packageService.SearchByNameAsync(SearchQuery);

                        if (namePackage != null && namePackage.Any())
                        {

                            foreach (var p in namePackage)
                            {

                                FoundPackages.Add(p);

                            }

                            ResultMessage = $"Найдено посылок: {namePackage.Count}";

                        }
                        else
                        {
                            ResultMessage = "Посылка не найдена";
                            FoundPackages.Clear();

                        }
                        break;


                }

            }
            catch (Exception ex) { ResultMessage = ex.Message; FoundPackages.Clear(); }

            finally { IsBusy = false; }












        }

            
        


    }
}
