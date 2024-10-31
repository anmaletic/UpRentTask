using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using UpRentTask.DataAccess.Services;
using UprentTask.Library.Messages;
using UprentTask.Library.Models;

namespace UpRentTask.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    private readonly IUserService _userService;
    
    [ObservableProperty] private UserModel _selectedUser = new UserModel();
    [ObservableProperty] private ObservableCollection<UserModel>? _users;


    public UsersViewModel(IUserService userService)
    {
        _userService = userService;
        Task.Run(async () => await Init());
    }

    [RelayCommand]
    private void EditUsers()
    {
        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("EditUsers"));

    }
    
    private async Task Init()
    {
        Users = new ObservableCollection<UserModel>(await _userService.GetAll());
    }
}