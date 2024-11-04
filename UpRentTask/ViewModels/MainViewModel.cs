namespace UpRentTask.ViewModels;

public partial class MainViewModel : ObservableObject, IAsyncInitialization
{
    private readonly ILoggedInUser _loggedInUser;
    private readonly IUserService _userService;
    private readonly IViewManager _viewManager;

    public Task Initialization { get; }

    [ObservableProperty] private UserControl? _activeView;
    [ObservableProperty] private DisplayMessageModel _message = new();

    public MainViewModel(ILoggedInUser loggedInUser, IUserService userService, IRoleService roleService,
        IViewManager viewManager)
    {
        _loggedInUser = loggedInUser;
        _userService = userService;
        _viewManager = viewManager;

        WeakReferenceMessenger.Default.Register<ChangeViewMessage>(this, (r, m) =>
        {
            ActiveView = m.View switch
            {
                "AddUser" => _viewManager.GetView("EditUsersView"),
                "EditUser" => _viewManager.GetView("EditUsersView", m.Parameters),
                "Users" => _viewManager.GetView("UsersView"),
                _ => throw new ArgumentOutOfRangeException()
            };
        });


        WeakReferenceMessenger.Default.Register<DisplayDialogMessage>(this, (_, m) => { DisplayDialog(m.Msg); });

        Initialization = Init();
    }

    private async Task Init()
    {
        var result = (await _userService.GetAll())[0];

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
        
        ActiveView = _viewManager.GetView("UsersView");
    }

    private void DisplayDialog(DisplayMessageModel msg)
    {
        Message = msg;
    }
}