namespace UpRentTask.Library.Messages;

public class ChangeViewMessage(string view)
{
    public string View { get; set; } = view;
}