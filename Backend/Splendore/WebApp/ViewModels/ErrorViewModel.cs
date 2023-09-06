namespace WebApp.ViewModels;
#pragma warning disable CS1591
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}