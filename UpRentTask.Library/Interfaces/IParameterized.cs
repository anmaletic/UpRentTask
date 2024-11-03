namespace UpRentTask.Library.Interfaces;

public interface IParameterized
{
    Task OnParametersSet(Dictionary<string, string> parameters);
}