namespace UpRentTask.ViewModels;

public partial class EditUsersViewModel : ObservableObject
{
    private readonly ILoggedInUser _loggedInUser;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    [ObservableProperty] private ObservableCollection<RoleModel>? _roles;
    [ObservableProperty] private UserModel _user;
    [ObservableProperty] private string _username = "";

    public EditUsersViewModel(ILoggedInUser loggedInUser, IRoleService roleService, IUserService userService)
    {
        _loggedInUser = loggedInUser;
        _roleService = roleService;
        _userService = userService;

        Task.Run(async () => await Init());
    }
    
    private async Task Init()
    {
        Roles = new ObservableCollection<RoleModel>(await _roleService.GetAll());
    }
    
    [RelayCommand]
    private async Task AddUser()
    {
        User = new UserModel()
        {
            Username = Username
        };

        foreach (var role in Roles)
        {
            if (role.IsSelected)
            {
                User.Roles.Add(role);
            }
        }
        
        var result = await _userService.Add(User, _loggedInUser.UserId);
        
        
        
        
    }
    

    [RelayCommand]
    private void LeaveForm()
    {
        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("Users"));
    }
}