using Project.UI.Views;

namespace Project
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EmployerRegisterPage), typeof(EmployerRegisterPage));
            Routing.RegisterRoute(nameof(EmployerIssuePackage), typeof(EmployerIssuePackage));


            InitializeAsUnauthenticated();

        }

        public void InitializeAsUnauthenticated()
        {
            Items.Clear();

          
            var loginContent = new ShellContent
            {
                
                ContentTemplate = new DataTemplate(typeof(MainPage)) 
            };

            Items.Add(loginContent);
        }

        public void InitializeAsEmployee()
        {
            Items.Clear();

            var tabBar = new TabBar();


            tabBar.Items.Add(new ShellContent
            {
                Title = "Прием",
                ContentTemplate = new DataTemplate(typeof(EmployerRegisterPage))

            });

            tabBar.Items.Add(new ShellContent
            {
                Title = "Выдача",
                ContentTemplate = new DataTemplate(typeof(EmployerIssuePackage))

            });

            Items.Add(tabBar);
        }

        public void InitializeAsManager()
        {
            Items.Clear();

            var tabBar = new TabBar();


            tabBar.Items.Add(new ShellContent
            {
                Title = "Журнал операций",
                ContentTemplate = new DataTemplate(typeof(ManagerOperationPage))

            });

            tabBar.Items.Add(new ShellContent
            {
                Title = "Заметки",
                ContentTemplate = new DataTemplate(typeof(ManagerNotesPage))

            });



            Items.Add(tabBar);
  
        }
    }
}
