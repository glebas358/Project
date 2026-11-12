using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Application.Interfaces.Repositories; 
using Project.Domain.Enums;



namespace Project.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IEmployeeRepository _apiEmployeRepository;


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
        
        public MainViewModel(IEmployeeRepository apiEmployeRepository)
        {
            _apiEmployeRepository = apiEmployeRepository;
            
        }

        [RelayCommand]
        private async Task CheckAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) && string.IsNullOrWhiteSpace(Password)) return;

            HapticFeedback.Default.Perform(HapticFeedbackType.Click);

            IsBusy = true;
            try
            {
                var employee = await _apiEmployeRepository.LoginAsync(Login.Trim(), Password);



                

                if (employee.Role == UserRole.Employee)
                {


                    if (Microsoft.Maui.Controls.Application.Current?.MainPage is AppShell shell)
                    {
                     shell.InitializeAsEmployee();
                    }


                }
                else if (employee.Role == UserRole.Manager)
                {
                    if (Microsoft.Maui.Controls.Application.Current?.MainPage is AppShell shell)
                    {
                        shell.InitializeAsManager();
                    }
                }

            }
            catch (Exception ex) { await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК"); }

            finally { IsBusy = false; }
        }

    }
}
