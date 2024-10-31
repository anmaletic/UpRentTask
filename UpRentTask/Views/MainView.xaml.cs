namespace UpRentTask.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        
        DataContext = Ioc.Default.GetService<MainViewModel>();
    }
}