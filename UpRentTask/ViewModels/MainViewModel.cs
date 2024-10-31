using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace UpRentTask.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private UserControl _activeView;
}