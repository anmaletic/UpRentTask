namespace UpRentTask.Views;

public partial class EditUsersView : UserControl
{
    public static readonly DependencyProperty UserIdProperty = 
        DependencyProperty.Register(nameof(UserId), typeof(int), typeof(EditUsersView), new PropertyMetadata(-1));

    public int UserId
    {
        get => (int)GetValue(UserIdProperty);
        set => SetValue(UserIdProperty, value);
    }
    
    public EditUsersView()
    {
        InitializeComponent();
        
        Loaded += OnLoaded;
        
        DataContext = Ioc.Default.GetService<EditUsersViewModel>();
    }
    
    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is EditUsersViewModel vm)
        {
            await vm.Initialization;
            await vm.LoadUser(UserId);
        }
    }
}