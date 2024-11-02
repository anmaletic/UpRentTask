namespace UpRentTask.Library.Models;

public enum Btn
{
    Ok,
    Yes,
    No,
    YesNo
}

public class DisplayMessageModel : INotifyPropertyChanged
{
    private bool _isVisible;
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public Btn Btn { get; set; } = Btn.Ok;

    public Action<Btn> Closed { get; set; }  = _ => { };

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (value == _isVisible) return;
            _isVisible = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}