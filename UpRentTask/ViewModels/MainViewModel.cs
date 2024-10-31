using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using UpRentTask.Views;

namespace UpRentTask.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private UserControl _activeView = new UsersView();
}