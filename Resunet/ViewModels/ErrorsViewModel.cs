using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Resunet.ViewModels;

public class ErrorsViewModel
{
    public ErrorsViewModel(ModelStateDictionary modelState)
    {
        foreach (var error in modelState.Keys)
        {
            Errors.Add(error, modelState[error].Errors[0].ErrorMessage ?? "");
        }
    }

    public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
}
