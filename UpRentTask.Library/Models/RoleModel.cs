using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UpRentTask.Library.Models;

public class RoleModel : INotifyPropertyChanged
{
    private bool _isSelected;
    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (value == _isSelected)
            {
                return;
            }

            _isSelected = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
   
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}