namespace UpRentTask.Library.ViewManager;

public static class ViewManagerExtension
{
    public static IServiceCollection RegisterViewWithViewManager<TView, TViewModel>(this IServiceCollection services,
        string viewName)
        where TView : UserControl, new() where TViewModel : notnull
    {
        ViewManager.Instance.AddView<TView, TViewModel>(viewName);

        return services;
    }
}