namespace UpRentTask.ViewModels;

public partial class MainViewModel : ObservableObject, IAsyncInitialization
{
    private readonly ILoggedInUser _loggedInUser;
    private readonly IUserService _userService;
    
    public Task Initialization { get; }
    
    [ObservableProperty] private UserControl? _activeView;

    public MainViewModel(ILoggedInUser loggedInUser, IUserService userService, IRoleService roleService)
    {
        _loggedInUser = loggedInUser;
        _userService = userService;

        WeakReferenceMessenger.Default.Register<ChangeViewMessage>(this, (r, m) =>
        {
            ActiveView = m.View switch
            {
                "AddUser" => new EditUsersView(),
                "EditUser" => new EditUsersView { UserId = int.Parse(m.Parameters!["UserId"]) },
                "Users" => new UsersView(),
                _ => throw new ArgumentOutOfRangeException()
            };
        });

        Initialization = Init();
    }

    private async Task Init()
    {
        var result = await _userService.GetById(1);

        if (result is not null)
        {
            _loggedInUser.UserId = result.UserId;
            _loggedInUser.Username = result.Username;
            _loggedInUser.CreatedBy = result.CreatedBy;
            _loggedInUser.CreatedDate = result.CreatedDate;
            _loggedInUser.ModifiedBy = result.ModifiedBy;
            _loggedInUser.ModifiedDate = result.ModifiedDate;
            _loggedInUser.Roles = result.Roles;
        }
        
        Application.Current.Dispatcher.Invoke(() => ActiveView = new UsersView());
    }
}