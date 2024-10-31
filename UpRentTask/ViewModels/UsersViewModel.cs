using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using UprentTask.Library.Messages;
using UprentTask.Library.Models;

namespace UpRentTask.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    [ObservableProperty] private UserModel _selectedUser = new UserModel();
    [ObservableProperty] private ObservableCollection<UserModel> _users;


    public UsersViewModel()
    {
        Task.Run(() =>
        {
           GenerateDemoUsers();
        });
    }

    [RelayCommand]
    private void EditUsers()
    {
        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("EditUsers"));
    }

    private void GenerateDemoUsers()
    {
        Users = new ObservableCollection<UserModel>
        {
            new UserModel
            {
                UserId = 1,
                Username = "admin",
                CreatedDate = DateTime.Now,
                CreatedBy = new UserModel { UserId = 1, Username = "admin" },
                // Roles = new List<RoleModel>
                // {
                //     new RoleModel { RoleId = 1, RoleName = "Admin" }
                // }
            },
            new UserModel
            {
                UserId = 2,
                Username = "user",
                CreatedDate = DateTime.Now,
                CreatedBy = new UserModel { UserId = 1, Username = "admin" },
                ModifiedDate = DateTime.Now,
                ModifiedBy = new UserModel { UserId = 1, Username = "admin" },
                // Roles = new List<RoleModel>
                // {
                //     new RoleModel { RoleId = 2, RoleName = "User" }
                // }
            }
        };
        
    }
    
}