using Project.UI.ViewModels;

namespace Project.UI.Views;

public partial class ManagerOperationPage : ContentPage
{
	public ManagerOperationPage(ManagerOperationViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}