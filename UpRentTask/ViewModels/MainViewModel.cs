using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using UprentTask.Library.Messages;
using UpRentTask.Views;

namespace UpRentTask.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private UserControl _activeView = new UsersView();

    public MainViewModel()
    {
        WeakReferenceMessenger.Default.Register<ChangeViewMessage>(this, (r, m) => 
            ActiveView = m.View switch
        {
            "EditUsers" => new EditUsersView(),
            "Users" => new UsersView(),
            _ => throw new ArgumentOutOfRangeException()
        });
    }
}