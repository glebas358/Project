
using Project.UI.ViewModels;

namespace Project.UI.Views;

public partial class EmployerIssuePackage : ContentPage
{
	public EmployerIssuePackage(EmployerIssueViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}