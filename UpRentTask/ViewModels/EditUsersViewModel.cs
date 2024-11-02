using UpRentTask.Library.Validators;

namespace UpRentTask.ViewModels;

public partial class EditUsersViewModel : ObservableObject, IAsyncInitialization
{
    private readonly ILoggedInUser _loggedInUser;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;
    
    public Task Initialization { get; }

    [ObservableProperty] private UserModel _user;
    private bool _isEdit;

    [ObservableProperty] private ObservableCollection<RoleModel> _roles;
    [ObservableProperty] private string _username = "";
    [ObservableProperty] private string _validationMsg = "";

    public EditUsersViewModel(ILoggedInUser loggedInUser, IRoleService roleService, IUserService userService)
    {
        _loggedInUser = loggedInUser;
        _roleService = roleService;
        _userService = userService;

        Initialization = Init();
    }

    public async Task LoadUser(int userId)
    {
        if (userId != -1 )
        {
            _isEdit = true;
            User = (await _userService.GetById(userId))!;
            Username = User.Username;
            foreach (var role in Roles)
            {
                role.IsSelected = User.Roles.Any(x => x.Id == role.Id);
            }
        }
        else
        {
            _isEdit = false;
            User = new UserModel();
        }
    }

    private async Task Init()
    {
        Roles = new ObservableCollection<RoleModel>(await _roleService.GetAll());
    }

    [RelayCommand]
    private async Task Save(string exit)
    {
        if (_isEdit)
        {
            await UpdateUser(exit);
        }
        else
        {
            await AddUser();
        }

        if (!string.IsNullOrEmpty(exit))
        {
            LeaveForm();
        }
    }

    private bool ValidateUser()
    {
        ValidationMsg = "";
        
        UserValidator userValidator = new UserValidator();
        var validationResult = userValidator.Validate(User);

        if (validationResult.IsValid)
        {
            return true;
        }

        foreach (var error in validationResult.Errors)
        {
            ValidationMsg += error.ErrorMessage + "\n";
        }
        return false;
    }

    private async Task<bool> AddUser()
    {
        User.Username = Username;

        foreach (var role in Roles)
        {
            if (role.IsSelected)
            {
                User.Roles.Add(role);
            }
        }

        if (!ValidateUser()) return false;
            
        var result = await _userService.Add(User, _loggedInUser.UserId);
        ClearForm();
        
        return result;
    }

    private async Task<bool> UpdateUser(string exit)
    {
        User.Username = Username;
        User.Roles.Clear();

        foreach (var role in Roles)
        {
            if (role.IsSelected)
            {
                User.Roles.Add(role);
            }
        }

        if (!ValidateUser()) return false;

        var result = await _userService.Update(User, _loggedInUser.UserId);
        return result;
    }


    private void ClearForm()
    {
        User = new UserModel();
        Username = "";
        foreach (var role in Roles)
        {
            role.IsSelected = false;
        }
    }


    [RelayCommand]
    private void LeaveForm()
    {
        if (IsDataChanged())
        {
            WeakReferenceMessenger.Default.Send(new DisplayDialogMessage(new DisplayMessageModel
            {
                Title = "Upozorenje",
                Content = "Nisu spremljene promjene. Želite li izaći?",
                Btn = Btn.YesNo,
                IsVisible = true,
                Closed = async (result) =>
                {
                    if (result == Btn.Yes)
                    {
                        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("Users"));
                    }
                }
            }));
            return;
        }
        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("Users"));
    }
    
    private bool IsDataChanged()
    {
        if (User.Username != Username)
        {
            return true;
        }
        
        foreach (var role in Roles)
        {
            if (role.IsSelected != User.Roles.Any(x => x.Id == role.Id))
            {
                return true;
            }
        }
        
        return false;
    }
}