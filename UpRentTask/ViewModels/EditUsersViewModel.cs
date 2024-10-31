namespace UpRentTask.ViewModels;

public partial class EditUsersViewModel : ObservableObject
{
    private readonly IRoleService _roleService;
    
    [ObservableProperty] private ObservableCollection<RoleModel>? _roles;

    public EditUsersViewModel(IRoleService roleService)
    {
        _roleService = roleService;
        
        Task.Run(async () => await Init());
    }
    
    private async Task Init()
    {
        Roles = new ObservableCollection<RoleModel>(await _roleService.GetAll());
    }

    [RelayCommand]
    private void LeaveForm()
    {
        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("Users"));
    }
}