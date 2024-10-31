namespace UpRentTask.Views;

public partial class EditUsersView : UserControl
{
    public EditUsersView()
    {
        InitializeComponent();
        
        DataContext = Ioc.Default.GetService<EditUsersViewModel>();
    }
}