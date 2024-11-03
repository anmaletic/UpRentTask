namespace UpRentTask.Library.ViewManager;

public class ViewManager : IViewManager
{
    private static ViewManager _instance;
    public static ViewManager Instance => _instance ??= new ViewManager();

    private readonly Dictionary<string, Func<Dictionary<string, string>, UserControl>> _views = new();

    public void AddView<TView, TViewModel>(string viewName)
        where TView : UserControl, new() where TViewModel : notnull
    {
        _views[viewName] = parameters =>
        {
            var view = new TView();
            var viewModel = Ioc.Default.GetRequiredService<TViewModel>();
            
            if (viewModel is IParameterized parameterizedVm)
            {
                parameterizedVm.OnParametersSet(parameters);
            }
            
            view.DataContext = viewModel;
            return view;
        };
    }

    public UserControl GetView(string viewName, Dictionary<string, string>? parameters = null)
    {
        if (_views.TryGetValue(viewName, out var viewFactory))
        {
            return viewFactory(parameters ?? new Dictionary<string, string>());
        }

        throw new KeyNotFoundException($"View '{viewName}' not found.");
    }
}