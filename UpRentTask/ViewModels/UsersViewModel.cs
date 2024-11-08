﻿namespace UpRentTask.ViewModels;

public partial class UsersViewModel : ObservableObject, IAsyncInitialization
{
    private readonly ILoggedInUser _loggedInUser;
    private readonly IUserService _userService;
    
    public Task Initialization { get; }
    
    [ObservableProperty] private UserModel? _selectedUser;
    [ObservableProperty] private ObservableCollection<UserModel>? _users;


    public UsersViewModel(ILoggedInUser loggedInUser, IUserService userService)
    {
        _loggedInUser = loggedInUser;
        _userService = userService;
        
        Initialization = Init();
    }
    
    private async Task Init()
    {
        Users = new ObservableCollection<UserModel>(await _userService.GetAll());
    }
    
    [RelayCommand]
    private void AddUser()
    {
        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("AddUser"));
    }
    
    [RelayCommand]
    private void EditUser()
    {
        if (!IsUserSelected())
        {
            return;
        }

        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("EditUser", new Dictionary<string, string>
        {
            { "UserId", SelectedUser!.UserId.ToString() }
        }));
    }

    [RelayCommand]
    private async Task DeleteUser()
    {
        if (!IsUserSelected())
        {
            return;
        }
        
        var result = await _userService.Delete(SelectedUser.UserId, _loggedInUser.UserId);
        
        if (result)
        {
            Users!.Remove(SelectedUser);
        }
    }
    
    private bool IsUserSelected()
    {
        if (SelectedUser is null)
        {
            WeakReferenceMessenger.Default.Send(new DisplayDialogMessage(new DisplayMessageModel
            {
                Title = "Upozorenje",
                Content = "Korisnik nije odabran.",
                IsVisible = true
            }));

            return false;
        }

        return true;
    }
    

}