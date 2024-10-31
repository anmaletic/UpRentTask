using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using UprentTask.Library.Messages;

namespace UpRentTask.ViewModels;

public partial class EditUsersViewModel : ObservableObject
{
    [RelayCommand]
    private void LeaveForm()
    {
        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("Users"));
    }
}