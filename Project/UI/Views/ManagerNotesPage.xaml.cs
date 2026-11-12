using Project.UI.ViewModels;

namespace Project.UI.Views;

public partial class ManagerNotesPage : ContentPage
{
	public ManagerNotesPage(ManagerNotesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        
        if (BindingContext is ManagerNotesViewModel viewModel)
        {
            
            await viewModel.LoadNotesCommand.ExecuteAsync(null);
        }
    }
}