namespace UpRentTask.Library.Messages;

public class ChangeViewMessage(string view, Dictionary<string, string>? parameters = null)
{
    public string View { get; set; } = view;
    public Dictionary<string, string>? Parameters { get; set; } = parameters;
}