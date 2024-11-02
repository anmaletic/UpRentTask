namespace UpRentTask.Library.Controls;

public partial class MessageControl : UserControl
{
    public MessageControl()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
        nameof(Message), typeof(DisplayMessageModel), typeof(MessageControl));
    
    public DisplayMessageModel Message
    {
        get => (DisplayMessageModel)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var result = (Btn)Enum.Parse(
            typeof(Btn),
            ((Button)sender).CommandParameter.ToString()!);
        
        Close(result);
    }

    private void Close(Btn btn)
    {
        Message.Closed(btn);
        Message.IsVisible = false;
    }
}