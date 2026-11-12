using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Application.Interfaces.Repositories;
using Project.Domain.Models;
using System.Collections.ObjectModel;


namespace Project.UI.ViewModels
{
    public partial class ManagerOperationViewModel : ObservableObject
    {
        private readonly IOperationRepository _operationRepository;

        public ManagerOperationViewModel(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
            Operations = new ObservableCollection<Operation>();
        }

        public ObservableCollection<Operation> Operations { get; }

        [ObservableProperty]
        private DateTime _fromDate = DateTime.Today.AddDays(-7); 

        [ObservableProperty]
        private DateTime _toDate = DateTime.Today;

        [ObservableProperty]
        private bool _isBusy;

        [RelayCommand]
        private async Task LoadHistoryAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var history = await _operationRepository.GetHistoryAsync(FromDate, ToDate);

                Operations.Clear();

               
                foreach (var op in history)
                {
                    
                    op.Timestamp = op.Timestamp.ToLocalTime();
                    Operations.Add(op);
                }
            }
            catch (Exception ex)
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК");
                Operations.Clear();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
