namespace UpRentTask.Library.ViewManager;

public interface IViewManager
{
    UserControl GetView(string viewName, Dictionary<string, string>? parameters = null);
}
