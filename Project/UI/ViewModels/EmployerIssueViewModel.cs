using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Application.Interfaces.Repositories;
using Project.Domain.Enums;
using Project.Domain.Models;
using System.Collections.ObjectModel;




namespace Project.UI.ViewModels
{
    public partial class EmployerIssueViewModel : ObservableObject
    {
        private readonly IPackageRepository _apiPackageRepository;

        private readonly IReturnReasonRepository _returnReasonRepository;

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

        public EmployerIssueViewModel(IPackageRepository apiPackageRepository, IReturnReasonRepository returnReasonRepository)
        {
            _apiPackageRepository = apiPackageRepository;
            _returnReasonRepository = returnReasonRepository;
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
                        

                        var package = await _apiPackageRepository.SearchByNumberAsync(SearchQuery.Trim());
                        if (package != null)
                        {

        
                                FoundPackages.Add(package);


                            ResultMessage = $"Посылка найдена!";

                        }
                        else
                        {
                            ResultMessage = "Посылка не найдена";
                            FoundPackages.Clear();

                        }
                        break;

                    case SearchType.ByPhone:

                        

                        var packagePhone = await _apiPackageRepository.SearchByPhoneAsync(SearchQuery.Trim());

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
                        
                        var namePackage = await _apiPackageRepository.SearchByNameAsync(SearchQuery.Trim());

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

        [RelayCommand]
        private async Task IssuePackageAsync(Package selectedPackage)
        {
            if (selectedPackage == null) return;

            
            string result = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayPromptAsync(
                title: "Подтверждение выдачи",
                message: $"Введите номер посылки ({selectedPackage.Number}) для подтверждения:",
                accept: "Выдать",
                cancel: "Отмена");

            if (string.IsNullOrWhiteSpace(result)) return;

            
            if (result.Trim() != selectedPackage.Number)
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Введенный номер не совпадает!", "ОК");
                return;
            }

            try
            {
                var issuedPackage = await _apiPackageRepository.ExtraditionPackageAsync(selectedPackage.Number);

                if (issuedPackage != null)
                {
                    await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Успех", "Посылка выдана!", "ОК");
                    FoundPackages.Clear();
                    SearchQuery = string.Empty;
                }
            }
            catch (Exception ex)
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК");
            }
        }

        [RelayCommand]
        private async Task ReturnPackageAsync(Package selectedPackage)
        {
            if (selectedPackage == null) return;

            try
            {
               
                var reasons = await _returnReasonRepository.GetActiveReasonsAsync();
                if (reasons == null || reasons.Count == 0)
                {
                    await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Не удалось загрузить причины возврата.", "ОК");
                    return;
                }

                
                var reasonNames = reasons.Select(r => r.Name).ToArray();
                string selectedReasonName = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayActionSheetAsync(
                    "Выберите причину возврата",
                    "Отмена",
                    null,
                    reasonNames);

                
                if (string.IsNullOrEmpty(selectedReasonName) || selectedReasonName == "Отмена") return;

                
                var selectedReason = reasons.FirstOrDefault(r => r.Name == selectedReasonName);
                if (selectedReason == null) return;

                
                string confirmNumber = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayPromptAsync(
                    title: "Подтверждение возврата",
                    message: $"Введите номер посылки ({selectedPackage.Number}) для подтверждения:",
                    accept: "Вернуть",
                    cancel: "Отмена");

                if (string.IsNullOrWhiteSpace(confirmNumber)) return;

                if (confirmNumber.Trim() != selectedPackage.Number)
                {
                    await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Введенный номер не совпадает!", "ОК");
                    return;
                }

            
                bool success = await _apiPackageRepository.ReturnPackageAsync(selectedPackage.Number, selectedReason.Id);

                if (success)
                {
                    await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Успех", "Возврат успешно оформлен!", "ОК");

                    
                    if (FoundPackages.Contains(selectedPackage))
                    {
                        FoundPackages.Remove(selectedPackage);
                    }
                }
            }
            catch (Exception ex) { await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК"); }
        }





    }
}
