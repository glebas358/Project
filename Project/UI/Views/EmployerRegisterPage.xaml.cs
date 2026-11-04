using Project.UI.ViewModels;
using Project.ViewModels;

namespace Project;

public partial class EmployerRegisterPage : ContentPage
{
	
	public EmployerRegisterPage(EmployerRegisterPageViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}