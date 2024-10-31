namespace UpRentTask.ViewModels;

public partial class EditUsersViewModel : ObservableObject
{
    [RelayCommand]
    private void LeaveForm()
    {
        WeakReferenceMessenger.Default.Send(new ChangeViewMessage("Users"));
    }
}