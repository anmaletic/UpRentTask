namespace UpRentTask.Library.Messages;

public class DisplayDialogMessage(DisplayMessageModel msg)
{
    public  DisplayMessageModel Msg { get; set; } = msg;
}