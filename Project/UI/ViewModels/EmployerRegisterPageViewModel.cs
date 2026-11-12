using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Application.Interfaces.Repositories;

namespace Project.UI.ViewModels
{
    public partial class EmployerRegisterPageViewModel : ObservableObject
    {
        private readonly IPackageRepository _apiPackageRepository;

        [ObservableProperty]
        private string _number = string.Empty;

        [ObservableProperty]
        private string _zone = string.Empty;

        [ObservableProperty]
        private string _cell = string.Empty;

       


        [ObservableProperty]
        private bool _isBusy = false;

        public EmployerRegisterPageViewModel(IPackageRepository apiPackageRepository)
        {
            _apiPackageRepository = apiPackageRepository;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Number) || string.IsNullOrWhiteSpace(Zone) || string.IsNullOrWhiteSpace(Cell)) return;

            HapticFeedback.Default.Perform(HapticFeedbackType.Click);

            IsBusy = true;

            try
            {

                var package = await _apiPackageRepository.RegisterPackageAsync(Number.Trim(), Zone.Trim(), Cell.Trim());


                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Успех!","Посылка успешно зарегестрирована!", "ОК");

                Number = string.Empty;
                Zone = string.Empty;
                Cell = string.Empty;

            }
            catch (Exception ex) { await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК"); }

            finally { IsBusy = false; }




        }



    }
}
